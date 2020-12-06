using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentBreakable : MonoBehaviour, IDamagable
{
    public EnviromentBreakableData data;

    private int quality;
    private int health;
    private Sprite sprite;
    private Sprite[] destructionAnimation;
    private Sprite[] beingHitAnimation;
    public Animator animator;
    private BoxCollider2D bc;

    void Start()
    {
        //TEMP WHILE SPRITES ARE TINY
        //this.transform.localScale = new Vector2(5, 5);

        quality = data.quality;
        health = data.health;
        sprite = data.sprite;
        destructionAnimation = data.destructionAnimation;
        beingHitAnimation = data.beingHitAnimation;
    }

    public virtual void OnDestroy()
    {
        return;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0) 
        {
            StartCoroutine(Break()); 
        }
    }

    IEnumerator Break()
    {
        if (animator != null)
        {
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<BoxCollider2D>());
            animator.enabled = true;
            yield return new WaitForSeconds(0.2f);
            LootGenerator.Generate(this.transform.position, quality);
            yield return new WaitForSeconds(1);
            Destroy(this.gameObject);
        }
    }
}
