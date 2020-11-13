using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class PubData
{
    // two for equipment plus six others
    static public int numberOfDungeonSlots = 8;

    // these values are for the overworld map
    static public int slotsPerCamel = 10;
    static public int numberOfCamels = 1;

    static public Sprite inventoryBackgroundSprite;
    static public Sprite inventorySlotSprite;
    static public Sprite coinSprite;

    // background for everything inventory related
    static public GameObject inventoryBackground;

    // Items that are actually in the Inventory, the first two are the weapon and armour slots respectively
    // The first 5 other slots will be included in the dungeon inventory as well
    static public bool open; // is the inventory currently open

    static public EquipmentManager equipment = GameObject.Find("Equipment").GetComponent<EquipmentManager>();

    static public List<ItemData> inventory;
    static public GameObject coin;
    static public GameObject coinText;
    static public GameObject weaponSlot;
    static public GameObject armourSlot;
    static public GameObject inventoryText;
    static public List<GameObject> slots;
    static public GameObject weaponText;
    static public GameObject armourText;

    static public GameObject craftingSlotOne;
    static public GameObject craftingSlotTwo;
    static public GameObject craftingSlotThree;
    static public GameObject craftingSlotResult;
    static public GameObject craftingSlotsText;
    static public GameObject craftingSlotResultText;

    static public List<GameObject> shopSlots;
    static public int coins;
}

abstract class InventoryState
{
    public bool IsOpen()
    {
        return PubData.open;
    }

    public virtual void Enter()
    {
        return;
    }

    public virtual IEnumerator Swapped(Slot slotOne, Slot slotTwo)
    {
        yield break;
    }

    public virtual IEnumerator Interact()
    {
        yield break;
    }

    public void Triggered()
    {
        if (PubData.open) { Exit(); }
        else { Enter(); }
    }

    public virtual void Exit()
    {
        return;
    }

    public void PickUpCoins(int amount)
    {
        PubData.coins += amount;
        this.Exit();  // refresh the inventory in case it changes something
        this.Enter();
    }

    public bool AddToInventory(ItemData item)
    {
        // we have room
        if (PubData.inventory.Count < PubData.slots.Count)
        {
            int i = 0;
            while (PubData.inventory[i] != null) {  i += 1; }
            PubData.inventory[i] = item;
            PubData.slots[i].GetComponent<Slot>().AssignData(item);
            return true;
        }
        // we dont have any more inventory space
        else return false;
    }
}

