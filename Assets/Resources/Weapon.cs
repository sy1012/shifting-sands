using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : ItemArchtype
{
    public WeaponData data;
    public ItemTypes.Type type;          // What type of item is this (hint its a weapon)

    private Sprite[] spriteAnimation;    // Animation to play when attacking
    private float speed;                 // Speed at which the weapon attacks in seconds
    private Vector2 hitBoxSize;          // The size of the hit box to be created when attacking
    private float coolDown;              // How long before we can swing again after completing a swing
    private int damage;                  // how much damage does it do upon hitting something
    private int currentFrame;            // If we are in the animation, what frame is it
    private new BoxCollider2D collider;  // Quick Reference to the collider attached to this object  

    // item needs to be set up but only after the Data has been added
    public override void Initialize()
    {
        // DEBUGGING
        //Inventory.AddToInventory();

        // set up all the initial values for this weapon
        this.scrollOffset = data.scrollOffset;
        this.transform.localScale = data.spriteScaling;
        this.relativeWeight = data.relativeWeight;
        this.description = data.description;
        this.spriteAnimation = data.spriteAnimation;
        this.sprite = data.sprite;
        this.speed = data.speed;
        this.hitBoxSize = data.hitBoxSize;
        this.value = data.value;
        this.recipe = data.recipe;
        this.sprite = data.sprite;
        this.damage = data.damage;
        this.itemName = data.name;
        this.scroll = data.scroll;
        this.coolDown = 0;
        this.currentFrame = 0;

        // set up the SpriteRenderer and BoxCollider2d
        this.sr = this.gameObject.AddComponent<SpriteRenderer>();
        this.sr.sortingLayerName = "Player";
        this.sr.sprite = sprite;
        this.collider = this.gameObject.AddComponent<BoxCollider2D>();
        this.collider.size = hitBoxSize;
        this.collider.offset = new Vector2(0, hitBoxSize.y / 2);
        this.collider.enabled = false;
    }

    void Update()
    {
        if (this.GetComponent<SpriteRenderer>() == null) { Initialize(); }
        if (this.coolDown > 0)
        {  
            this.coolDown -= Time.deltaTime;
            if (this.coolDown <= 0) // check if we are done the attack
            {
                this.collider.enabled = false;
                sr.sprite = sprite;
                this.coolDown = 0;
            }
            else if (this.currentFrame != Mathf.Round(((this.speed - this.coolDown) / this.speed) * (spriteAnimation.Length - 1))&&spriteAnimation.Length!=0)
            {
                currentFrame += 1;  //it could of only ever change upwards
                this.sr.sprite = spriteAnimation[currentFrame];
            }
        }
    }

    public void Attack()
    {
        if (this.coolDown <= 0)
        {
            this.collider.enabled = true;
            this.coolDown = speed;
            currentFrame = 0;
            Animator animator = transform.GetComponent<Animator>();
            if (spriteAnimation.Length != 0)
            {
                this.sr.sprite = spriteAnimation[0];
            }
            else if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Character>() != null) collision.collider.GetComponent<Character>().TakeDamage(this.damage,collision);
    }
}
