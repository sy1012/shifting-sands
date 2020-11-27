using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class Boss2Anubis : Enemy
{
    public GameObject bombPrefab;
    public Animator animator;
    private float upperTeleportTime = 10;
    private float lowerTeleportTime = 5;
    private float upperAttackTime = 5;
    private float lowerAttackTime = 3;
    private bool attacking = false;
    private float attackingTimer = 0; // an attack isnt instant
    private float attackTime;
    private float teleportTime;
    private int teleportTo = 1;  // This will hold the value 1-4 (inclusive) to determine where he will teleport next
    public bool startFight = false;
    private bool initialized = false;
    GameObject bomb;
    private List<GameObject> bombs;
    private GameObject bossText;
    private bool dying;
    private float dyingTimer;

    protected override void Awake()
    {

    }


    // Start is called before the first frame update
    void Initialize()
    {
        // let it start path finding
        this.maxHealth = 50;
        teleportTo = Random.Range(1, 5);
        bombs = new List<GameObject>();

        //character awake
        healthCanvas = Instantiate(healthCanvasPrefab, transform.position, transform.rotation, gameObject.transform);
        health = maxHealth;
        healthbar = healthCanvas.GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(maxHealth);
        Canvas canvas = healthbar.GetComponentInParent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        bossText = new GameObject("boss text");
        bossText.transform.SetParent(this.transform);
        bossText.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        bossText.AddComponent<TextMeshProUGUI>().text = "Anubis The Wise";
        bossText.GetComponent<TextMeshProUGUI>().fontSize = 25;
        bossText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopGeoAligned;
        bossText.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .9f));
        healthbar.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .80f));
        healthCanvas.transform.position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .9f));

        initialized = true;
        damage = 20;
        detectionRange = 10;
        randTarget = new GameObject().transform;
        //move healthbar to a more suitable position
        //healthCanvas.transform.position = transform.position + new Vector3(0, 2.5f, 0);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            if (startFight)
            {
                if (!initialized)
                {
                    Initialize();
                }

                // If he isn't ready to teleport update his cooldown on the teleport, if he is then teleport him and reset the timer
                // Make him finish his attack if he is attacking though
                if (teleportTime > 0)
                {
                    teleportTime -= Time.deltaTime;
                }
                else
                {
                    teleportTime = Random.Range(lowerTeleportTime, upperTeleportTime);
                    if (teleportTo == 1) { this.transform.position = GameObject.Find("AnubisPillarOne").transform.position; }
                    else if (teleportTo == 2) { this.transform.position = GameObject.Find("AnubisPillarTwo").transform.position; }
                    else if (teleportTo == 3) { this.transform.position = GameObject.Find("AnubisPillarThree").transform.position; }
                    else if (teleportTo == 4) { this.transform.position = GameObject.Find("AnubisPillarFour").transform.position; }
                    teleportTo = Random.Range(0, 5);
                }

                if (attackTime > 0)
                {
                    attackTime -= Time.deltaTime;
                }
                else
                {
                    attacking = true;
                }

                if (room == null)
                {
                    throw new System.Exception("The Boss enemy:" + transform.name + "'s room is Null. Set Anubis' room.");
                }
                if (psm == null)
                {
                    throw new System.Exception("The Anubis' enemy:" + transform.name + "'s target player is Null");
                }

                //if the player is in the same room and the attack time is 0 then start an attack
                if (room.Equals(psm.GetRoom()) && attacking)
                {
                    // this is the first frame of the attack so set everything in motion
                    if (attackingTimer <= 0)
                    {
                        Debug.Log("ATTACKING!");
                        // the four entrances will always be bombed
                        bomb = Instantiate(bombPrefab);
                        bombs.Add(bomb);
                        bomb.GetComponent<AnubisBombs>().player = Player;
                        bomb.transform.position = this.transform.position;
                        attackingTimer = 1.5f;
                        animator.SetBool("Attack", true);
                    }

                    attackingTimer -= Time.deltaTime;
                
                    // this will only run at the end of a attack
                    if (attackingTimer <= 0)
                    {
                        // Detonate the bombs
                        foreach (GameObject bomb in bombs)
                        {
                            bomb.GetComponent<AnubisBombs>().Detonate();
                        }
                        bombs = new List<GameObject>();

                        attackTime = Random.Range(lowerAttackTime, upperAttackTime);
                        attacking = false;
                        animator.SetBool("Attack", false);
                        Debug.Log("Explosion noises!");
                    }
                }
            }
        }
        else
        {
            dyingTimer -= Time.deltaTime;
            if (dyingTimer <= 0)
            {
                //Destroy(gameObject);
            }
        }
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.transform == Player)
        {
            cooldown = damageSpeed;
            psm.TakeDamage(0, collision);
        }
    }

    public override void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            if (gameObject.GetComponent<Enemy>() != null)
            {
                gameObject.GetComponent<Enemy>().generateLoot();
            }
            Die();
        }
        healthbar.SetHealth(health);
    }

    public void Die()
    {
        dying = true;
        animator.SetBool("Dying", true);
        animator.speed = .5f;

        // Get rid of health bar and name
        Destroy(bossText);
        Destroy(healthbar);

        // get rid of the bombs
        foreach(GameObject bomb in bombs)
        {
            Destroy(bomb);
        }
    }
}
