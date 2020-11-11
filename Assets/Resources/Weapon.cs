using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public new WeaponData data;
    public ItemTypes.Type type;          // What type of item is this (hint its a weapon)


    //private Sprite[] spriteAnimation;  // Animation to play when attacking
    private float speed;                 // Speed at which the weapon attacks in seconds
    private Vector2 hitBoxSize;          // The size of the hit box to be created when attacking
    private float coolDown;              // How long before we can swing again after completing a swing
    private int damage;                  // how much damage does it do upon hitting something
    private new BoxCollider2D collider;  // Quick Reference to the collider attached to this object 
    protected Vector2 scrollOffset;
    protected float relativeWeight;
    protected Sprite sprite;               // This weapons resting sprite
    protected int value;                   // How much could this be sold for
    protected List<ItemData> recipe;       // List of items that could be put together to make this item
    protected SpriteRenderer sr;           // Quick Reference to the Sprite Renderer attached to this object  
    protected string itemName;             // Name of the item
    protected GameObject text;
    protected GameObject background;
    public string description;             // description of the item
    public Sprite scroll;

    // item needs to be set up but only after the Data has been added
    public void Initialize()
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

        // set up the SpriteRenderer, BoxCollider2d, and rigidbody
        
        if (this.GetComponent<BoxCollider2D>() != null) { this.collider = this.GetComponent<BoxCollider2D>(); }
        else { this.collider = this.gameObject.AddComponent<BoxCollider2D>(); }
        this.collider.size = hitBoxSize;
        this.collider.offset = new Vector2(0, hitBoxSize.y / 2);
        this.collider.enabled = false;
        this.collider.isTrigger = true;
        if (this.gameObject.GetComponent<Rigidbody2D>() != null) { this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0; }
        else this.gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (this.collider == null && this.data != null) { Initialize(); }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null && collision.gameObject.GetComponent<PlayerStateMachine>() == null) collision.gameObject.GetComponent<IDamagable>().TakeDamage(this.damage);
    }
}
