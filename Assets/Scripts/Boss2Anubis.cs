using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class Boss2Anubis : Enemy
{
    private float upperTeleportTime = 20;
    private float lowerTeleportTime = 10;
    private float upperAttackTime = 8;
    private float lowerAttackTime = 5;
    private bool attacking = false;
    private float attackTime;
    private float teleportTime;
    private int teleportTo = 1;  // This will hold the value 1-4 (inclusive) to determine where he will teleport next
    public bool startFight = false;
    private bool initialized = false;
    private float rotatingTime = 1;
    private float chargeTimer;


    protected override void Awake()
    {

    }


    // Start is called before the first frame update
    void Initialize()
    {
        // let it start path finding
        this.maxHealth = 500;
        teleportTo = Random.Range(0, 4);

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
        if (startFight)
        {
            if (!initialized)
            {
                Initialize();
            }

            // If he isn't ready to teleport update his cooldown on the teleport, if he is then teleport him and reset the timer
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
                teleportTo = Random.Range(0, 4);

            }

            if (room == null)
            {
                throw new System.Exception("The Boss enemy:" + transform.name + "'s room is Null. Set Anubis' room.");
            }
            if (psm == null)
            {
                throw new System.Exception("The Anubis' enemy:" + transform.name + "'s target player is Null");
            }
            //if the player is in the same room as the Mummy, and is within the Mummy's detection radius, pursue the player to attack!
            if (room.Equals(psm.GetRoom()))
            {

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
}
