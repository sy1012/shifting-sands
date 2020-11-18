using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SnowBall : MonoBehaviour
{
    // Hit effect for later
    public GameObject hitEffect;
    public int damage = 40;
    private int speed = 4;
    public AIPath AI;
    //public float timerSecond;
    GameObject target;
    private float duration = 2f;
    public float normalSpeed;
    public float timer = 0;
    private bool onHitAI;
    
    public void Update()
    {
        transform.position = transform.position + transform.right * speed * Time.deltaTime;

        // Timer
        if (onHitAI)
        {
            timer += 1 / duration * Time.deltaTime;
            

        }

        if (timer >= duration)
        {
            
            target.GetComponent<IAstarAI>().maxSpeed = normalSpeed;
            onHitAI = false;
            timer = 0;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Can access what u hit 4 health stuff
        target = collision.gameObject;
        if (target.GetComponent<IDamagable>() != null)
        {
            target.GetComponent<IDamagable>().TakeDamage(damage);
        }

        // Hit Effect
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        EventManager.TriggerOnSnowballCollison();

        // target.GetComponent<>
        if (target.GetComponent<IAstarAI>() != null)
        {
            normalSpeed = target.GetComponent<IAstarAI>().maxSpeed;
            target.GetComponent<IAstarAI>().maxSpeed = normalSpeed * 0.6f;
            onHitAI = true;
        }

        // Destroy Effect
        Destroy(effect, 1f);

        // Destroying the Fireball
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        // GetComponent<Rigidbody2D>().mass = 0;
        Destroy(gameObject, 4f);

    }
}
