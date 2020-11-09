using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public Transform firePoint;
    public GameObject fireballPrefab;
    public float speed = 5f;
    // public Rigidbody2D rb;
    public Camera cam;
    public Vector3 mousePos;
    private Vector3 dir;
    
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                isUsingAbility1 = true;
                nextFireTime1 = Time.time + coolDown1;
            }
        }

        // For ability 2 chk isUsing Ability 1 etc
        // Also array of prefabs for the abilities...
    
    }

    private void FixedUpdate()
    {
        // Cast Ability 1
        if (isUsingAbility1)
        {
            // Trigger the Event for others
            EventManager.TriggerOnCastFireball();


            // Get Mouse Position
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 firePos = firePoint.position;
            firePos.z = 0;
            // Calculate Direction with firepoint Position
            dir = (mousePos - firePoint.position).normalized;
            
            // Using a spawnpoint so firePoint position doesn't keep changing
            Vector3 spawnnPoint = firePos + dir;


            // Get the fireball's transform
            GameObject fireball = Instantiate(fireballPrefab, spawnnPoint, firePoint.rotation);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

            fireball.transform.right = dir;
            // Reset the is UsingAbility1
            isUsingAbility1 = false;
            
        }
    }
}
