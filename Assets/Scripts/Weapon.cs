using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData data;

    private Sprite[] spriteAnimation;    // Animation to play when attacking
    private Sprite sprite;               // This weapons resting sprite
    private float speed;                 // Speed at which the weapon attacks in seconds
    private Vector2 hitBoxSize;          // The size of the hit box to be created when attacking
    private float coolDown;              // How long before we can swing again after completing a swing
    private int damage;                  // how much damage does it do upon hitting something
    private int currentFrame;            // If we are in the animation, what frame is it
    private int value;                   // How much could this be sold for
    private List<ItemData> recipe;       // List of items that could be put together to make this item
    private SpriteRenderer sr;           // Quick Reference to the Sprite Renderer attached to this object  
    private new BoxCollider2D collider;  // Quick Reference to the collider attached to this object  
    private string itemName;                 // name of the item

    // Start is called before the first frame update
    void Start()
    {
        // set up all the initial values for this weapon
        this.spriteAnimation = data.spriteAnimation;
        this.sprite = data.sprite;
        this.speed = data.speed;
        this.hitBoxSize = data.hitBoxSize;
        this.value = data.value;
        this.recipe = data.recipe;
        this.sprite = data.sprite;
        this.damage = data.damage;
        this.itemName = data.name;
        this.coolDown = 0;
        this.currentFrame = 0;

        // set up the SpriteRenderer and BoxCollider2d
        this.sr = this.gameObject.AddComponent<SpriteRenderer>();
        this.sr.sprite = sprite;
        this.collider = this.gameObject.AddComponent<BoxCollider2D>();
        this.collider.size = hitBoxSize;
        this.collider.offset = new Vector2(hitBoxSize.x / 2, 0);
        this.collider.enabled = false;
    }

    void Update()
    {
        if (this.coolDown > 0)
        {  
            this.coolDown -= Time.deltaTime;
            if (this.coolDown <= 0) // check if we are done the attack
            {
                this.collider.enabled = false;
                sr.sprite = sprite;
                this.coolDown = 0;
            }
            else if (this.currentFrame != Mathf.Round(((this.speed - this.coolDown) / this.speed) * (spriteAnimation.Length - 1)))
            {
                Debug.Log(coolDown);
                Debug.Log((this.speed - this.coolDown) / this.coolDown);
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
            this.sr.sprite = spriteAnimation[0];
        }
    }
}
