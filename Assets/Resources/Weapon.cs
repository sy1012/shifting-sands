using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : ItemArchtype
{
    public new WeaponData data;
    public ItemTypes.Type type;          // What type of item is this (hint its a weapon)


    //private Sprite[] spriteAnimation;    // Animation to play when attacking
    private float speed;                 // Speed at which the weapon attacks in seconds
    private Vector2 hitBoxSize;          // The size of the hit box to be created when attacking
    private float coolDown;              // How long before we can swing again after completing a swing
    private int damage;                  // how much damage does it do upon hitting something
<<<<<<< HEAD
    private int currentFrame;            // If we are in the animation, what frame is it
    private new BoxCollider2D collider;  // Quick Reference to the collider attached to this object  
    private Animator animator;
=======
    private new BoxCollider2D collider;  // Quick Reference to the collider attached to this object 
>>>>>>> Items

    // item needs to be set up but only after the Data has been added
    public override void Initialize()
    {
        // set up all the initial values for this weapon
        this.scrollOffset = data.scrollOffset;
        this.transform.localScale = data.spriteScaling;
        this.relativeWeight = data.relativeWeight;
        this.description = data.description;
        //this.spriteAnimation = data.spriteAnimation;
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
        if (this.transform.parent.GetComponent<Animator>() != null)
        {
            this.transform.parent.GetComponent<Animator>().SetInteger("Weapon Equipped", data.animationNumber);
            this.transform.parent.GetComponent<Animator>().speed = 1/speed;
        }

<<<<<<< HEAD
        // set up the SpriteRenderer, BoxCollider2d and Animator
        //this.animator = this.gameObject.AddComponent<Animator>();
        //this.sr = this.gameObject.AddComponent<SpriteRenderer>();
        //this.sr.sortingLayerName = "Player";
        //this.sr.sprite = sprite;
        this.collider = this.gameObject.AddComponent<BoxCollider2D>();
=======
        // set up the SpriteRenderer, BoxCollider2d, and rigidbody
        
        if (this.GetComponent<BoxCollider2D>() != null) { this.collider = this.GetComponent<BoxCollider2D>(); }
        else { this.collider = this.gameObject.AddComponent<BoxCollider2D>(); }
>>>>>>> Items
        this.collider.size = hitBoxSize;
        this.collider.offset = new Vector2(0, hitBoxSize.y / 2);
        this.collider.enabled = false;
        this.collider.isTrigger = true;
        if (this.gameObject.GetComponent<Rigidbody2D>() != null) { this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0; }
        else this.gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (this.collider == null) { Initialize(); }
        if (this.coolDown > 0)
        {
            this.coolDown -= Time.deltaTime;
            if (this.coolDown <= 0) // check if we are done the attack
            {
                this.collider.enabled = false;
                this.coolDown = 0;
            }
        }
    }

    public void Attack()
    {
        if (this.data == null) { return; }
        //if (this.collider == null) { Initialize(); }
        if (this.coolDown <= 0)
        {
            this.transform.parent.GetComponent<Animator>().SetTrigger("Attack");
            this.collider.enabled = true;
            this.coolDown = speed;
        }
    }

<<<<<<< HEAD
    public override void PickedUp()
    {
        if (this.GetComponent<Rigidbody2D>() != null) Destroy(this.GetComponent<Rigidbody2D>());
        DungeonMaster.loot.Remove(this.gameObject);
        Debug.Log(this.data);
        Camera.main.GetComponent<Inventory>().AddToInventory(this.data);
        Destroy(background);
        Destroy(text);
        Destroy(this.gameObject);
        // ADD TO INVENTORY OR WHATEVER HERE
    }
=======
>>>>>>> Items

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null && collision.gameObject.GetComponent<PlayerStateMachine>() == null) collision.gameObject.GetComponent<IDamagable>().TakeDamage(this.damage);
    }
}
