using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private  Sprite inventoryBackgroundSprite;
    private  Sprite inventorySlotSprite;
    private  GameObject inventoryBackground;
    private  List<GameObject> inventory;
    private List<GameObject> slots;
    private  bool inventoryOpen;

    private static Inventory _current;
    public static Inventory current
    {
        get
        {
            if (_current == null) { _current = new Inventory(); }
            return _current;
        }
    }

    // Start is called before the first frame update
    public void Start()
    { 
        // How large should the inventory be on the Viewport?
        const float VIEWPERCENT = 0.95f;

        EventManager.onInventoryInteraction += TriggerInventory;

        //Initialize stuff
        slots = new List<GameObject>();
        inventory = new List<GameObject>();
        inventoryBackgroundSprite = Resources.Load<Sprite>("Sprites/InventoryBackground");
        inventorySlotSprite = Resources.Load<Sprite>("Sprites/InventorySlot");
        inventoryBackground = new GameObject("Inventory");
        inventoryBackground.AddComponent<SpriteRenderer>().sprite = inventoryBackgroundSprite;
        inventoryBackground.AddComponent<RectTransform>().SetParent(Camera.main.transform);
        inventoryBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        inventoryBackground.SetActive(false);
        inventoryBackground.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        inventoryBackground.GetComponent<SpriteRenderer>().sortingOrder = 15;

        inventoryOpen = false;

        // Scale the images for this resolution
        Vector2 screenUpperRight = Camera.main.ViewportToWorldPoint(new Vector2(VIEWPERCENT, VIEWPERCENT));
        Vector2 screenLowerLeft = Camera.main.ViewportToWorldPoint(new Vector2(1 - VIEWPERCENT, 1 - VIEWPERCENT));
        float xScale = inventoryBackgroundSprite.bounds.size.x;
        float yScale = inventoryBackgroundSprite.bounds.size.y;
        float worldUnitsWide = screenUpperRight.x - screenLowerLeft.x;
        float worldUnitsTall = screenUpperRight.y - screenLowerLeft.y;
        inventoryBackground.transform.localScale = new Vector2(1/xScale * worldUnitsWide, 1/yScale * worldUnitsTall);

        float slotScale = (1 / inventorySlotSprite.bounds.size.x * (screenUpperRight.x - screenLowerLeft.x) / 9);

        // what Size is each block
        const float BLOCKSIZE = 0.1f;
        float GRIDSIZE = (Camera.main.ViewportToWorldPoint(new Vector2(0, BLOCKSIZE)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y) * 2;

        // Make the two equipment slots
        GameObject slot = new GameObject("Weapon Inventory Slot");
        slot.transform.localScale = new Vector2(slotScale, slotScale);
        slot.AddComponent<SpriteRenderer>().sprite = inventorySlotSprite;
        slot.AddComponent<RectTransform>().SetParent(this.transform);
        slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 1.5f * GRIDSIZE);
        slot.SetActive(false);
        slot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        slot.GetComponent<SpriteRenderer>().sortingOrder = 16;
        slots.Add(slot);

        slot = new GameObject("Armour Inventory Slot");
        slot.transform.localScale = new Vector2(slotScale, slotScale);
        slot.AddComponent<SpriteRenderer>().sprite = inventorySlotSprite;
        slot.AddComponent<RectTransform>().SetParent(this.transform);
        slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(1 * GRIDSIZE, 1.5f * GRIDSIZE);
        slot.SetActive(false);
        slot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        slot.GetComponent<SpriteRenderer>().sortingOrder = 16;
        slots.Add(slot);

        // Make the grid of Slots
        int numOfRows = 2;
        while (numOfRows > 0)
        {
            int numOfColumns = 7;
            while (numOfColumns > 0)
            {
                slot = new GameObject("Inventory Slot");
                slot.transform.localScale = new Vector2(slotScale, slotScale);
                slot.AddComponent<SpriteRenderer>().sprite = inventorySlotSprite;
                slot.AddComponent<RectTransform>().SetParent(this.transform);
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2((numOfColumns - 4) * GRIDSIZE, (numOfRows-2) * GRIDSIZE);
                slot.SetActive(false);
                slot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                slot.GetComponent<SpriteRenderer>().sortingOrder = 16;
                slots.Add(slot);
                numOfColumns -= 1;
            }
            numOfRows -= 1;
        }
    }

    private void LateUpdate()
    {
        if (inventoryOpen)
        {
            //inventoryBackground.transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
        }
    }


    private void TriggerInventory(object sender, EventArgs e)
    {
        if (inventoryOpen) CloseInventory();
        else OpenInventory();
    }

    private void OpenInventory()
    {
        inventoryOpen = true;
        foreach (GameObject obj in slots)
        {
            obj.SetActive(true);
        }
        inventoryBackground.SetActive(true);
        EventManager.TriggerOnOpenInventory();
    }

    private void CloseInventory()
    {
        inventoryOpen = false;
        foreach (GameObject obj in slots)
        {
            obj.SetActive(false);
        }
        inventoryBackground.SetActive(false);
        EventManager.TriggerOnCloseInventory();
    }

    public void AddToInventory()
    {
        
    }

    public void RemoveFromInventory()
    {
        
    }
}
