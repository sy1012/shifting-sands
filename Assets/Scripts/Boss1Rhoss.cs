using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class Boss1Rhoss : Enemy
{
    public float mvmt_timer = 0.0f; //used to update enemy movement every few seconds.
    public int mvmt_seconds = 0;
    private int upperChargeTime = 4;  // how often should the enemy try to charge
    private int lowerChargeTime = 2;  // random amount between these two each time
    public bool startFight = false;
    private bool initialized = false;
    private int chargeSpeed = 20;
    private int runSpeed = 4;
    private bool charging;
    private float stunned;
    private float rotatingTime = 1;
    private float chargeTimer;
    [SerializeField]
    Animator animLegs;
    [SerializeField]
    Animator animBody;
    private bool isDying;

    protected override void Awake()
    {
        animLegs.speed = 1;
    }

    IEnumerator Intro()
    {
        animBody.SetTrigger("awake");
        animLegs.SetTrigger("awake");
        yield return new WaitForSeconds(4);
        startFight = true;
    }

    IEnumerator Death()
    {
        animBody.SetTrigger("dead");
        DungeonDataKeeper.getInstance().beatRhoss = true;
        yield return new WaitForSeconds(4.8f);
        base.Die();
    }

    // Start is called before the first frame update
    void Initialize()
    {
        // let it start path finding
        this.GetComponent<IAstarAI>().canMove = true;
        this.maxHealth = 1000;

        //character awake
        healthCanvas = Instantiate(healthCanvasPrefab, transform.position, transform.rotation, gameObject.transform);
        health = maxHealth;
        healthbar = healthCanvas.GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(maxHealth);
        Canvas canvas = healthbar.GetComponentInParent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        GameObject bossText = new GameObject("boss text");
        bossText.transform.SetParent(this.transform);
        bossText.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        TextMeshProUGUI bossTextUI = bossText.AddComponent<TextMeshProUGUI>();
        bossTextUI.font = Resources.Load("CASTELAR SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
        bossTextUI.text = "Rhoss the Rammy";
        bossTextUI.fontSize = 25;
        bossTextUI.color = Color.white;
        bossTextUI.alignment = TextAlignmentOptions.TopGeoAligned;
        bossText.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .9f));
        healthbar.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .80f));
        healthCanvas.transform.position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .9f));

        destination.target = Player;

        initialized = true;
        damageSpeed = 1;
        damage = 30;
        detectionRange = 10;
        randTarget = new GameObject().transform;
        //move healthbar to a more suitable position
        //healthCanvas.transform.position = transform.position + new Vector3(0, 2.5f, 0);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned <= 0 && !isDying)
        {
            if (startFight)
            {
                if (!initialized)
                {
                    Initialize();
                }

                if (chargeTimer > 0)
                {
                    chargeTimer -= Time.deltaTime;
                }

                // he failed to connect stop the charge
                if (charging && chargeTimer <= 0)
                {
                    charging = false;
                    chargeTimer = Random.Range(lowerChargeTime, upperChargeTime);
                }

                //update the timer and number of seconds passed since last movement
                mvmt_timer += Time.deltaTime;
                mvmt_seconds = (int)(mvmt_timer % 60);

                if (room == null)
                {
                    throw new System.Exception("The Boss enemy:" + transform.name + "'s room is Null. Set its' room.");
                }
                if (psm == null)
                {
                    throw new System.Exception("The Boss enemy:" + transform.name + "'s target player is Null");
                }
                //if the player is in the same room as Rhoss, and is within Rhoss's detection radius, pursue the player to attack!
                if (room.Equals(psm.GetRoom()) && Vector3.Distance(Player.position, transform.position) <= detectionRange && !charging && stunned <= 0 && chargeTimer <= 0)
                {
                    Debug.Log("Trying to charge");
                    if (rotatingTime >= 0) { Rotate(); }
                    else
                    {
                        this.GetComponent<Seeker>().graphMask = 2;
                        this.GetComponent<AIPath>().maxSpeed = chargeSpeed;
                        this.GetComponent<AIPath>().canSearch = true;
                        this.GetComponent<AIPath>().rotationSpeed = 10;
                        charging = true;
                        rotatingTime = 1;
                        chargeTimer = 1;
                        animLegs.speed = 4.5f;
                    }
                }
                //enter randomly-moving patrol mode if the player isn't nearby.
                //(If the player is in the same room and is out of range, patrol randomly).
                else if (room.Equals(psm.GetRoom()) && !charging && stunned <= 0)
                {
                    this.GetComponent<AIPath>().maxSpeed = runSpeed;
                    this.GetComponent<AIPath>().canSearch = true;
                    this.GetComponent<AIPath>().rotationSpeed = 360;
                }
            }
            else if (Vector3.Distance(Player.transform.position,transform.position)<5)
            {
                StartCoroutine(Intro());
            }
        }
        else
        {
            animLegs.speed = 0.24f;
            this.stunned -= Time.deltaTime;
            if (this.stunned <= 0)
            {
                this.GetComponent<AIPath>().canSearch = true;
                this.GetComponent<AIPath>().rotationSpeed = 360;
                animLegs.speed = 1.2f;
            }
        }
    }

    public void Rotate()
    {
        this.GetComponent<Seeker>().graphMask = 3;
        this.GetComponent<AIPath>().canSearch = true;
        this.GetComponent<AIPath>().maxSpeed = 0.0001f;
        this.GetComponent<AIPath>().rotationSpeed = 360;
        this.rotatingTime -= Time.deltaTime;
    }

    public override void Die()
    {
        if (isDying)
        {
            //cant take more damage
            return;
        }
        
        isDying = true;
        StartCoroutine(Death());
        return;
    }

    public override void TakeDamage(int damage)
    {
        if (startFight)
        {
            base.TakeDamage(damage);
        } 
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        

        if (collision.collider.transform == Player && cooldown <= 0 && stunned <= 0 && health > 0)
        {
            
            cooldown = damageSpeed;
            psm.TakeDamage(damage, collision);
            stunned = 1;
        }
        else if (collision.collider.name == "Pillar" && this.charging)
        {
            this.TakeDamage(200);
            this.stunned = 4;
            this.charging = false;
            Destroy(collision.collider.gameObject);
            this.GetComponent<AIPath>().maxSpeed = 0;
            this.GetComponent<AIPath>().canMove = true;
        }
        this.GetComponent<AIPath>().rotationSpeed = 360;
        this.GetComponent<AIPath>().maxSpeed = 0;
        this.charging = false;
        chargeTimer = Random.Range(lowerChargeTime, upperChargeTime);
    }

}
