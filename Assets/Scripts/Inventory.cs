using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Inventory
{
    private static Sprite inventoryBackgroundSprite;
    private static Sprite inventorySlotSprite;
    private static GameObject inventorySprite;
    private static List<GameObject> inventory;

    // Start is called before the first frame update
    public static void Initialize()
    { 
        // How large should the inventory be on the Viewport?
        const float VIEWPERCENT = 0.8f;

        // Scale the images for this resolution
        Debug.Log(Camera.main.orthographicSize);
        //Camera.main.ViewportToScreenPoint(VIEWPERCENT * Camera.main.orthographicSize * 2, VIEWPERCENT);

        EventManager.onOpenInventory += OpenInventory();
        EventManager.onCloseInventory += CloseInventory();

        //Initialize stuff
        inventory = new List<GameObject>();
        inventoryBackgroundSprite = Resources.Load<Sprite>("Sprites/InventoryBackground");
        inventorySlotSprite = Resources.Load<Sprite>("Sprites/InventorySlot");
    }

    private static EventHandler OpenInventory()
    {
        if (inventory == null) { Initialize(); }
        return null;
    }

    private static EventHandler CloseInventory()
    {
        if (inventory == null) { Initialize(); }
        return null;
    }

    public static void AddToInventory()
    {
        if (inventory == null) { Initialize(); }
    }

    public static void RemoveFromInventory()
    {
        if (inventory == null) { Initialize(); }
    }
}
