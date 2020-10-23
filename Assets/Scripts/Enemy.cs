using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

//base enemy class
public class Enemy : Character
{
    public int damage; // any Enemy inheriting this class should set these two values themselves
    public float damageSpeed; // note this is per second
    public float cooldown = 0;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            LootGenerator.current.generate(this.transform.position);
        }

        if (cooldown >= 0) { cooldown -= Time.deltaTime; }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player" && cooldown <= 0)
        {
            cooldown = damageSpeed;
            collision.collider.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
    }
}
