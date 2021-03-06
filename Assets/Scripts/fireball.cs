﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    // Hit effect for later
    public GameObject hitEffect;
    private int fireballDamage = 70;
    private int speed = 8;
    public Animator animator;

    public void Update()
    {
        transform.position = transform.position + transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Can access what u hit 4 health stuff
        GameObject target = collision.gameObject;
        if (target.GetComponent<IDamagable>() != null)
        {
            target.GetComponent<IDamagable>().TakeDamage(fireballDamage);
        } 

        // You can have a Hit Effect
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

        // trigger explosion sounds
        EventManager.TriggerOnFireballCollison();

        // Destrying the Fireball
        Destroy(gameObject);

        // Destroy Effect
        Destroy(effect, 1f);

    }
}