// This will prepare all the initial views and is only used at the start of the application.
class InitialInventoryState : InventoryState
{
    public override void Enter()
    {
        // initialize the variables needed
        PubData.equipment = GameObject.Find("Equipment").GetComponent<EquipmentManager>();

        Transform inventoryTransform = GameObject.Find("Inventory").transform;
        PubData.inventoryBackgroundSprite = Resources.Load<Sprite>("Sprites/InventoryBackground");
        PubData.inventorySlotSprite = Resources.Load<Sprite>("Sprites/InventorySlot");
        PubData.coinSprite = Resources.Load<Sprite>("Sprites/coin");

        // How large should the inventory be on the Viewport?
        const float VIEWPERCENT = 0.93f;

        //Initialize stuff
        PubData.slots = new List<GameObject>();
        PubData.inventory = new List<ItemData>();
        PubData.inventoryBackground = new GameObject("Inventory Back");
        PubData.inventoryBackground.AddComponent<SpriteRenderer>().sprite = PubData.inventoryBackgroundSprite;
        PubData.inventoryBackground.AddComponent<RectTransform>().SetParent(Camera.main.transform);
        PubData.inventoryBackground.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.5f, .45f));
        PubData.inventoryBackground.SetActive(false);
        PubData.inventoryBackground.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.inventoryBackground.GetComponent<SpriteRenderer>().sortingOrder = 15;

        // Scale the images for this resolution
        Vector2 screenUpperRight = Camera.main.ViewportToWorldPoint(new Vector2(VIEWPERCENT, VIEWPERCENT));
        Vector2 screenLowerLeft = Camera.main.ViewportToWorldPoint(new Vector2(1 - VIEWPERCENT, 1 - VIEWPERCENT));
        float xScale = PubData.inventoryBackgroundSprite.bounds.size.x;
        float yScale = PubData.inventoryBackgroundSprite.bounds.size.y;
        float worldUnitsWide = screenUpperRight.x - screenLowerLeft.x;
        float worldUnitsTall = screenUpperRight.y - screenLowerLeft.y;
        PubData.inventoryBackground.transform.localScale = new Vector2(1 / xScale * worldUnitsWide, 1 / yScale * worldUnitsTall);

        float slotWorldUnits = (screenUpperRight.x - screenLowerLeft.x) / 15;
        float slotScale = 1 / PubData.inventorySlotSprite.bounds.size.x * slotWorldUnits;
        float coinScale = (1 / PubData.coinSprite.bounds.size.x * (screenUpperRight.x - screenLowerLeft.x) / 9);

        // what Size is each block
        float BLOCKSIZE = 0.1f;
        float GRIDSIZE = (Camera.main.ViewportToWorldPoint(new Vector2(0, BLOCKSIZE)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y) * 2;

        // Make the two equipment slots and the coin display and text and the text saying Inventory
        PubData.inventoryText = Formatter.ScaleTextToPercentOfScreen("Inventory", 12, new Vector2(0.4f, 0.05f));
        PubData.inventoryText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.inventoryText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 0);
        PubData.inventoryText.SetActive(false);

        PubData.weaponText = Formatter.ScaleTextToPercentOfScreen("Equipped Weapon", 4, new Vector2(0.4f, 0.1f));
        PubData.weaponText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.weaponText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 2f * GRIDSIZE);
        PubData.weaponText.SetActive(false);

        PubData.armourText = Formatter.ScaleTextToPercentOfScreen("Equipped Armour", 4, new Vector2(0.4f, 0.1f));
        PubData.armourText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.armourText.GetComponent<RectTransform>().anchoredPosition = new Vector2(1 * GRIDSIZE, 2f * GRIDSIZE);
        PubData.armourText.SetActive(false);

        PubData.coin = new GameObject("Coin Inventory");
        PubData.coin.transform.localScale = new Vector2(coinScale, coinScale);
        PubData.coin.AddComponent<SpriteRenderer>().sprite = PubData.coinSprite;
        PubData.coin.AddComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.coin.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3 * GRIDSIZE, 1.5f * GRIDSIZE);
        PubData.coin.SetActive(false);
        PubData.coin.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.coin.GetComponent<SpriteRenderer>().sortingOrder = 16;

        PubData.coinText = Formatter.ScaleTextToPercentOfScreen(PubData.coins.ToString(), 8, new Vector2(0.2f, 0.2f));
        PubData.coinText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.coinText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3.6f * GRIDSIZE, .6f * GRIDSIZE);
        PubData.coinText.SetActive(false);

        PubData.weaponSlot = new GameObject("Weapon Inventory Slot");
        PubData.weaponSlot.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.weaponSlot.AddComponent<SpriteRenderer>().sprite = PubData.inventorySlotSprite;
        PubData.weaponSlot.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.weaponSlot.AddComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.weaponSlot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.weaponSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 1.5f * GRIDSIZE);
        PubData.weaponSlot.SetActive(false);
        PubData.weaponSlot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.weaponSlot.GetComponent<SpriteRenderer>().sortingOrder = 16;
        PubData.weaponSlot.transform.position += new Vector3(0, 0, -8);

        PubData.armourSlot = new GameObject("Armour Inventory Slot");
        PubData.armourSlot.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.armourSlot.AddComponent<SpriteRenderer>().sprite = PubData.inventorySlotSprite;
        PubData.armourSlot.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.armourSlot.AddComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.armourSlot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.armourSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(1 * GRIDSIZE, 1.5f * GRIDSIZE);
        PubData.armourSlot.SetActive(false);
        PubData.armourSlot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.armourSlot.GetComponent<SpriteRenderer>().sortingOrder = 16;
        PubData.armourSlot.transform.position += new Vector3(0, 0, -8);


        // Make the grid of Slots
        GameObject slot;
        int numOfRows = 2;
        while (numOfRows > 0)
        {
            int numOfColumns = 7;
            while (numOfColumns > 0)
            {
                slot = new GameObject("Inventory Slot");
                slot.transform.localScale = new Vector2(slotScale, slotScale);
                slot.AddComponent<SpriteRenderer>().sprite = PubData.inventorySlotSprite;
                slot.AddComponent<BoxCollider2D>().isTrigger = true;
                slot.AddComponent<RectTransform>().SetParent(inventoryTransform);
                slot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2((-numOfColumns + 4) * GRIDSIZE, (numOfRows - 2.5f) * GRIDSIZE);
                slot.SetActive(false);
                slot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                slot.GetComponent<SpriteRenderer>().sortingOrder = 16;
                slot.transform.position += new Vector3(0, 0, -8);
                PubData.slots.Add(slot);
                numOfColumns -= 1;
            }
            numOfRows -= 1;
        }

        // make the combinination specific stuff here
        PubData.craftingSlotOne = new GameObject("Crafting Slot One");
        PubData.craftingSlotOne.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotOne.AddComponent<SpriteRenderer>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotOne.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotOne.AddComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotOne.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotOne.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 1.5f * GRIDSIZE);
        PubData.craftingSlotOne.SetActive(false);
        PubData.craftingSlotOne.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.craftingSlotOne.GetComponent<SpriteRenderer>().sortingOrder = 16;
        PubData.craftingSlotOne.transform.position += new Vector3(0, 0, -8);

        PubData.craftingSlotTwo = new GameObject("Crafting Slot Two");
        PubData.craftingSlotTwo.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotTwo.AddComponent<SpriteRenderer>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotTwo.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotTwo.AddComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotTwo.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotTwo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-0.5f * GRIDSIZE, 1.2f * GRIDSIZE);
        PubData.craftingSlotTwo.SetActive(false);
        PubData.craftingSlotTwo.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.craftingSlotTwo.GetComponent<SpriteRenderer>().sortingOrder = 16;
        PubData.craftingSlotTwo.transform.position += new Vector3(0, 0, -8);


        PubData.craftingSlotThree = new GameObject("Crafting Slot Three");
        PubData.craftingSlotThree.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotThree.AddComponent<SpriteRenderer>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotThree.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotThree.AddComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotThree.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotThree.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 * GRIDSIZE, 1.5f * GRIDSIZE);
        PubData.craftingSlotThree.SetActive(false);
        PubData.craftingSlotThree.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.craftingSlotThree.GetComponent<SpriteRenderer>().sortingOrder = 16;
        PubData.craftingSlotThree.transform.position += new Vector3(0, 0, -8);


        PubData.craftingSlotResult = new GameObject("Crafting Slot Result");
        PubData.craftingSlotResult.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotResult.AddComponent<SpriteRenderer>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotResult.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotResult.AddComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotResult.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotResult.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * GRIDSIZE, 1.5f * GRIDSIZE);
        PubData.craftingSlotResult.SetActive(false);
        PubData.craftingSlotResult.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        PubData.craftingSlotResult.GetComponent<SpriteRenderer>().sortingOrder = 16;
        PubData.craftingSlotResult.transform.position += new Vector3(0, 0, -8);


        PubData.craftingSlotsText = new GameObject();


        PubData.craftingSlotResultText = new GameObject();


        PubData.open = false;

        return;
    }
}

