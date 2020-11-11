﻿using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public new ItemData data;
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

    // Start is called before the first frame update
    public void Initialize()
    {
        this.scrollOffset = data.scrollOffset;
        this.transform.localScale = data.spriteScaling;
        this.scroll = data.scroll;
        this.relativeWeight = data.relativeWeight;  // How much should this item move when it is dropped
        this.sprite = data.sprite;              // This weapons resting sprite
        this.value = data.value;                // How much could this be sold for
        this.recipe = data.recipe;              // List of items that could be put together to make this item
        //this.sr = this.gameObject.AddComponent<SpriteRenderer>();      // Quick Reference to the Sprite Renderer attached to this object  
        this.itemName = data.itemName;          // Name of the ite
        this.description = data.description;    // description of the item

        //this.sr = this.GetComponent<SpriteRenderer>();
        //if (sr == null) sr = this.gameObject.AddComponent<SpriteRenderer>();
        //this.sr.sprite = data.sprite;
        //this.sr.sortingLayerName = "Player";
        this.gameObject.AddComponent<BoxCollider2D>();
    }

    //public void PickedUp()
    //{
    //    DungeonMaster.loot.Remove(this.gameObject);
    //    if (this.data.itemName == "Silver") { Camera.main.GetComponent<Inventory>().PickUpCoin(this.value); }
    //    else Camera.main.GetComponent<Inventory>().AddToInventory(this.data);
    //    Destroy(background);
    //    Destroy(text);
    //    Destroy(this.gameObject);
    //}
}