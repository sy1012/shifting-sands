using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class Boss2Anubis : Enemy
{
    public GameObject anubisReflection;
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
    private float dyingTimer = 2;

    protected override void Awake()
    {

    }

    public override void Start()
    {
        base.Start();
        room.enemies.Add(this);
    }

    // Start is called before the first frame update
    void Initialize()
    {
        EventManager.TriggerOnAnubisStart();
        // let it start path finding
        this.maxHealth = 750;
        teleportTo = Random.Range(1, 5);
        teleportTime = 1;
        attackTime = Random.Range(lowerAttackTime, upperAttackTime);
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
        bossText.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .895f));
        healthbar.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .89f));
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
                // handle the Anuibis layers
                if (this.transform.position.y - 1.5f >= Player.transform.position.y)
                {
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Default"); ;
                }
                else
                {
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Player");
                }


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
                    Teleporting();
                    teleportTime = Random.Range(lowerTeleportTime, upperTeleportTime);
                    if (teleportTo == 1) { this.transform.position = GameObject.Find("AnubisPillarOne").transform.position; }
                    else if (teleportTo == 2) { this.transform.position = GameObject.Find("AnubisPillarTwo").transform.position; }
                    else if (teleportTo == 3) { this.transform.position = GameObject.Find("AnubisPillarThree").transform.position; }
                    else if (teleportTo == 4) { this.transform.position = GameObject.Find("AnubisPillarFour").transform.position; }
                    int last = teleportTo;
                    while (teleportTo == last)
                    {
                        Debug.Log("What?");
                        teleportTo = Random.Range(0, 5);
                    }
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
                        // the anubis' current location will always be bombed
                        bomb = Instantiate(bombPrefab);
                        bombs.Add(bomb);
                        bomb.GetComponent<AnubisBombs>().player = Player;
                        bomb.transform.position = this.transform.position;

                        // randomly spread 20 bombs throughout the rest of the room
                        for (int row = -10; row < 10; row += 2)
                        {
                            for (int column = -10; column < 10; column += 2)
                            {
                                bomb = Instantiate(bombPrefab);
                                bombs.Add(bomb);
                                bomb.GetComponent<AnubisBombs>().player = Player;
                                bomb.transform.position = (Vector2)this.room.transform.position + 
                                new Vector2(Random.Range(row -1f, row + 1f), Random.Range(column - 1f, column + 1f));
                            }
                        }
                        attackingTimer = 1.5f;
                        animator.SetBool("Attack", true);
                        EventManager.TriggerOnAnubisAttack();
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
                    }
                }
            }

            else if (Vector3.Distance(Player.transform.position, transform.position) < 5)
            {
                startFight = true;
            }
        }
        else
        {
            dyingTimer -= Time.deltaTime;
            if (dyingTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Teleporting()
    {
        GameObject temp = Instantiate(anubisReflection);
        if (teleportTo == 1) { temp.GetComponent<AnubisReflection>().target = GameObject.Find("AnubisPillarOne").transform.position; }
        else if (teleportTo == 2) { temp.GetComponent<AnubisReflection>().target = GameObject.Find("AnubisPillarTwo").transform.position; }
        else if (teleportTo == 3) { temp.GetComponent<AnubisReflection>().target = GameObject.Find("AnubisPillarThree").transform.position; }
        else if (teleportTo == 4) { temp.GetComponent<AnubisReflection>().target = GameObject.Find("AnubisPillarFour").transform.position; }
        EventManager.TriggerOnAnubisTeleport();
        temp.transform.position = this.transform.position;
    }

    public override void OnCollisionStay2D(Collision2D collision) { }

    public override void TakeDamage(int damage)
    {
        if (!startFight) { return; }
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
        EventManager.TriggerOnAnubisEnd();
        dying = true;
        animator.SetBool("Dying", true);
        animator.speed = .5f;

        // Tell data keeper anubis was beat
        DungeonDataKeeper.getInstance().beatAnubis = true;

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
