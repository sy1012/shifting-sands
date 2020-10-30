using System.Collections.Generic;
using UnityEngine;

public class Item : ItemArchtype
{
    public ItemData data;

    // Start is called before the first frame update
    public override void Initialize()
    {
        this.scrollOffset = data.scrollOffset;
        this.transform.localScale = data.spriteScaling;
        this.scroll = data.scroll;
        this.relativeWeight = data.relativeWeight;  // How much should this item move when it is dropped
        this.sprite = data.sprite;              // This weapons resting sprite
        this.value = data.value;                // How much could this be sold for
        this.recipe = data.recipe;              // List of items that could be put together to make this item
        this.sr = this.gameObject.AddComponent<SpriteRenderer>();      // Quick Reference to the Sprite Renderer attached to this object  
        this.itemName = data.itemName;          // Name of the ite
        this.description = data.description;    // description of the item

        this.gameObject.AddComponent<SpriteRenderer>();
        this.sr = this.GetComponent<SpriteRenderer>();
        this.sr.sprite = data.sprite;
        this.sr.sortingLayerName = "Player";
        this.gameObject.AddComponent<BoxCollider2D>();
    }
}