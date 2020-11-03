using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool occupied;
    private ItemData data;
    private GameObject itemFrame;
    private GameObject item;
    private GameObject popUpHolder;

    public void OnMouseEnter()
    {
        //if (data != null)
        //{
        //    if (popUpHolder == null)
        //    {
        //        popUpHolder = new GameObject("popupholder");
        //        popUpHolder.AddComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        //        popUpHolder.transform.SetParent(this.transform);
        //        popUpHolder.AddComponent<Item>().data = data;
        //    }
        //    popUpHolder.GetComponent<Item>().CreateInfoPopUp();
        //}    
    }

    public void OnMouseExit()
    {
        //if (data != null)
        //{
        //    popUpHolder.GetComponent<Item>().DestroyInfoPopUp();
        //}
    }

    public (GameObject, GameObject) Clicked()
    {
        return (itemFrame, item);
    }
    
    public ItemData RetrieveData()
    {
        return this.data;
    }

    public void AssignData(ItemData data)
    {
        Destroy(itemFrame);
        Destroy(item);
        this.occupied = true;
        this.data = data;
        Vector2 slotPercent = Camera.main.WorldToViewportPoint(new Vector2(Camera.main.GetComponent<Inventory>().slotWorldUnits, Camera.main.GetComponent<Inventory>().slotWorldUnits))
            - Camera.main.WorldToViewportPoint(new Vector2(0, 0));
        item = Formatter.ScaleSpriteToPercentOfScreen(data.sprite, slotPercent, 18);
        item.transform.SetParent(this.transform);
        item.transform.position = this.transform.position;
        item.AddComponent<Item>().data = this.data;
       
        itemFrame = Formatter.ScaleSpriteToPercentOfScreen(data.frame, slotPercent, 17);
        itemFrame.transform.SetParent(this.transform);
        itemFrame.transform.position = this.transform.position;
    }

    public void RemoveItem()
    {
        this.occupied = false;
        this.data = null;
        Destroy(itemFrame);
        Destroy(item);
    }
}
