using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArchtype : MonoBehaviour, IItem
{
    public ItemData data;
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
    public virtual void Initialize()
    {
        //this.scrollOffset = data.scrollOffset;
        //this.transform.localScale = data.spriteScaling;
        //this.scroll = data.scroll;
        //this.relativeWeight = data.relativeWeight;  // How much should this item move when it is dropped
        //this.sprite = data.sprite;              // This weapons resting sprite
        //this.value = data.value;                // How much could this be sold for
        //this.recipe = data.recipe;              // List of items that could be put together to make this item
        ////this.sr = this.gameObject.AddComponent<SpriteRenderer>();      // Quick Reference to the Sprite Renderer attached to this object  
        //this.itemName = data.itemName;          // Name of the ite
        //this.description = data.description;    // description of the item

        ////this.sr = this.GetComponent<SpriteRenderer>();
        ////if (sr == null) sr = this.gameObject.AddComponent<SpriteRenderer>();
        ////this.sr.sprite = data.sprite;
        ////this.sr.sortingLayerName = "Player";
        //this.gameObject.AddComponent<BoxCollider2D>();
    }

    public void PickedUp()
    {
        //if (this.GetComponent<Rigidbody2D>() != null) Destroy(this.GetComponent<Rigidbody2D>());
        DungeonMaster.loot.Remove(this.gameObject);
        if (this.data.itemName != "Silver") GameObject.Find("Inventory").GetComponent<Inventory>().AddToInventory(this.data);
        else GameObject.Find("Inventory").GetComponent<Inventory>().PickUpCoin(this.data.value);

        Destroy(this.gameObject);
    }

    public void Dropped()
    {
        Debug.Log(data.name);
        this.sr = this.gameObject.AddComponent<SpriteRenderer>();
        this.sr.sprite = data.sprite;
        this.transform.localScale = this.data.spriteScaling;
        this.sr.sortingLayerName = "Player";
        this.gameObject.AddComponent<BoxCollider2D>();
        if (this.GetComponent<Rigidbody2D>() == null) this.gameObject.AddComponent<Rigidbody2D>();
        Vector2 force = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
        this.GetComponent<Rigidbody2D>().AddForce(force);
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.GetComponent<Rigidbody2D>().drag = relativeWeight;
    }

    public void Sold()
    {
        throw new System.NotImplementedException();
    }

    public virtual void CreateInfoPopUp()
    {
        DungeonMaster.loot.Remove(this.gameObject);
        if (this.data.itemName != "Silver") GameObject.Find("Inventory").GetComponent<Inventory>().AddToInventory(this.data);
        else GameObject.Find("Inventory").GetComponent<Inventory>().PickUpCoin(this.data.value);

        Destroy(this.gameObject);
        ////if (this.sprite == null) { Initialize(); }
        //if (this.background == null) { (this.text, this.background) = Formatter.CreateAssetsFromScratch(data.description, data.scroll); }
        //text.transform.SetParent(this.transform);
        //background.transform.SetParent(this.transform);
        //text.gameObject.SetActive(true);
        //background.SetActive(true);
        ////if (this.GetComponent<RectTransform>() != null)
        ////{
        ////    if (background.GetComponent<RectTransform>() == null) { background.AddComponent <RectTransform>(); }
        ////    text.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition + scrollOffset;
        ////    background.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition + scrollOffset;
        ////}
        //text.transform.position = (Vector2)this.transform.position + scrollOffset;
        //background.transform.position = (Vector2)this.transform.position + scrollOffset;
    }

    public void DestroyInfoPopUp()
    {
        //if (this.sr == null) { Initialize(); }
        //if (text != null)
        //{
        //    text.gameObject.SetActive(false);
        //    background.SetActive(false);
        //}
    }
}
