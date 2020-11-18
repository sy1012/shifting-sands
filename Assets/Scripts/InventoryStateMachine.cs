using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

static class PubData
{
    // Crafting System
    static public CraftingSystem craftingSystem;

    // two for equipment plus six others
    static public int numberOfDungeonSlots = 8;

    // these values are for the overworld map
    static public int slotsPerCamel = 10;
    static public int numberOfCamels = 1;

    static public Sprite inventoryBackgroundSprite;
    static public Sprite inventorySlotSprite;
    static public Sprite coinSprite;
    static public Sprite craftSprite;

    // background for everything inventory related
    static public GameObject inventoryBackground;

    // Items that are actually in the Inventory, the first two are the weapon and armour slots respectively
    // The first 5 other slots will be included in the dungeon inventory as well
    static public bool open; // is the inventory currently open

    static public EquipmentManager equipment = GameObject.Find("Equipment").GetComponent<EquipmentManager>();

    static public List<ItemData> armourMerchantInventory;
    static public List<ItemData> WeaponMerchantInventory;
    static public List<ItemData> RuneMerchantInventory;
    static public List<GameObject> merchantSlots;
    static public GameObject runeText;

    static public List<ItemData> inventory;
    static public GameObject coin;
    static public GameObject coinText;
    static public GameObject weaponSlot;
    static public GameObject armourSlot;
    static public GameObject runeSlot;
    static public GameObject inventoryText;
    static public GameObject craftingText;
    static public List<GameObject> slots;
    static public GameObject weaponText;
    static public GameObject armourText;
    static public GameObject runeEquippedText;
    static public GameObject weaponEquippedText;
    static public GameObject armourEquippedText;

    static public GameObject craftingSlotOne;
    static public GameObject craftingSlotTwo;
    static public GameObject craftingSlotThree;
    static public GameObject craftingSlotResult;
    static public GameObject craftingButton;
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

    public virtual void Swapped(Slot slotOne, Slot slotTwo)
    {
        return;
    }

    public virtual void Interact()
    {
        return;
    }

