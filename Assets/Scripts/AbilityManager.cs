using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public Transform firePoint;
    public GameObject fireballPrefab;
    public float speed = 10f;
    // public Rigidbody2D rb;
    public Camera cam;
    public Vector2 mousePos;
    
    // Ability 1
    [Header("Ability 1")]
    public float coolDown1 = 2f;
    private float nextFireTime1 = 0f;
    private bool isUsingAbility1;

    // Update is called once per frame
    void Update()
    {
        
        // Ability1 Button or Fireball Pressed
        if (Time.time > nextFireTime1)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isUsingAbility1 = true;
                nextFireTime1 = Time.time + coolDown1;
            }
        }

        // For ability 2 chk isUsing Ability 1 etc
    
    }

    private void FixedUpdate()
    {
        // Cast Ability 1
        if (isUsingAbility1)
        {
            // Trigger the Event for others
            EventManager.TriggerOnCastFireball();
            
            // Get the fireball's transform
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            
            // Get Mouse Position
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            // Calculate Direction
            Vector2 dir = mousePos - rb.position;
            rb.velocity = dir.normalized * speed;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            // Reset the is UsingAbility1
            isUsingAbility1 = false;
            
        }
    }
}
