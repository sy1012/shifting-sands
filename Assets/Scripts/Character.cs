﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHealable, IDamagable
{
    public bool isMousedOver = false;
    public int maxHealth = 100;
    public int health;
    //below are used to instantiate and manage the healthbar
    public Canvas healthCanvasPrefab;
    protected Canvas healthCanvas;
    private Healthbar healthbar;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        healthCanvas = Instantiate(healthCanvasPrefab, transform.position, transform.rotation, gameObject.transform);
        health = maxHealth;
        healthbar = healthCanvas.GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(maxHealth);
    }
    private void OnMouseOver()
    {
        isMousedOver = true;
    }

    private void Update()
    {

    }

    private void OnMouseExit()
    {
        isMousedOver = false;
    }

    public virtual void TakeDamage(int damage, Collision2D collision)
    {
        
        health -= damage;
        healthbar.SetHealth(health);
    }
    public virtual void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            if(gameObject.GetComponent<Enemy>() != null)
			{
                gameObject.GetComponent<Enemy>().generateLoot();

            }
            Destroy(this.gameObject);
        }
        healthbar.SetHealth(health);
    }
    public void Heal(int amount)
    {
        // trigger shrine usage sound effect
        EventManager.TriggerOnUseShrine();
        health+=amount;
        healthbar.SetHealth(health);
    }
    
    public virtual void OnDestroy()
    {
        return;
    }
}
