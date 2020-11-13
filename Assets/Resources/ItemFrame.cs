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
    protected GameObject scroll;
    private Vector2 originalPosition;


    public void HideInfo()
    {
        if (!initialized) { Initialize(); }
        text.SetActive(false);
        scroll.SetActive(false);
    }

    public void ShowInfo()
    {
        if (!initialized) { Initialize(); }
        this.text.GetComponent<RectTransform>().position = (Vector2)this.transform.position + data.scrollOffset;
        this.scroll.GetComponent<RectTransform>().position = (Vector2)this.transform.position + data.scrollOffset;
        text.SetActive(true);
        scroll.SetActive(true);
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
        //Destroy(this.frame.GetComponent<RectTransform>());
        //Destroy(this.item.GetComponent<RectTransform>());
        this.frame.GetComponent<SpriteRenderer>().sortingOrder += 2;
        this.item.GetComponent<SpriteRenderer>().sortingOrder += 2;
        return this.gameObject;
    }

    public void Released()
    {
        //Initialize();
        this.frame.GetComponent<SpriteRenderer>().sortingOrder -= 2;
        this.item.GetComponent<SpriteRenderer>().sortingOrder -= 2;
        this.transform.position = this.transform.parent.GetComponent<RectTransform>().position;
        this.HideInfo();
    }

    // Set up all the various stuff needed
    void Initialize()
    {
        initialized = true;

        // the size the frame should be
        //Vector2 slotPercent = Camera.main.WorldToViewportPoint(new Vector2(Camera.main.GetComponent<Inventory>().slotWorldUnits, Camera.main.GetComponent<Inventory>().slotWorldUnits))
        //    - Camera.main.WorldToViewportPoint(new Vector2(0, 0));
        Vector2 slotPercent = Camera.main.WorldToViewportPoint(new Vector2(this.transform.parent.GetComponent<Slot>().slotWorldUnits / data.frame.bounds.size.x,
            this.transform.parent.GetComponent<Slot>().slotWorldUnits / data.frame.bounds.size.x)) - Camera.main.WorldToViewportPoint(new Vector2(0, 0));
            //Camera.main.WorldToViewportPoint(this.gameObject.transform.parent.position) - Camera.main.WorldToViewportPoint(new Vector2(0, 0));

        // Create the item sprite and the frame for it
        (this.text, this.scroll) = Formatter.CreateAssetsFromScratch(this.data.description, this.data.scroll);
        this.item = Formatter.ScaleSpriteToPercentOfScreen(data.sprite, slotPercent, 18);
        this.frame = Formatter.ScaleSpriteToPercentOfScreen(data.frame, slotPercent, 17);
        this.item.name = ("Item");
        this.frame.name = ("Frame");


        // Set up all the rect transforms
        if (text.GetComponent<RectTransform>() == null) { text.AddComponent<RectTransform>(); }
        if (scroll.GetComponent<RectTransform>() == null) { scroll.AddComponent<RectTransform>(); }
        if (item.GetComponent<RectTransform>() == null) { item.AddComponent<RectTransform>(); }
        if (frame.GetComponent<RectTransform>() == null) { frame.AddComponent<RectTransform>(); }
        this.text.GetComponent<RectTransform>().anchoredPosition = (Vector2)this.transform.position + data.scrollOffset;
        this.scroll.GetComponent<RectTransform>().anchoredPosition = (Vector2)this.transform.position + data.scrollOffset;
        this.item.GetComponent<RectTransform>().anchoredPosition = (Vector2)this.transform.position;
        this.frame.GetComponent<RectTransform>().anchoredPosition = (Vector2)this.transform.position;

        text.transform.position = (Vector2)this.transform.position + data.scrollOffset;
        scroll.transform.position = (Vector2)this.transform.position + data.scrollOffset;

        // Parent everything so that it is neat and tidy and the rect transforms function appriatley
        item.transform.SetParent(this.transform);
        text.transform.SetParent(this.transform);
        scroll.transform.SetParent(this.transform);
        frame.transform.SetParent(this.transform);

        HideInfo();
    }
}