    public virtual void Triggered()
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
        PubData.coinText.GetComponent<TextMeshProUGUI>().text = PubData.coins.ToString();
    }

    public int GetCoinAmount()
    {
        return PubData.coins;
    }

    public bool Buy(int amount)
    {
        if (amount > PubData.coins) { return false; }
        PubData.coins -= amount;
        PubData.coinText.GetComponent<TextMeshProUGUI>().text = PubData.coins.ToString();
        return true;
    }

    public bool AddToInventory(ItemData item)
    {
        // we have room
        if (PubData.inventory.Count < PubData.slots.Count)
        {
            int i = 0;
            while (i < PubData.slots.Count && PubData.slots[i] != null) {  i += 1; }
            PubData.inventory.Add(item);
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
        // Set up the inventory state
        GameObject canvas = GameObject.Find("Canvas");
        PubData.open = false;

        PubData.craftingSystem = CraftingSystem.current;
        PubData.craftSprite = Resources.Load<Sprite>("Sprites/craft_icon");

        // initialize the variables needed
        PubData.equipment = GameObject.Find("Equipment").GetComponent<EquipmentManager>();

        Transform inventoryTransform = GameObject.Find("Inventory").transform;
        PubData.inventoryBackgroundSprite = Resources.Load<Sprite>("Sprites/InventoryBackground");
        PubData.inventorySlotSprite = Resources.Load<Sprite>("Sprites/InventorySlot");
        PubData.coinSprite = Resources.Load<Sprite>("Sprites/coin");

        // load in the merchants inventories
        PubData.RuneMerchantInventory = Resources.Load<ShopInventories>("RuneMerchant").items;
        PubData.WeaponMerchantInventory = Resources.Load<ShopInventories>("WeaponMerchant").items;
        PubData.armourMerchantInventory = Resources.Load<ShopInventories>("ArmourMerchant").items;

        // How large should the inventory be on the Viewport?
        const float VIEWPERCENT = 0.85f;

        //Initialize stuff
        PubData.slots = new List<GameObject>();
        PubData.inventory = new List<ItemData>();
        PubData.inventoryBackground = new GameObject("Inventory Back");
        PubData.inventoryBackground.AddComponent<Image>().sprite = PubData.inventoryBackgroundSprite;
        PubData.inventoryBackground.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.inventoryBackground.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.50f, 0.45f));
        PubData.inventoryBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(VIEWPERCENT * Camera.main.pixelWidth / PubData.inventoryBackgroundSprite.bounds.size.x,
            VIEWPERCENT * Camera.main.pixelHeight / PubData.inventoryBackgroundSprite.bounds.size.y);
        PubData.inventoryBackground.SetActive(false);

        // Scale the images for this resolution
        Vector2 screenUpperRight = Camera.main.ViewportToScreenPoint(new Vector2(VIEWPERCENT, VIEWPERCENT));
        Vector2 screenLowerLeft = Camera.main.ViewportToScreenPoint(new Vector2(1 - VIEWPERCENT, 1 - VIEWPERCENT));
        float xScale = PubData.inventoryBackgroundSprite.bounds.size.x;
        float yScale = PubData.inventoryBackgroundSprite.bounds.size.y;
        float worldUnitsWide = screenUpperRight.x - screenLowerLeft.x;
        float worldUnitsTall = screenUpperRight.y - screenLowerLeft.y;
        //PubData.inventoryBackground.transform.localScale = new Vector2(1 / xScale * worldUnitsWide, 1 / yScale * worldUnitsTall);

        float slotWorldUnits = (screenUpperRight.x - screenLowerLeft.x) / 330;
        float slotScale = 1 / PubData.inventorySlotSprite.bounds.size.x * slotWorldUnits;
        float coinScale = (1 / PubData.coinSprite.bounds.size.x * (screenUpperRight.x - screenLowerLeft.x) / 200);

        // what Size is each block
        float BLOCKSIZE = 0.1f;
        float GRIDSIZE = (Camera.main.ViewportToWorldPoint(new Vector2(0, BLOCKSIZE)).y - Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y) * 2;

        // Make stuff
        PubData.inventoryText = Formatter.ScaleTextToPercentOfScreenUI("Inventory", 20, Camera.main.ViewportToScreenPoint(new Vector2(0.3f, 0.1f)));
        PubData.inventoryText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.inventoryText.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, .81f));
        PubData.inventoryText.SetActive(false);

        PubData.craftingText = Formatter.ScaleTextToPercentOfScreenUI("Crafting", 20, Camera.main.ViewportToScreenPoint(new Vector2(0.3f, 0.1f)));
        PubData.craftingText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, .81f));
        PubData.craftingText.SetActive(false);

        PubData.runeEquippedText = Formatter.ScaleTextToPercentOfScreenUI("Equipped Rune", 10, Camera.main.ViewportToScreenPoint(new Vector2(0.15f, 0.05f)));
        PubData.runeEquippedText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.runeEquippedText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(.5f, 0.55f));
        PubData.runeEquippedText.SetActive(false);

        PubData.weaponEquippedText = Formatter.ScaleTextToPercentOfScreenUI("Equipped Weapon", 10, Camera.main.ViewportToScreenPoint(new Vector2(0.15f, 0.05f)));
        PubData.weaponEquippedText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.weaponEquippedText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(.3f,  0.55f));
        PubData.weaponEquippedText.SetActive(false);

        PubData.armourEquippedText = Formatter.ScaleTextToPercentOfScreenUI("Equipped Armour", 10, Camera.main.ViewportToScreenPoint(new Vector2(0.15f, 0.05f)));
        PubData.armourEquippedText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.armourEquippedText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(.7f , 0.55f));
        PubData.armourEquippedText.SetActive(false);

        PubData.coin = new GameObject("Coin Inventory");
        PubData.coin.transform.localScale = new Vector2(coinScale, coinScale);
        PubData.coin.AddComponent<Image>().sprite = PubData.coinSprite;
        PubData.coin.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.coin.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(new Vector2(.2f, .7f));
        PubData.coin.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
        PubData.coin.SetActive(false);

        PubData.coinText = Formatter.ScaleTextToPercentOfScreenUI(PubData.coins.ToString(), 8, Camera.main.ViewportToScreenPoint(new Vector2(0.15f, 0.05f)));
        PubData.coinText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.coinText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(.2f, .6f));
        PubData.coinText.SetActive(false);

        PubData.runeSlot = new GameObject("Rune Inventory Slot");
        PubData.runeSlot.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.runeSlot.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
        PubData.runeSlot.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.runeSlot.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.runeSlot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.runeSlot.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .7f));
        PubData.runeSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.runeSlot.SetActive(false);
        PubData.runeSlot.transform.position += new Vector3(0, 0, -8);

        PubData.weaponSlot = new GameObject("Weapon Inventory Slot");
        PubData.weaponSlot.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.weaponSlot.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
        PubData.weaponSlot.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.weaponSlot.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.weaponSlot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.weaponSlot.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(.3f, .7f));
        PubData.weaponSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.weaponSlot.SetActive(false);
        PubData.weaponSlot.transform.position += new Vector3(0, 0, -8);

        PubData.armourSlot = new GameObject("Armour Inventory Slot");
        PubData.armourSlot.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.armourSlot.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
        PubData.armourSlot.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.armourSlot.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.armourSlot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.armourSlot.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(.7f, .7f));
        PubData.armourSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.armourSlot.SetActive(false);
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
                slot.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
                slot.AddComponent<BoxCollider2D>().isTrigger = true;
                slot.GetComponent<RectTransform>().SetParent(inventoryTransform);
                slot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
                slot.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2( 0.9f + -0.1f * numOfColumns, -0.2f * numOfRows + 0.5f));
                slot.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
                slot.SetActive(false);
                slot.transform.position += new Vector3(0, 0, -8);
                PubData.slots.Add(slot);
                numOfColumns -= 1;
            }
            numOfRows -= 1;
        }

        // make the combinination specific stuff here
        PubData.craftingSlotOne = new GameObject("Crafting Slot One");
        PubData.craftingSlotOne.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotOne.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotOne.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotOne.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotOne.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotOne.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.2f, .7f));
        PubData.craftingSlotOne.SetActive(false);
        PubData.craftingSlotOne.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.craftingSlotOne.transform.position += new Vector3(0, 0, -8);

        PubData.craftingSlotTwo = new GameObject("Crafting Slot Two");
        PubData.craftingSlotTwo.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotTwo.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotTwo.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotTwo.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotTwo.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotTwo.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.3f, 0.6f));
        PubData.craftingSlotTwo.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.craftingSlotTwo.SetActive(false);
        PubData.craftingSlotTwo.transform.position += new Vector3(0, 0, -8);


        PubData.craftingSlotThree = new GameObject("Crafting Slot Three");
        PubData.craftingSlotThree.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotThree.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotThree.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotThree.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotThree.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotThree.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.4f, 0.7f));
        PubData.craftingSlotThree.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.craftingSlotThree.SetActive(false);
        PubData.craftingSlotThree.transform.position += new Vector3(0, 0, -8);


        PubData.craftingSlotResult = new GameObject("Crafting Slot Result");
        PubData.craftingSlotResult.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingSlotResult.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
        PubData.craftingSlotResult.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingSlotResult.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingSlotResult.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
        PubData.craftingSlotResult.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.7f, 0.65f));
        PubData.craftingSlotResult.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.craftingSlotResult.SetActive(false);
        PubData.craftingSlotResult.transform.position += new Vector3(0, 0, -8);


        PubData.craftingButton = new GameObject("Interact Button");
        PubData.craftingButton.transform.localScale = new Vector2(slotScale, slotScale);
        PubData.craftingButton.AddComponent<Image>().sprite = PubData.craftSprite;
        PubData.craftingButton.AddComponent<BoxCollider2D>().isTrigger = true;
        PubData.craftingButton.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.craftingButton.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(new Vector2(.55f, 0.6f));
        PubData.craftingButton.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
        PubData.craftingButton.SetActive(false);
        PubData.craftingButton.transform.position += new Vector3(0, 0, -8);


        PubData.craftingSlotsText = Formatter.ScaleTextToPercentOfScreenUI("Crafting Slots", 10, Camera.main.ViewportToScreenPoint(new Vector2(0.15f, 0.05f)));
        PubData.craftingSlotsText.transform.SetParent(inventoryTransform);
        PubData.craftingSlotsText.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.3f, 0.45f));
        PubData.craftingSlotsText.SetActive(false);

        PubData.craftingSlotResultText = Formatter.ScaleTextToPercentOfScreenUI("Crafting Result", 10, Camera.main.ViewportToScreenPoint(new Vector2(0.15f, 0.05f)));
        PubData.craftingSlotResultText.transform.SetParent(inventoryTransform);
        PubData.craftingSlotResultText.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.7f, 0.5f));
        PubData.craftingSlotResultText.SetActive(false);


        // Set up the rune state
        PubData.merchantSlots = new List<GameObject>();

        // Make the grid of Slots
        numOfRows = 2;
        while (numOfRows > 0)
        {
            int numOfColumns = 7;
            while (numOfColumns > 0)
            {
                slot = new GameObject("Merchant Slot");
                slot.transform.localScale = new Vector2(slotScale, slotScale);
                slot.AddComponent<Image>().sprite = PubData.inventorySlotSprite;
                slot.AddComponent<BoxCollider2D>().isTrigger = true;
                slot.GetComponent<RectTransform>().SetParent(inventoryTransform);
                slot.AddComponent<Slot>().slotWorldUnits = slotWorldUnits;
                slot.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.9f + -0.1f * numOfColumns, -0.17f * numOfRows + 0.9f));
                slot.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 4);
                slot.SetActive(false);
                slot.transform.position += new Vector3(0, 0, -8);
                PubData.merchantSlots.Add(slot);
                numOfColumns -= 1;
            }
            numOfRows -= 1;
        }

        PubData.weaponText = Formatter.ScaleTextToPercentOfScreenUI("Weapon Merchant", 20, Camera.main.ViewportToScreenPoint(new Vector2(0.3f, 0.1f)));
        PubData.weaponText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.weaponText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.48f, .83f));
        PubData.weaponText.SetActive(false);

        PubData.armourText = Formatter.ScaleTextToPercentOfScreenUI("Armour Merchant", 20, Camera.main.ViewportToScreenPoint(new Vector2(0.3f, 0.1f)));
        PubData.armourText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.armourText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.48f, .83f));
        PubData.armourText.SetActive(false);

        PubData.runeText = Formatter.ScaleTextToPercentOfScreenUI("Rune Merchant", 20, Camera.main.ViewportToScreenPoint(new Vector2(0.3f, 0.1f)));
        PubData.runeText.GetComponent<RectTransform>().SetParent(inventoryTransform);
        PubData.runeText.GetComponent<RectTransform>().localPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.48f, .83f));
        PubData.runeText.SetActive(false);

        // set up the intial values for the equipment
        if (PubData.equipment.GetWeapon() != null)
        {
            PubData.weaponSlot.GetComponent<Slot>().AssignData(PubData.equipment.GetWeapon().data);
        }

        if (PubData.equipment.GetArmour().data != null)
        {
            PubData.armourSlot.GetComponent<Slot>().AssignData(PubData.equipment.GetArmour().data);
        }

        if (PubData.equipment.GetRune().data != null)
        {
            PubData.armourSlot.GetComponent<Slot>().AssignData(PubData.equipment.GetRune().data);
        }

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

        if (PubData.equipment.GetArmour().data != null)
        {
            PubData.armourSlot.GetComponent<Slot>().AssignData(PubData.equipment.GetArmour().data);
        }

        if (PubData.equipment.GetRune().data != null)
        {
            PubData.runeSlot.GetComponent<Slot>().AssignData(PubData.equipment.GetRune().data);
        }

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(true);
        }
        PubData.runeSlot.SetActive(true);
        PubData.runeEquippedText.SetActive(true);
        PubData.inventoryBackground.SetActive(true);
        PubData.armourSlot.SetActive(true);
        PubData.weaponEquippedText.SetActive(true);
        PubData.armourEquippedText.SetActive(true);
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

        // assign the rune data
        if (PubData.runeSlot.GetComponent<Slot>().occupied)
        {
            PubData.equipment.SetRune((RuneData)PubData.runeSlot.GetComponent<Slot>().RetrieveData());
        }
        else PubData.equipment.SetRune(null);

        // assign the weapon data
        if (PubData.weaponSlot.GetComponent<Slot>().occupied)
        {
            PubData.equipment.SetWeapon((WeaponData)PubData.weaponSlot.GetComponent<Slot>().RetrieveData());
        }
        else PubData.equipment.SetWeapon(null);

        // assign the armour data
        if (PubData.armourSlot.GetComponent<Slot>().occupied)
        {
            PubData.equipment.SetArmour((ArmourData)PubData.armourSlot.GetComponent<Slot>().RetrieveData());
        }
        else PubData.equipment.SetArmour(null);

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(false);
        }

        PubData.runeSlot.SetActive(false);
        PubData.runeEquippedText.SetActive(false);
        PubData.inventoryBackground.SetActive(false);
        PubData.armourSlot.SetActive(false);
        PubData.weaponSlot.SetActive(false);
        PubData.weaponEquippedText.SetActive(false);
        PubData.armourEquippedText.SetActive(false);
        PubData.coin.SetActive(false);
        PubData.coinText.SetActive(false);
        PubData.inventoryText.SetActive(false);

        return;
    }

    public override void Interact()
    {
        if (PubData.weaponSlot.activeSelf) { Exit(); }
        else { Enter(); }

        return;
    }

    public override void Swapped(Slot slotOne, Slot slotTwo)
    {
        // if you put something into the equipment slots make sure it is the right type of item
        if (slotTwo.name == "Weapon Inventory Slot" && slotOne.RetrieveData().itemType != ItemTypes.Type.weapon) { return; }
        if (slotTwo.name == "Armour Inventory Slot" && slotOne.RetrieveData().itemType != ItemTypes.Type.armour) { return; }
        if (slotTwo.name == "Rune Inventory Slot" && slotOne.RetrieveData().itemType != ItemTypes.Type.rune) { return; }

        if (slotTwo.name == "Weapon Inventory Slot") { }
        else if (slotTwo.name == "Armour Inventory Slot") { }
        else if (slotTwo.name == "Rune Inventory Slot") { }
        else EventManager.TriggerOnInventorySwap();

        ItemData temp = slotOne.RetrieveData();
        slotOne.AssignData(slotTwo.RetrieveData());
        slotTwo.AssignData(temp);

        return;
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
        PubData.craftingButton.SetActive(true);
        PubData.craftingText.SetActive(true);
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
        PubData.craftingButton.SetActive(false);
        PubData.craftingText.SetActive(false);
        PubData.inventoryBackground.SetActive(false);
        PubData.craftingSlotOne.SetActive(false);
        PubData.craftingSlotTwo.SetActive(false);
        PubData.craftingSlotThree.SetActive(false);
        PubData.craftingSlotResult.SetActive(false);
        PubData.craftingSlotsText.SetActive(false);
        PubData.craftingSlotResultText.SetActive(false);

        return;
    }

    public override void Interact()
    {
        ItemData[] items = new ItemData[]{ PubData.craftingSlotOne.GetComponent<Slot>().RetrieveData(),
        PubData.craftingSlotTwo.GetComponent<Slot>().RetrieveData(), PubData.craftingSlotThree.GetComponent<Slot>().RetrieveData()};

        // If there is a match in the recipes
        ItemData item = PubData.craftingSystem.Craft(items);
        if (item != null)
        {
            PubData.craftingSlotResult.GetComponent<Slot>().AssignData(item);
            PubData.craftingSlotOne.GetComponent<Slot>().AssignData(null);
            PubData.craftingSlotTwo.GetComponent<Slot>().AssignData(null);
            PubData.craftingSlotThree.GetComponent<Slot>().AssignData(null);
        }

        return;
    }

    // allow two slots to be swapped as long as the only way a result can be swapped is if it is the first slot and the second is empty
    public override void Swapped(Slot slotOne, Slot slotTwo)
    {
        // invalid swap so just pass
        if (slotTwo.name == "Crafting Slot Result" || (slotOne.name == "Crafting Slot Result" && slotTwo.GetComponent<Slot>().RetrieveData( )!= null))
        {
            return;
        }

        EventManager.TriggerOnInventorySwap();
        ItemData temp = slotOne.RetrieveData();
        slotOne.AssignData(slotTwo.RetrieveData());
        slotTwo.AssignData(temp);

        return;
    }
}

