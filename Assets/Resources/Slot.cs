using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool occupied = false;
    //private ItemData data;
    //private GameObject itemFrame;
    private GameObject item;
    //private GameObject popUpHolder;

    public void OnMouseEnter()
    {
        Camera.main.GetComponent<Inventory>().slotHovered = this;
        if (occupied == true)
        {
            item.GetComponent<ItemFrame>().ShowInfo();
        }
    }

    public void OnMouseExit()
    {
        Camera.main.GetComponent<Inventory>().slotHovered = null;
        if (occupied == true)
        {
            item.GetComponent<ItemFrame>().HideInfo();
        }
    }

    public void OnMouseDown()
    {
        if (occupied == true)
        {
            Camera.main.GetComponent<Inventory>().SetHeld(this, item.GetComponent<ItemFrame>().Clicked());
        }
    }

    public void Released()
    {
        item.GetComponent<ItemFrame>().Released();
    }

    //public (GameObject, GameObject) Clicked()
    //{
    //    return (itemFrame, item);
    //}
    
    public ItemData RetrieveData()
    {
        if (this.item == null) { return null; }
        return this.item.GetComponent<ItemFrame>().data;
    }

    public void AssignData(ItemData data)
    {
        if (data != null)
        {
            this.occupied = true;
            Destroy(item);
            this.item = new GameObject("ItemFrame");
            this.item.transform.SetParent(this.transform);
            this.item.AddComponent<ItemFrame>().data = data;
            this.item.transform.position = this.transform.position;
            this.item.GetComponent<ItemFrame>().ShowFrame();
        }
        else
        {
            this.occupied = false;
            Destroy(item);
        }
        
        //Destroy(itemFrame);
        //Destroy(item);
        //this.occupied = true;
        //this.data = data;
        //Vector2 slotPercent = Camera.main.WorldToViewportPoint(new Vector2(Camera.main.GetComponent<Inventory>().slotWorldUnits, Camera.main.GetComponent<Inventory>().slotWorldUnits))
        //    - Camera.main.WorldToViewportPoint(new Vector2(0, 0));
        //item = Formatter.ScaleSpriteToPercentOfScreen(data.sprite, slotPercent, 18);
        //item.transform.SetParent(this.transform);
        //item.transform.position = this.transform.position;
        //item.AddComponent<ItemFrame>().data = this.data;
       
        //itemFrame = Formatter.ScaleSpriteToPercentOfScreen(data.frame, slotPercent, 17);
        //itemFrame.transform.SetParent(this.transform);
        //itemFrame.transform.position = this.transform.position;
    }

    public void RemoveItem()
    {
        //this.occupied = false;
        //this.data = null;
        //Destroy(itemFrame);
        //Destroy(item);
    }
}
