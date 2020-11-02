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
    public static void Start()
    {
        // load the sprites and put them into their objects
        inventoryBackgroundSprite = Resources.Load<Sprite>("Sprites/InventoryBackground");
        inventorySlotSprite = Resources.Load<Sprite>("Sprites/InventorySlot");



        // Scale the images for this resolution
        

        EventManager.onOpenInventory += OpenInventory();
        EventManager.onCloseInventory += CloseInventory();
    }

    private static EventHandler OpenInventory()
    {
        // display the inventory sprite

        return null;
    }

    private static EventHandler CloseInventory()
    {
        return null;
    }

    private static void addToInventory()
    {

    }

    // Update is called once per frame
    public static void Update()
    {
        
    }
}
