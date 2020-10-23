using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private string itemName;             // Name of the item
    private bool initialized;            // The weapon has to be initialized before use 
    private TextMeshPro text;
    private GameObject background;
    public string description;           // description of the item

    private void Start()
    {
        initialized = false;
    }

    // item needs to be set up but only after the Data has been added
    void Initialize()
    {
        //TEMP FOR MILSTONE
        this.transform.localScale = new Vector2(4f, 4f);

        // make sure not to call this fn multiple times
        initialized = true;

        // set up all the initial values for this weapon
        this.description = data.description;
        this.description = this.description.Replace("NEWLINE", "\n");
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
        this.text = new GameObject("text").AddComponent<TextMeshPro>();
        this.text.gameObject.transform.SetParent(this.transform);
        this.background = new GameObject();
        this.background.transform.SetParent(this.transform);
        this.text.text = this.description;
        this.background.AddComponent<SpriteRenderer>().sprite = data.scroll;
        this.background.transform.localScale = new Vector2(.03f, .03f);
        this.background.SetActive(false);
        this.text.gameObject.SetActive(false);
        this.text.fontSize = 4;
        this.text.color = Color.black;
        this.text.alignment = TextAlignmentOptions.Midline;
        this.text.sortingOrder = 4;
        this.background.GetComponent<SpriteRenderer>().sortingOrder = 4;

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
        if (!initialized) { Initialize(); }
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Character>() != null) collision.collider.GetComponent<Character>().TakeDamage(this.damage);
    }

    public (GameObject, GameObject) Info()
    {
        if (!initialized) { Initialize(); }
        text.gameObject.SetActive(true);
        background.SetActive(true);
        return (text.gameObject, background);
    }
}