// You are in the overworld and the player has the shop tab selected
class OverworldRuneState : InventoryState
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

        foreach (GameObject slot in PubData.merchantSlots)
        {
            slot.SetActive(true);
        }

        int i = 0;
        foreach (ItemData data in PubData.RuneMerchantInventory)
        {
            PubData.merchantSlots[i].GetComponent<Slot>().AssignData(data);
            i += 1;
        }
        
        PubData.runeText.SetActive(true);
        PubData.inventoryBackground.SetActive(true);

        return;
    }

    public override void Exit()
    {
        PubData.open = false;

        // Fire the event
        EventManager.TriggerOnCloseInventory();

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(false);
        }

        foreach (GameObject slot in PubData.merchantSlots)
        {
            slot.SetActive(false);
        }

        int i = 0;
        foreach (ItemData data in PubData.RuneMerchantInventory)
        {
            PubData.merchantSlots[i].GetComponent<Slot>().AssignData(null);
            i += 1;
        }

        PubData.runeText.SetActive(false);
        PubData.inventoryBackground.SetActive(false);
    }

    public override void Interact()
    {
        return;
    }

    public override void Swapped(Slot slotOne, Slot slotTwo)
    {
        // You are buying an item
        if (slotOne.gameObject.name == "Merchant Slot" && !slotTwo.occupied && slotTwo.name != "Merchant Slot")
        {
            // check that you have the money
            if (Buy(slotOne.RetrieveData().value))
            {
                EventManager.TriggerOnInventorySwap();
                slotTwo.GetComponent<Slot>().AssignData(slotOne.RetrieveData());
            }
        }

        // You are selling or moving an item
        else if (slotOne.gameObject.name == "Inventory Slot" && !slotTwo.occupied)
        {
            // if you are selling
            if (slotTwo.name == "Merchant Slot")
            {
                EventManager.TriggerOnCoinPickedUp();
                this.PickUpCoins(slotOne.RetrieveData().value);
            }
            slotOne.AssignData(null);
        }
    }
}

