using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

//base enemy class
public class Enemy : Character
{
    public int quality; // WHat tier are the drops from this enemy?
    public int damage;  // any Enemy inheriting this class should set these two values themselves
    public float damageSpeed; // note this is per second
    public float cooldown = 0;

    // Update is called once per frame
    private void LateUpdate()
    {
        if (cooldown >= 0) { cooldown -= Time.deltaTime; }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player" && cooldown <= 0)
        {
            cooldown = damageSpeed;
            collision.collider.gameObject.GetComponent<Character>().TakeDamage(damage);
        }
    }

    public void generateLoot()
	{
        LootGenerator.Generate(this.transform.position, quality);
    }
}
