using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArchtype : MonoBehaviour, IItem
{
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
        Debug.Log("werong one");
    }

    public void PickedUp()
    {
        if (this.GetComponent<Rigidbody2D>() != null) Destroy(this.GetComponent<Rigidbody2D>());
        DungeonMaster.loot.Remove(this.gameObject);
        Destroy(background);
        Destroy(text);
        Destroy(this.gameObject);
        // ADD TO INVENTORY OR WHATEVER HERE
    }

    public void Dropped()
    {
        if (this.sr == null) { Initialize(); }
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

    public void CreateInfoPopUp()
    {
        if (this.sr == null) { Initialize(); }
        if (this.background == null) { (this.text, this.background) = ScrollTextFormatter.CreateAssetsFromScratch(this.description, this.scroll);  }
        else
        {
            text.gameObject.SetActive(true);
            background.SetActive(true);
        }
        text.transform.position = (Vector2)this.transform.position + scrollOffset;
        background.transform.position = (Vector2)this.transform.position + scrollOffset;
    }

    public void DestroyInfoPopUp()
    {
        if (this.sr == null) { Initialize(); }
        if (text != null)
        {
            text.gameObject.SetActive(false);
            background.SetActive(false);
        }
    }
}
