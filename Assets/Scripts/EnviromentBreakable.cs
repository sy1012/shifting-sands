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
    private Animator animator;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    void Start()
    {
        //TEMP WHILE SPRITES ARE TINY
        this.transform.localScale = new Vector2(5, 5);

        sr = this.gameObject.AddComponent<SpriteRenderer>();
        animator = this.gameObject.AddComponent<Animator>();
        quality = data.quality;
        health = data.health;
        sprite = data.sprite;
        destructionAnimation = data.destructionAnimation;
        beingHitAnimation = data.beingHitAnimation;
        this.sr.sortingLayerName = "Player";
        this.sr.sprite = sprite;
        this.bc = this.gameObject.AddComponent<BoxCollider2D>();
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
            LootGenerator.Generate(this.transform.position, quality);
            Destroy(this.gameObject); 
        }
    }
}