// You are in the overworld and the player has the inventory tab selected
class OverworldInventoryState : InventoryState
{
    public override void Enter()
    {        
        // Fire the event
        EventManager.TriggerOnOpenInventory();
        
        // put the equipped item in the weapon and armour slots
        PubData.open = true;
        if (PubData.equipment.GetWeapon() != null){
            PubData.weaponSlot.GetComponent<Slot>().AssignData(PubData.equipment.GetWeapon().data);
        }
        

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(true);
        }
        PubData.inventoryBackground.SetActive(true);
        PubData.armourSlot.SetActive(true);
        PubData.weaponSlot.SetActive(true);
        PubData.coin.SetActive(true);
        PubData.coinText.SetActive(true);
        PubData.inventoryText.SetActive(true);

        return;
    }

    public override void Exit()
    {
        // Fire the event
        EventManager.TriggerOnCloseInventory();

        PubData.open = false;

        // assign the weapon data
        if (PubData.weaponSlot.GetComponent<Slot>().occupied)
        {
            PubData.equipment.SetWeapon((WeaponData)PubData.weaponSlot.GetComponent<Slot>().RetrieveData());
        }
        else PubData.equipment.SetWeapon(null);

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(false);
        }
        PubData.inventoryBackground.SetActive(false);
        PubData.armourSlot.SetActive(false);
        PubData.weaponSlot.SetActive(false);
        PubData.coin.SetActive(false);
        PubData.coinText.SetActive(false);
        PubData.inventoryText.SetActive(false);

        return;
    }

    public override IEnumerator Interact()
    {
        if (PubData.weaponSlot.activeSelf) { Exit(); }
        else { Enter(); }

        yield break;
    }

    public override IEnumerator Swapped(Slot slotOne, Slot slotTwo)
    {
        ItemData temp = slotOne.RetrieveData();
        slotOne.AssignData(slotTwo.RetrieveData());
        slotTwo.AssignData(temp);

        yield break;
    }
}

// You are in the overworld and the player has the crafting tab selected
class OverworldCraftingState : InventoryState
{
    public override void Enter()
    {
        // Fire the event
        EventManager.TriggerOnOpenInventory();

        PubData.open = true;

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(true);
        }
        PubData.inventoryBackground.SetActive(true);
        PubData.craftingSlotOne.SetActive(true);
        PubData.craftingSlotTwo.SetActive(true);
        PubData.craftingSlotThree.SetActive(true);
        PubData.craftingSlotResult.SetActive(true);
        PubData.craftingSlotsText.SetActive(true);
        PubData.craftingSlotResultText.SetActive(true);

