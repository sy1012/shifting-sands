using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{

    InventoryState state;
    EquipmentManager equipment;
    private bool inDungeon;
    private bool inventoryOpen;
    private bool mouseDown;
    private GameObject held;
    private Slot slotClicked;
    public Slot slotHovered;
    private Slot hoveredPreviously;
    private bool displayed;  // is the info for the current item hovered displayed already
  
    // Start is called before the first frame update
    public void Start()
    {
        this.transform.position = Camera.main.transform.position;
        EventManager.onInventoryTrigger += TriggerInventory;
        EventManager.onWeaponMerchant += WeaponMerchantClicked;
        EventManager.onArmourMerchant += ArmourMerchantClicked;
        EventManager.onRuneMerchant += RuneMerchantClicked;
        EventManager.onCrafting += CraftingClicked;
        EventManager.onCloseInventory += SaveInventory;
        
        // Set up initial state of the Inventory
        state = new InitialInventoryState();
        state.Enter();
        state = new OverworldInventoryState();

        state.QuietPickUpCoins(300);

        LoadInventory();

    }
    private void CraftingClicked(object sender, EventArgs e)
    {
        ChangeState(new OverworldCraftingState());
    }

    private void RuneMerchantClicked(object sender, EventArgs e)
    {
        ChangeState(new OverworldRuneState());
    }

    private void ArmourMerchantClicked(object sender, EventArgs e)
    {
        ChangeState(new OverworldArmourState());
    }

    private void WeaponMerchantClicked(object sender, EventArgs e)
    {
        ChangeState(new OverworldWeaponState());
    }


    private void InventoryInteracted(object sender, EventArgs e)
    {
        state.Interact();
    }

    public void Interact()
    {
        state.Interact();
    }

    private void ChangeState(InventoryState newState)
    {
        if (state.ToString() == newState.ToString())
        {
            state.Triggered();
        }
        else
        {
            state.Exit();
            state = newState;
            state.Enter();
        }
    }

    private void InventorySlotsSwapped(Slot slotOne, Slot slotTwo)
    {
        state.Swapped(slotOne, slotTwo);
    }

    public void Update()
    {
        if (slotHovered != null && !displayed && held == null)
        {
            displayed = true;
            //slotHovered.ShowInfo();
            hoveredPreviously = slotHovered;
            slotHovered.transform.SetAsLastSibling();
        }
        else if (slotHovered == null && displayed)
        {
            displayed = false;
            //hoveredPreviously.HideInfo();
        }

        if (state.IsOpen())
        {
            if (Input.GetMouseButton(0))
            {
                if (held != null)
                {
                    held.transform.parent.transform.SetAsLastSibling();
                    held.transform.position = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (slotHovered != null && held != null)
                {
                    InventorySlotsSwapped(held.transform.parent.GetComponent<Slot>(), slotHovered);
                    held.transform.position = held.transform.parent.position;
                    held = null;
                    slotHovered = null;
                }
                else if (held != null)
                {
                    held.transform.position = held.transform.parent.position;
                    held = null;
                }
            }
        }
    }

    public void SetHeld(Slot slot, GameObject item)
    {
        this.held = item;
        slotClicked = slot;
    }

    private void TriggerInventory(object sender, EventArgs e)
    {
        ChangeState(new OverworldInventoryState());
    }

    public bool AddToInventory(ItemData data)
    {
        return state.AddToInventory(data);
    }

    //public void RemoveFromInventory()
    //{

    //}

    public void PickUpCoin(int amount)
    {
        // Updates the Coin Count on the HUD 
        state.PickUpCoins(amount);
    }

    public int GetCoinAmount()
    {
        return state.GetCoinAmount();
    }

    // Gets a list of the entire Inventory
    public List<ItemData> GetInventoryList()
    {
        List<ItemData> items = state.GetInventory();
        return items;
    }

    // Sets a list of the entire Inventory
    public bool SetInventoryList(List<ItemData> items)
    {
        if(state == null)
        {
            Debug.Log("bruh");
        }
        return state.SetInventory(items);
    }

    public void SaveInventory(object sender, EventArgs e)
	{
        SaveSystem.SaveInventory(this, FindObjectOfType<EquipmentManager>());
	}

    public void LoadInventory()
	{
        if (FindObjectOfType<MenuSelection>().load)
        {
            InventoryData data = SaveSystem.LoadInventory();
            ItemAccess itemAccess = FindObjectOfType<ItemAccess>();
            EquipmentManager equipment = FindObjectOfType<EquipmentManager>();

            List<ItemData> loadedItems = new List<ItemData>();

            for (int i = 0; i < data.inventoryItems.Length; i++)
            {
                if (data.inventoryItems[i] == null)
                {
                    loadedItems.Add(null);
                }
                else
                {
                    foreach (ItemData item in itemAccess.items)
                    {
                        if (item.itemName == data.inventoryItems[i])
                        {
                            loadedItems.Add(item);
                            break;
                        }
                    }
                }
            }

            SetInventoryList(loadedItems);

            if (data.armour != null)
            {
                foreach (ItemData item in itemAccess.items)
                {
                    if (item.itemName == data.armour)
                    {
                        equipment.SetArmour((ArmourData)item);
                        break;
                    }
                }
            }

            if (data.weapon != null)
            {
                foreach (ItemData item in itemAccess.items)
                {
                    if (item.itemName == data.weapon)
                    {
                        equipment.SetWeapon((WeaponData)item);
                        break;
                    }
                }
            }

            if (data.rune != null)
            {
                foreach (ItemData item in itemAccess.items)
                {
                    if (item.itemName == data.rune)
                    {
                        equipment.SetRune((RuneData)item);
                        break;
                    }
                }
            }

            state.QuietPickUpCoins(data.gold - GetCoinAmount());
        }
    }
}
