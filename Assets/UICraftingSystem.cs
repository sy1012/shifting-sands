using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICraftingSystem : MonoBehaviour
{
    private Transform[] inputSlots;
    private Transform outputSlot;
    private Transform itemContainer;
    CraftingSystem craftingSystem;

    private void Awake()
    {
        //craftingSystem = new CraftingSystem();

        //Find transforms based on name in UI hierachy
        Transform slotContainer = transform.Find("SlotContainer");
        Transform itemContainer = transform.Find("ItemContainer");


        inputSlots = new Transform[CraftingSystem.maxIngredients];
        for (int i = 0; i < inputSlots.Length; i++)
        {
            inputSlots[i] = slotContainer.Find("In"+i);
        }

        outputSlot = slotContainer.Find("Out");
    }

    /// <summary>
    /// Remove an item from the crafting system
    /// </summary>
    /// <param name="index"></param>
    private void RemoveItem(int index) { craftingSystem.RemoveItem(index); }
    /// <summary>
    /// Places an item
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    private void PlaceItem(int index, Item item)
    {
        bool canAdd = craftingSystem.TryAddItem(item, index);
        if (canAdd)
        {
            Transform itemTransform = item.transform;
            RectTransform rectTransform = itemTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = inputSlots[index].GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