        return;
    }

    public override void Exit()
    {
        // Fire the event
        EventManager.TriggerOnCloseInventory();

        PubData.open = false;

        // make sure first crafting slot is empty and put the data back where it belongs
        if (PubData.craftingSlotOne.GetComponent<Slot>().RetrieveData() != null)
        {
            int i = 0;
            while (PubData.inventory[i] != null) { i += 1; }
            PubData.slots[i].GetComponent<Slot>().AssignData(PubData.craftingSlotOne.GetComponent<Slot>().RetrieveData());
        }

        // make sure second crafting slot is empty and put the data back where it belongs
        if (PubData.craftingSlotTwo.GetComponent<Slot>().RetrieveData() != null)
        {
            int i = 0;
            while (PubData.inventory[i] != null) { i += 1; }
            PubData.slots[i].GetComponent<Slot>().AssignData(PubData.craftingSlotTwo.GetComponent<Slot>().RetrieveData());
        }

        // make sure third crafting slot is empty and put the data back where it belongs
        if (PubData.craftingSlotThree.GetComponent<Slot>().RetrieveData() != null)
        {
            int i = 0;
            while (PubData.inventory[i] != null) { i += 1; }
            PubData.slots[i].GetComponent<Slot>().AssignData(PubData.craftingSlotThree.GetComponent<Slot>().RetrieveData());
        }

        // make sure resulting crafting slot is empty and put the data back where it belongs
        if (PubData.craftingSlotResult.GetComponent<Slot>().RetrieveData() != null)
        {
            int i = 0;
            while (PubData.inventory[i] != null) { i += 1; }
            PubData.slots[i].GetComponent<Slot>().AssignData(PubData.craftingSlotResult.GetComponent<Slot>().RetrieveData());
            PubData.inventory[i] = PubData.craftingSlotResult.GetComponent<Slot>().RetrieveData(); ;
        }

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(false);
        }
        PubData.inventoryBackground.SetActive(false);
        PubData.craftingSlotOne.SetActive(false);
        PubData.craftingSlotTwo.SetActive(false);
        PubData.craftingSlotThree.SetActive(false);
        PubData.craftingSlotResult.SetActive(false);
        PubData.craftingSlotsText.SetActive(false);
        PubData.craftingSlotResultText.SetActive(false);

        return;
    }

    public override IEnumerator Interact()
    {
        // CALL TO SOME TYPE OF CRAFTING THING HERE

        yield break;
    }

    // allow two slots to be swapped as long as the only way a result can be swapped is if it is the first slot and the second is empty
    public override IEnumerator Swapped(Slot slotOne, Slot slotTwo)
    {
        // invalid swap so just pass
        if (slotTwo.name == "Crafting Slot Reult" || (slotOne.name == "Crafting Slot Result" && slotTwo.GetComponent<Slot>().RetrieveData( )!= null))
        {
            yield break;
        }

        ItemData temp = slotOne.RetrieveData();
        slotOne.AssignData(slotTwo.RetrieveData());
        slotTwo.AssignData(temp);

        yield break;
    }
}

// You are in the overworld and the player has the shop tab selected
class OverworldShopingState : InventoryState
{
    public override void Enter()
    {
        return;
    }

    public override void Exit()
    {
        return;
    }

    public override IEnumerator Interact()
    {
        yield break;
    }

    public override IEnumerator Swapped(Slot slotOne, Slot slotTwo)
    {
        yield break;
    }
}

// You are in a dungeon
class DungeonState : InventoryState
{
    public override void Enter()
    {
        // Fire the event
        EventManager.TriggerOnOpenInventory();

        PubData.open = true;

        int i = 0;
        while (i <= 7)
        {
            PubData.slots[i].SetActive(true);
            i += 1;
        }
        PubData.armourSlot.SetActive(true);
        PubData.weaponSlot.SetActive(true);
        PubData.coin.SetActive(true);
        PubData.coinText.SetActive(true);
        PubData.inventoryText.SetActive(true);

        return;
    }

    public override void Exit()
    {
        // Fire the event
        EventManager.TriggerOnCloseInventory();

        PubData.open = false;

        int i = 0;
        while (i <= 7)
        {
            PubData.slots[i].SetActive(false);
            i += 1;
        }
        PubData.armourSlot.SetActive(false);
        PubData.weaponSlot.SetActive(false);
        PubData.coin.SetActive(false);
        PubData.coinText.SetActive(false);
        PubData.inventoryText.SetActive(false);

        return;
    }

    public override IEnumerator Interact()
    {
        // DROP ITEM HERE

        yield break;
    }

    public override IEnumerator Swapped(Slot slotOne, Slot slotTwo)
    {
        ItemData temp = slotOne.RetrieveData();
        slotOne.AssignData(slotTwo.RetrieveData());
        slotTwo.AssignData(temp);

        yield break;
    }
}