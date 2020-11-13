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
  
    // Start is called before the first frame update
    public void Start()
    {
        //DontDestroyOnLoad(this);
        EventManager.onDungeonGenerated += DungeonEntered;
        EventManager.OnExitDungeon += DungeonExited;
        EventManager.onInventoryTrigger += TriggerInventory;

        // Set up initial state of the Inventory
        state = new InitialInventoryState();
        state.Enter();
        state = new OverworldInventoryState();
    }

    private void InventoryInteracted(object sender, EventArgs e)
    {
        state.Interact();
    }

    private void ChangeState(InventoryState newState)
    {
        state.Exit();
        state = newState;
        state.Enter();
    }

    private void InventorySlotsSwapped(Slot slotOne, Slot slotTwo)
    {
        state.Swapped(slotOne, slotTwo);
    }

    private void DungeonEntered(EventArgs e)
    {
        ChangeState(new DungeonState());
    }

    private void DungeonExited(EventArgs e)
    {
        ChangeState(new OverworldInventoryState());
    }

    public void Update()
    {
        if (state.IsOpen())
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
        }
    }

    public void SetHeld(Slot slot, GameObject item)
    {
        this.held = item;
        slotClicked = slot;
    }

    private void TriggerInventory(object sender, EventArgs e)
    {
        state.Triggered();
    }

    //private void OpenInventory()
    //{
    //    inventoryOpen = true;
    //    foreach (GameObject obj in slots)
    //    {
    //        obj.SetActive(true);
    //    }
    //    coinText.SetActive(true);
    //    weaponText.SetActive(true);
    //    armourText.SetActive(true);
    //    inventoryBackground.SetActive(true);
    //    EventManager.TriggerOnOpenInventory();
    //}

    //private void CloseInventory()
    //{
    //    inventoryOpen = false;
    //    foreach (GameObject obj in slots)
    //    {
    //        obj.SetActive(false);
    //    }
    //    coinText.SetActive(false);
    //    weaponText.SetActive(false);
    //    armourText.SetActive(false);
    //    inventoryBackground.SetActive(false);
    //    EventManager.TriggerOnCloseInventory();
    //}

    public bool AddToInventory(ItemData data)
    {
        return state.AddToInventory(data);
    }

    //public void RemoveFromInventory()
    //{

    //}

    public void PickUpCoin(int amount)
    {
        state.PickUpCoins(amount);
    }
}
