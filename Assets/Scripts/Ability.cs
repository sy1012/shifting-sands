using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Ability : MonoBehaviour
{

    public Transform firePoint;
    public GameObject fireballPrefab;
    public GameObject snowballPrefab;
    public float speed = 5f;
    public Camera cam;
    public Vector3 mousePos;
    private Vector3 dir;
    

    // Ability 1
    [Header("Ability 1")]
    public Image abilityImage1;
    public float coolDown1 = 3f;
    private bool isCoolDown1;
    private KeyCode abilitybutton1 = KeyCode.E;

    // Ability 2
    [Header("Ability 2")]
    public Image abilityImage2;
    public float coolDown2 = 3f;
    private bool isCoolDown2;
    private KeyCode abilitybutton2 = KeyCode.R;

    private void Start()
    {
        if (cam==null)
        {
            cam = Camera.current;
        }

        // Set Fill amounts to 0
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        isCoolDown1 = false;
        isCoolDown2 = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        // Ability1 Button Pressed and not on CD
        if (Input.GetKeyDown(abilitybutton1) && isCoolDown1 == false)
        {
            isCoolDown1 = true;
            abilityImage1.fillAmount = 1;
            Fireball();
                        
        }

        // Ability2 Button Pressed and not on CD
        if (Input.GetKeyDown(abilitybutton2) && isCoolDown2 == false)
        {
            isCoolDown2 = true;
            abilityImage2.fillAmount = 1;
            SnowBall();

        }
        
    }

    private void FixedUpdate()
    {

        if (isCoolDown1)
        {
            // The CoolDown Effect 
            abilityImage1.fillAmount -= 1 / coolDown1 * Time.deltaTime;
            if (abilityImage1.fillAmount <= 0)
            {
                isCoolDown1 = false;
                abilityImage1.fillAmount = 0;
            }
        }

        if (isCoolDown2)
        {
            // The CoolDown Effect 
            abilityImage2.fillAmount -= 1 / coolDown2 * Time.deltaTime;
            if (abilityImage2.fillAmount <= 0)
            {
                isCoolDown2 = false;
                abilityImage2.fillAmount = 0;
            }
        }
    }

    public void Fireball()
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
        fireball.transform.right = dir;

    }

    public void SnowBall()
    {
        // Trigger the Event for others
        EventManager.TriggerOnCastSnowball();

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
        GameObject snowball = Instantiate(snowballPrefab, spawnnPoint, firePoint.rotation);
        snowball.transform.right = dir;

    }

}
