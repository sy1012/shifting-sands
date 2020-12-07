using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisBombs : MonoBehaviour
{
    public Transform player;
    public Sprite target;
    public Sprite[] explosion;
    public float timePerFrame;

    private int explosionFrame;
    private float timeSinceFrameChanged;
    private bool detonating;

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = target;
    }

    private void Update()
    {
        if (detonating)
        {
            // explosion is over
            if (timeSinceFrameChanged <= 0 && explosionFrame == explosion.Length - 2)
            {
                Destroy(gameObject);
            }
            if (timeSinceFrameChanged <= 0)
            {
                timeSinceFrameChanged = timePerFrame;
                explosionFrame += 1;
                this.GetComponent<SpriteRenderer>().sprite = explosion[explosionFrame];
            }
            timeSinceFrameChanged -= Time.deltaTime;
        }
    }

    public void Detonate()
    {
        this.gameObject.AddComponent<CircleCollider2D>();
        detonating = true;
        EventManager.TriggerOnAnubisAttackExplosion();
        this.GetComponent<SpriteRenderer>().sprite = explosion[explosionFrame];
        timeSinceFrameChanged = timePerFrame;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform == player)
        {
            player.GetComponent<PlayerStateMachine>().TakeDamage(10, collision);
        }
    }
}
