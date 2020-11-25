﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class Boss1Rhoss : Enemy
{
    public float mvmt_timer = 0.0f; //used to update enemy movement every few seconds.
    public int mvmt_seconds = 0;
    private int upperChargeTime = 10;  // how often should the enemy try to charge
    private int lowerChargeTime = 5;  // random amount between these two each time
    public bool startFight = false;
    private bool initialized = false;
    private int chargeSpeed = 20;
    private int runSpeed = 7;
    private bool charging;
    private float stunned;
    private float rotatingTime = 2;
    private float chargeTimer;


    protected override void Awake()
    {

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
        bossText.AddComponent<TextMeshProUGUI>().text = "Rhoss the Rammy";
        bossText.GetComponent<TextMeshProUGUI>().fontSize = 25;
        bossText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopGeoAligned;
        bossText.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .9f));
        healthbar.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .75f));
        healthCanvas.transform.position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .9f));

        initialized = true;
        damageSpeed = 1;
        damage = 45;
        detectionRange = 10;
        randTarget = new GameObject().transform;
        //move healthbar to a more suitable position
        //healthCanvas.transform.position = transform.position + new Vector3(0, 2.5f, 0);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned <= 0)
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

                //update the timer and number of seconds passed since last movement
                mvmt_timer += Time.deltaTime;
                mvmt_seconds = (int)(mvmt_timer % 60);

                if (room == null)
                {
                    throw new System.Exception("The Boss enemy:" + transform.name + "'s room is Null. Set Mummy's room.");
                }
                if (psm == null)
                {
                    throw new System.Exception("The Boss enemy:" + transform.name + "'s target player is Null");
                }
                //if the player is in the same room as the Mummy, and is within the Mummy's detection radius, pursue the player to attack!
                if (room.Equals(psm.GetRoom()) && Vector3.Distance(Player.position, transform.position) <= detectionRange && !charging && stunned <= 0 && chargeTimer <= 0)
                {
                    Debug.Log("Trying to charge");
                    if (rotatingTime >= 0) { Rotate(); }
                    else
                    {
                        this.GetComponent<AIPath>().maxSpeed = chargeSpeed;
                        this.GetComponent<AIPath>().canSearch = true;
                        this.GetComponent<AIPath>().rotationSpeed = 360;
                        charging = true;
                        rotatingTime = 2;
                        chargeTimer = Random.Range(lowerChargeTime, upperChargeTime);
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
        }
        else
        {
            this.stunned -= Time.deltaTime;
            if (this.stunned <= 0)
            {
                this.GetComponent<AIPath>().canSearch = true;
                this.GetComponent<AIPath>().rotationSpeed = 360;
            }
        }
    }

    public void Rotate()
    {
        this.GetComponent<AIPath>().canSearch = true;
        this.GetComponent<AIPath>().maxSpeed = 0;
        this.GetComponent<AIPath>().rotationSpeed = 360;
        this.rotatingTime -= Time.deltaTime;
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.transform == Player && cooldown <= 0 && stunned <= 0)
        {
            cooldown = damageSpeed;
            psm.TakeDamage(damage, collision);
        }
        else if (collision.collider.name == "Pillar" && this.charging)
        {
            this.TakeDamage(150);
            this.stunned = 2;
            this.charging = false;
            Destroy(collision.collider.gameObject);
            this.GetComponent<AIPath>().maxSpeed = 0;
            this.GetComponent<AIPath>().canMove = true;
        }
    }
}