// You are in the overworld and the player has the shop tab selected
class OverworldWeaponState : InventoryState
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

        foreach (GameObject slot in PubData.merchantSlots)
        {
            slot.SetActive(true);
        }

        int i = 0;
        foreach (ItemData data in PubData.WeaponMerchantInventory)
        {
            PubData.merchantSlots[i].GetComponent<Slot>().AssignData(data);
            i += 1;
        }
        
        PubData.weaponText.SetActive(true);
        PubData.inventoryBackground.SetActive(true);

        return;
    }

    public override void Exit()
    {
        PubData.open = false;

        // Fire the event
        EventManager.TriggerOnCloseInventory();

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(false);
        }

        foreach (GameObject slot in PubData.merchantSlots)
        {
            slot.SetActive(false);
        }

        int i = 0;
        foreach (ItemData data in PubData.WeaponMerchantInventory)
        {
            PubData.merchantSlots[i].GetComponent<Slot>().AssignData(null);
            i += 1;
        }

        PubData.weaponText.SetActive(false);
        PubData.inventoryBackground.SetActive(false);
    }

    public override void Interact()
    {
        return;
    }

    public override void Swapped(Slot slotOne, Slot slotTwo)
    {
        // You are buying an item
        if (slotOne.gameObject.name == "Merchant Slot" && !slotTwo.occupied && slotTwo.name != "Merchant Slot")
        {
            // check that you have the money
            if (Buy(slotOne.RetrieveData().value))
            {
                EventManager.TriggerOnInventorySwap();
                slotTwo.GetComponent<Slot>().AssignData(slotOne.RetrieveData());
            }
        }

        // You are selling or moving an item
        else if (slotOne.gameObject.name == "Inventory Slot" && !slotTwo.occupied)
        {
            // if you are selling
            if (slotTwo.name == "Merchant Slot")
            {
                EventManager.TriggerOnCoinPickedUp();
                this.PickUpCoins(slotOne.RetrieveData().value);
            }
            slotOne.AssignData(null);
        }
    }
}

