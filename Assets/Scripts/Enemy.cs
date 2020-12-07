using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Pathfinding;

//base enemy class
public class Enemy : Character
{

    [SerializeField]
    protected AIDestinationSetter destination;
    [SerializeField]
    protected Transform Player;
    protected PlayerStateMachine psm;
    


    public int quality; // WHat tier are the drops from this enemy?
    public int damage;  // any Enemy inheriting this class should set these two values themselves
    public float damageSpeed; // note this is per second
    public float cooldown = 0;
    public float detectionRange; //the distance at which the enemy can spot the player
    public Room room;
    public Transform randTarget; //an invisible gameobject used to implement random patrol behaviour
    public GameObject deathEffect;  // Smoke when they die

    public void Start()
    {
        psm = FindObjectOfType<PlayerStateMachine>();
        Player = psm.transform;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        if (cooldown >= 0) { cooldown -= Time.deltaTime; }
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.transform == Player && cooldown <= 0)
        {
            cooldown = damageSpeed;
            psm.TakeDamage(damage,collision);
        }
    }

    public override void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            Debug.Log("overide successful");
            Die();
        }
        healthbar.SetHealth(health);
    }

    public virtual void Die()
    {
        // Loot Drops
        gameObject.GetComponent<Enemy>().generateLoot();

        // Create the death effect
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);

        // trigger explosion sounds`
        EventManager.TriggerOnEnemyDeath();

        // Destroy Effect
        Destroy(effect, 1f);

        // Destroy The game object
        Destroy(this.gameObject);

    }
    public void generateLoot()
	{
        LootGenerator.Generate(this.transform.position, quality);
    }
}
