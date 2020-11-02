using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{

    public Transform firePoint;
    public GameObject fireballPrefab;
    public float speed = 10f;
    // public Rigidbody2D rb;
    public Camera cam;
    public Vector2 mousePos;
    private bool isUsingAbility1;

    // Update is called once per frame
    void Update()
    {
        
        // Ability1 Button or Fireball Pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isUsingAbility1 = true;
        }
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
            isUsingAbility1 = false;
            Destroy(fireball, 1);
        }
    }
}
