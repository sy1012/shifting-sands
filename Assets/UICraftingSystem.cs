using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICraftingSystem : MonoBehaviour
{
    [SerializeField] private Transform prefabItem;
    private Transform[] inputSlots;
    private Transform outputSlot;
    private Transform itemContainer;

    private void Awake()
    {
        Transform gridContainer = transform.Find("gridContainer");
        Transform itemContainer = transform.Find("itemContainer");

        inputSlots = new Transform[CraftingSystem.maxIngredients];

        for (int i = 0; i < inputSlots.Length; i++)
        {
            inputSlots[i] = gridContainer.Find("input_"+i);
        }
        outputSlot = gridContainer.Find("output");
    }
    private void CreateItem(int index, Item item)
    {
        Transform itemTransform = Instantiate(prefabItem, itemContainer);
        RectTransform rectTransform = itemTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = inputSlots[index].GetComponent<RectTransform>().anchoredPosition;
        
    }
}
