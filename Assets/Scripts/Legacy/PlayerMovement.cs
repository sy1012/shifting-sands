using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : Character
{

    public float moveSpeed = 5.0f;

    // Layer Mask for the dash
    [SerializeField] private LayerMask dashLayerMask;

    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public Transform player;
    private bool isDashButtonDown;
    private bool isAttacking;
    GameObject text;
    GameObject background;
    public float playerRootOffset = -0.5f;
    public Weapon weaponEquiped;
    KeyCode interactKey = KeyCode.E;
    private bool isInTransition;

    private void Start()
    {
        //move healthbar to a more suitable position
        healthCanvas.transform.position = transform.position + new Vector3(0, 1f, 0);
        dashLayerMask.value = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            pickUp();
        }

        if (DungeonMaster.getLootInRange(this.transform.position, 1).Count != 0)
        {
            List<GameObject> loot = DungeonMaster.getLootInRange(this.transform.position, 1);
            foreach (GameObject item in loot)
            {
                Debug.Log("Hello");
                if (item.GetComponent<ItemArchtype>() != null) item.GetComponent<ItemArchtype>().CreateInfoPopUp();
            }
        }
        if (DungeonMaster.getLootOuttaRange(this.transform.position, 1).Count != 0)
        {
            List<GameObject> loot = DungeonMaster.getLootInRange(this.transform.position, 1);
            foreach (GameObject item in loot)
            {
                Debug.Log("Hello");
                if (item.GetComponent<ItemArchtype>() != null) item.GetComponent<ItemArchtype>().DestroyInfoPopUp();
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // For Animator Idle State
        if(movement != Vector2.zero)
		{
            animator.SetFloat("PrevHorizontal", movement.x);
            animator.SetFloat("PrevVertical", movement.y);
		}
        

        // Movement Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = Vector3.ClampMagnitude(movement, 1f);

        // Make sure it hasnt died
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // Close Application on pressing "Escape"
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // Animator Variables
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Dash Key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }

        // Swing Key press
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
        }

    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Move the weapon
        if (weaponEquiped != null)
        {
            weaponEquiped.transform.position = this.transform.position;
        }

        // Dash
        if (isDashButtonDown == true)
        {
            float dashAmount = 100f;
            Vector3 dashPosition = rb.position + movement * dashAmount * Time.fixedDeltaTime;

            // RayCast to avoid teleporting through walls
            RaycastHit2D raycastHit2d = Physics2D.Raycast(GetRoot(), movement, dashAmount * Time.fixedDeltaTime, dashLayerMask);
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
                dashPosition = new Vector2(dashPosition.x, dashPosition.y - playerRootOffset);
            }

            // transform.position = dashPosition;
            rb.MovePosition(dashPosition);
            isDashButtonDown = false;
        }

        // attack
        if (isAttacking && weaponEquiped != null)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Rad2Deg * Mathf.Atan((this.transform.position.y - mouse.y) / (this.transform.position.x - mouse.x));

            // now convert the angle into a degrees cw of up(north) based on the current value of direction and what quadrant it is in
            // quadrant 1
            if (mouse.y >= this.transform.position.y && mouse.x >= this.transform.position.x) angle -= 90;

            // quadrant 2
            else if (mouse.y >= this.transform.position.y) angle += 90;

            // quadrant 3
            else if (mouse.x <= this.transform.position.x) angle += 90;

            // quadrant 4
            else angle -= 90;
            weaponEquiped.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponEquiped.Attack();
            isAttacking = false;
        }
 
    }



    public Vector2 GetRoot()
    {
        return new Vector2(transform.position.x, transform.position.y + playerRootOffset);
    }

    private void pickUp()
    {
        List<GameObject> items = DungeonMaster.getLootInRange(this.transform.position, 1);
        foreach (GameObject item in items)
        {
            DungeonMaster.loot.Remove(item);
            Destroy(item);
        }
    }
}