// You are in the overworld and the player has the shop tab selected
class OverworldArmourState : InventoryState
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

        foreach (GameObject slot in PubData.merchantSlots)
        {
            slot.SetActive(true);
        }

        int i = 0;
        foreach (ItemData data in PubData.armourMerchantInventory)
        {
            PubData.merchantSlots[i].GetComponent<Slot>().AssignData(data);
            i += 1;
        }

        PubData.armourText.SetActive(true);
        PubData.inventoryBackground.SetActive(true);

        return;
    }

    public override void Exit()
    {
        PubData.open = false;

        // Fire the event
        EventManager.TriggerOnCloseInventory();

        foreach (GameObject slot in PubData.slots)
        {
            slot.SetActive(false);
        }

        foreach (GameObject slot in PubData.merchantSlots)
        {
            slot.SetActive(false);
        }

        int i = 0;
        foreach (ItemData data in PubData.armourMerchantInventory)
        {
            PubData.merchantSlots[i].GetComponent<Slot>().AssignData(null);
            i += 1;
        }

        PubData.armourText.SetActive(false);
        PubData.inventoryBackground.SetActive(false);
    }

    public override void Interact()
    {
        return;
    }

    public override void Swapped(Slot slotOne, Slot slotTwo)
    {
        // You are buying an item
        if (slotOne.gameObject.name == "Merchant Slot" && !slotTwo.occupied && slotTwo.name != "Merchant Slot")
        {
            // check that you have the money
            if (Buy(slotOne.RetrieveData().value))
            {
                EventManager.TriggerOnInventorySwap();
                slotTwo.GetComponent<Slot>().AssignData(slotOne.RetrieveData());
            }
        }

        // You are selling or moving an item
        else if (slotOne.gameObject.name == "Inventory Slot" && !slotTwo.occupied)
        {
            // if you are selling
            if (slotTwo.name == "Merchant Slot")
            {
                EventManager.TriggerOnCoinPickedUp();
                this.PickUpCoins(slotOne.RetrieveData().value);
            }
            slotOne.AssignData(null);
        }
    }
}