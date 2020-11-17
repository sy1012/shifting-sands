using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : Character
{
    [SerializeField] public LayerMask dashLayerMask;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    GameObject text;
    GameObject background;
    public float playerRootOffset = -0.5f;
    public EquipmentManager equipment;
    public Transform HealthBarTransform;
    public Vector3 healthbarScale;
    public Weapon GetWeapon { get => equipment.GetWeapon(); }
    KeyCode interactKey = KeyCode.F;
    KeyCode InventoryKey = KeyCode.V;
    KeyCode dashKey = KeyCode.Space;

    // Dash Cooldown
    private float dashCoolDown = 1f;
    private float nextDashTime = 0f;

    //!!The Behavioural State of the Player!!
    public State state;
    // Debugging representation of state
    public StateEnum currentState;
    public float speed;
    private float invincibleTime;
    private bool onTriggerStay2D;

    public float InvincibleTime { get => invincibleTime; set => invincibleTime = value; }

    protected override void Awake()
    {
        base.Awake();
        EventManager.onDungeonGenerated += EnterDungeon;
    }

    //Change state. This is used by states to change the players current state. Always call states Enter() routine. Enum is just for debugging.
    public void SetState(State newstate)
    {
        currentState = newstate.stateEnum;
        state = newstate;
        StartCoroutine(newstate.Enter());
    }

    private void EnterDungeon(EventArgs e)
    {
        DungeonGenArgs de = e as DungeonGenArgs;
        Transform entrance = LevelUtils.FindEntrance(de.Graph, de.Rooms);
        Room entranceRoom = entrance.GetComponent<Room>();
        entranceRoom.PlaceObject(this);
        //offset down so player is in front of ladder and ZERO Z position
        transform.position -= Vector3.up;
        //No parent
        transform.SetParent(null);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set intial set to normal
        SetState(new NormalState(this));
        
        // Increase the scale of the healthbar
        healthbarScale = healthCanvas.transform.localScale;
        healthbarScale.x += 0.01f;
        healthbarScale.y += 0.01f;
        healthCanvas.transform.localScale = healthbarScale;
        // Move Heakthbar to bottom left 
        healthCanvas.transform.position = HealthBarTransform.position;

        dashLayerMask.value = 10;
        triggerCollisions = new List<Collider2D>();
       
    }

    //Try to get input from player here consistently
    public Vector2 GetArrowKeysDirectionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }

    public Room currentRoom;
    internal void SetRoom(Room newRoom)
    {
        currentRoom = newRoom;
    }

    internal Room GetRoom()
    {
        return currentRoom;
    }

    public void MoveCharacter(Vector2 heading, float speed)
    {
        rb.MovePosition(rb.position + heading * speed * Time.fixedDeltaTime);
    }

    //Apply Normal movement of the player based on arrow keys
    public void NormalMovement()
    {
        Vector2 input = GetArrowKeysDirectionalInput();
        // For Animator Idle State
        if (input != Vector2.zero)
        {
            animator.SetFloat("PrevHorizontal", input.x);
            animator.SetFloat("PrevVertical", input.y);
        }
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
        MoveCharacter(GetArrowKeysDirectionalInput(), speed);
    }
    //Fraction of normal movement. e.g. post roll, or hit
    public void NormalMovementFraction(float fraction)
    {
        Vector2 input = GetArrowKeysDirectionalInput();
        animator.SetFloat("Horizontal", input.x*fraction);
        animator.SetFloat("Vertical", input.y*fraction);
        animator.SetFloat("Speed", input.sqrMagnitude*fraction);
        MoveCharacter(GetArrowKeysDirectionalInput(), speed*fraction);
    }

    // Update is called once per frame
    void Update()
    {
        //State Specific Behaviour

        //Handle Input
        if (Input.GetKeyDown(interactKey))
        {
            StartCoroutine(state.Interact());
        }

        if (Input.GetKeyDown(InventoryKey))
        {
            StartCoroutine(state.Inventory());
        }

        // Dash Input with a CD
        if (Time.time > nextDashTime)
        {
            if (Input.GetKeyDown(dashKey))
            {
                StartCoroutine(state.OnRoll());
                nextDashTime = Time.time + dashCoolDown;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(state.OnAttack());
        }

        //Every state has an execute method. May be empty.
        state.Execute();

        //Global Player State Behaviour. Independant of state player logic goes here!

        //Handle Death
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //Lower invincibility time
        InvincibleTime -= Time.deltaTime;

        //Handle Loot
        if (DungeonMaster.getLootInRange(this.transform.position, 1).Count != 0)
        {
            List<GameObject> loot = DungeonMaster.getLootInRange(this.transform.position, 1);
            foreach (GameObject item in loot)
            {
                if (item.GetComponent<ItemArchtype>() != null) item.GetComponent<ItemArchtype>().CreateInfoPopUp();
            }
        }
        if (DungeonMaster.getLootOuttaRange(this.transform.position, 1).Count != 0)
        {
            List<GameObject> loot = DungeonMaster.getLootOuttaRange(this.transform.position, 1);
            foreach (GameObject item in loot)
            {
                if (item.GetComponent<ItemArchtype>() != null) item.GetComponent<ItemArchtype>().DestroyInfoPopUp();
            }
        }

    }

    //  Sync Up Trigger Data so that it can be reliable accesses within Update cycle. 
    //  Accessor for Trigger Colliders the player is currently in
    public List<Collider2D> GetTriggerCollisions { get { return triggerCollisions; } }
    List<Collider2D> triggerCollisions;
    bool onTriggerStay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerStay = true;
        triggerCollisions.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerCollisions.Remove(collision);
        if (triggerCollisions.Count<=0)
        {
            onTriggerStay = false;
        }
    }


    public override void TakeDamage(int damage, Collision2D collision)
    {
        if (InvincibleTime>0)
        {
            return;
        }
        StartCoroutine(state.OnHit(damage,collision));
    }
    // Get the world position of the characters feet, the place he is standing. This is determined by an offset from the transform center
    public Vector2 GetRoot()
    {
        return new Vector2(transform.position.x, transform.position.y + playerRootOffset);
    }
    //  Handle Triggers

    public void SetHealthBar(Healthbar h)
    {
        healthbar =  h;
    }

}
