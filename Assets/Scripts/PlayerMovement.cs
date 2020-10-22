using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    // Layer Mask for the dash
    [SerializeField] private LayerMask dashLayerMask;

    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public Transform player;
    public bool isDashButtonDown;

    // Update is called once per frame
    void Update()
    {
        // Movement Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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

    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Dash
        if (isDashButtonDown == true)
        {
            float dashAmount = 100f;
            Vector3 dashPosition = rb.position + movement * dashAmount * Time.fixedDeltaTime;

            // RayCast to avoid teleporting through walls
            RaycastHit2D raycastHit2d = Physics2D.Raycast(rb.position, movement, dashAmount * Time.fixedDeltaTime, dashLayerMask);
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
            }

            rb.MovePosition(dashPosition);
            isDashButtonDown = false;
        }
 
    }
}
