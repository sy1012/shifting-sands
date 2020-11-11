using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{
    private Sprite inventoryBackgroundSprite;
    private Sprite inventorySlotSprite;
    private GameObject inventoryBackground;
    private List<GameObject> inventory;
    private List<GameObject> slots;
    private GameObject coin;
    private GameObject coinText;
    private GameObject weaponText;
    private GameObject armourText;
    private Sprite coinSprite;
    private int coinAmount;
    private bool inventoryOpen;
    private bool mouseDown;
    public float slotWorldUnits;  // controls how large each slot of the inventory should be in world units
    private GameObject held;
    private Slot slotClicked;
    public Slot slotHovered;

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
        coinSprite = Resources.Load<Sprite>("Sprites/coin");
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

        slotWorldUnits = (screenUpperRight.x - screenLowerLeft.x) / 9;
        float slotScale = 1 / inventorySlotSprite.bounds.size.x * slotWorldUnits;
        float coinScale = (1 / coinSprite.bounds.size.x * (screenUpperRight.x - screenLowerLeft.x) / 9);

        // what Size is each block
        float BLOCKSIZE = 0.2f;
        float GRIDSIZE = (Camera.main.ViewportToWorldPoint(new Vector2(0, BLOCKSIZE)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y) * 2;

        // Make the two equipment slots and the coin display and text
        weaponText = Formatter.ScaleTextToPercentOfScreen("Equipped Weapon", 4, new Vector2(0.4f, 0.1f));
        weaponText.GetComponent<RectTransform>().SetParent(this.transform);
        weaponText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 2f * GRIDSIZE);
        weaponText.SetActive(false);

        armourText = Formatter.ScaleTextToPercentOfScreen("Equipped Armour", 4, new Vector2(0.4f, 0.1f));
        armourText.GetComponent<RectTransform>().SetParent(this.transform);
        armourText.GetComponent<RectTransform>().anchoredPosition = new Vector2(1 * GRIDSIZE, 2f * GRIDSIZE);
        armourText.SetActive(false);


        coin = new GameObject("Coin Inventory");
        coin.transform.localScale = new Vector2(coinScale, coinScale);
        coin.AddComponent<SpriteRenderer>().sprite = coinSprite;
        coin.AddComponent<RectTransform>().SetParent(this.transform);
        coin.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3 * GRIDSIZE, 1.5f * GRIDSIZE);
        coin.SetActive(false);
        coin.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        coin.GetComponent<SpriteRenderer>().sortingOrder = 16;
        slots.Add(coin);

        coinText = Formatter.ScaleTextToPercentOfScreen(coinAmount.ToString(), 8, new Vector2(0.2f, 0.2f));
        coinText.GetComponent<RectTransform>().SetParent(this.transform);
        coinText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3.6f * GRIDSIZE, .6f * GRIDSIZE);
        coinText.SetActive(false);

        GameObject slot = new GameObject("Weapon Inventory Slot");
        slot.transform.localScale = new Vector2(slotScale, slotScale);
        slot.AddComponent<SpriteRenderer>().sprite = inventorySlotSprite;
        slot.AddComponent<BoxCollider2D>().isTrigger = true;
        slot.AddComponent<RectTransform>().SetParent(this.transform);
        slot.AddComponent<Slot>();
        slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 1.5f * GRIDSIZE);
        slot.SetActive(false);
        slot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        slot.GetComponent<SpriteRenderer>().sortingOrder = 16;
        slots.Add(slot);

        slot = new GameObject("Armour Inventory Slot");
        slot.transform.localScale = new Vector2(slotScale, slotScale);
        slot.AddComponent<SpriteRenderer>().sprite = inventorySlotSprite;
        slot.AddComponent<BoxCollider2D>().isTrigger = true;
        slot.AddComponent<RectTransform>().SetParent(this.transform);
        slot.AddComponent<Slot>();
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
                slot.AddComponent<BoxCollider2D>().isTrigger = true;
                slot.AddComponent<RectTransform>().SetParent(this.transform);
                slot.AddComponent<Slot>();
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2((-numOfColumns + 4) * GRIDSIZE, (numOfRows - 2.5f) * GRIDSIZE);
                slot.SetActive(false);
                slot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                slot.GetComponent<SpriteRenderer>().sortingOrder = 16;
                slots.Add(slot);
                numOfColumns -= 1;
            }
            numOfRows -= 1;
        }
    }

    public void Update()
    {
        if (inventoryOpen)
        {
            if (Input.GetMouseButton(0))
            {
                if (held != null)
                {
                    Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    held.transform.position = mouse;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (slotClicked != null)
                {
                    slotClicked.Released();
                    if (slotHovered != null)
                    {
                        ItemData temp = slotHovered.RetrieveData();
                        slotHovered.AssignData(slotClicked.RetrieveData());
                        slotClicked.AssignData(temp);
                    }
                    slotClicked = null;
                    held= null;
                }           
            }

            //if (Input.GetMouseButtonDown(0))
            //{ 
            //    Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    int closest = 0;
            //    float closestDistance = 10000f;
            //    int i = 0;
            //    while (i < slots.Count - 1)
            //    {
            //        if (Mathf.Sqrt(Mathf.Pow(slots[i].transform.position.x - mouse.x, 2) + Mathf.Pow(slots[i].transform.position.y - mouse.y, 2)) < closestDistance)
            //        {
            //            closestDistance = Mathf.Sqrt(Mathf.Pow(slots[i].transform.position.x - mouse.x, 2) + Mathf.Pow(slots[i].transform.position.y - mouse.y, 2));
            //            closest = i;
            //        }
            //        i += 1;
            //    }
            //}
        }
    }

    public void SetHeld(Slot slot, GameObject item)
    {
        this.held = item;
        slotClicked = slot;
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
        coinText.SetActive(true);
        weaponText.SetActive(true);
        armourText.SetActive(true);
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
        coinText.SetActive(false);
        weaponText.SetActive(false);
        armourText.SetActive(false);
        inventoryBackground.SetActive(false);
        EventManager.TriggerOnCloseInventory();
    }

    public bool AddToInventory(ItemData data)
    {
        int i = 3;
        Debug.Log(slots[1]);
        while (i <= slots.Count-1 && slots[i].GetComponent<Slot>().occupied)
        {
            i += 1;
        }
        if (slots[i].GetComponent<Slot>().occupied) { return false; }
        else
        {
            slots[i].GetComponent<Slot>().AssignData(data);
            return true;
        }
    }

    public void RemoveFromInventory()
    {
        
    }

    public void PickUpCoin(int amount)
    {
        this.coinAmount += amount;
        coinText.GetComponent<TextMeshPro>().text = this.coinAmount.ToString();
    }
}
