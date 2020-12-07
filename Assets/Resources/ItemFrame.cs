using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFrame : MonoBehaviour
{
    public ItemData data;
    private bool initialized = false;
    protected GameObject item;
    protected GameObject frame;
    protected GameObject text;
    private Vector2 originalPosition;


    public void HideInfo()
    {
        if (!initialized) { Initialize(); }
        if (text.GetComponent<Animator>() != null)
        {
            text.GetComponent<Animator>().SetBool("Open", false);
        }
        else
        {
            text.SetActive(false);
        }
    }

    public void ShowInfo()
    {
        if (!initialized) { Initialize(); }
        if (this.transform.parent.name == "Merchant Slot" && (this.data.itemName == "Max Potion" || (this.data.itemName == "Large Potion"))) {
            this.text.GetComponent<RectTransform>().position = (Vector2)this.transform.position - (Vector2)Camera.main.ViewportToScreenPoint(data.scrollOffset);
        }
        else
        {
            this.text.GetComponent<RectTransform>().position = (Vector2)this.transform.position + (Vector2)Camera.main.ViewportToScreenPoint(data.scrollOffset);
        }
        this.text.SetActive(true);
        if (text.GetComponent<Animator>() != null)
        {
            text.GetComponent<Animator>().SetBool("Open", true);
        }
        else
        {
            text.SetActive(true);
        }
    }

    public void ShowFrame()
    {
        if (!initialized) { Initialize(); }
        this.item.SetActive(true);
        this.frame.SetActive(true);
    }

    public void HideFrame()
    {
        if (!initialized) { Initialize(); }
        this.item.SetActive(false);
        this.frame.SetActive(false);
    }

    public GameObject Clicked()
    {
        this.HideInfo();
        Destroy(this.frame.GetComponent<RectTransform>());
        Destroy(this.item.GetComponent<RectTransform>());
        this.frame.GetComponent<SpriteRenderer>().sortingOrder += 2;
        this.item.GetComponent<SpriteRenderer>().sortingOrder += 2;
        return this.gameObject;
    }

    public void Released()
    {
        //Initialize();
        this.transform.position = this.transform.parent.GetComponent<RectTransform>().position;
        this.HideInfo();
    }

    // Set up all the various stuff needed
    void Initialize()
    {
        initialized = true;

        this.item = new GameObject();
        this.frame = new GameObject();
        this.text = Instantiate(data.display);
        this.text.SetActive(false);

        // the size the frame should be
        Vector2 slotPercent = Camera.main.WorldToViewportPoint(new Vector2(this.transform.parent.GetComponent<Slot>().slotWorldUnits / 3,
            this.transform.parent.GetComponent<Slot>().slotWorldUnits / 3)) - Camera.main.WorldToViewportPoint(new Vector2(0, 0));

        // Create the item sprite and the frame for it
        //(this.text, this.scroll) = Formatter.CreateAssetsFromScratchUI(this.data.description, this.data.scroll);
        //this.frame = Formatter.ScaleSpriteToPercentOfScreenUI(data.sprite, slotPercent, 17);
        this.item = Formatter.ScaleSpriteToPercentOfScreenUI(data.sprite, 8 * slotPercent, 18);
        this.item.name = ("Item");
        //this.frame.name = ("Frame");


        // Set up all the rect transforms
        //if (text.GetComponent<RectTransform>() == null) { text.AddComponent<RectTransform>(); }
        //if (scroll.GetComponent<RectTransform>() == null) { scroll.AddComponent<RectTransform>(); }
        if (item.GetComponent<RectTransform>() == null) { item.AddComponent<RectTransform>(); }
        //if (frame.GetComponent<RectTransform>() == null) { frame.AddComponent<RectTransform>(); }
        //this.scroll.GetComponent<RectTransform>().position = (Vector2)this.transform.position + data.scrollOffset * 1000;
        this.item.GetComponent<RectTransform>().position = (Vector2)this.transform.position;
        //this.frame.GetComponent<RectTransform>().position = (Vector2)this.transform.position;
        text.transform.SetParent(this.transform);
        
        Vector2 scaler = (Vector2)Camera.main.ViewportToScreenPoint(new Vector2(.002f, .002f));
        this.text.GetComponent<RectTransform>().localScale *= new Vector2(scaler.y, scaler.y);
        //scroll.transform.position = (Vector2)this.transform.position + data.scrollOffset;

        // Parent everything so that it is neat and tidy and the rect transforms function appriatley
        //scroll.transform.SetParent(this.transform);

        //frame.transform.SetParent(this.transform);
        item.transform.SetParent(this.transform);

        HideInfo();
    }
}
