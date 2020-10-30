using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public Sprite sprite;             // this weapons resting sprite
    public ItemTypes.Type itemType = ItemTypes.Type.item;
    public string itemName;        // name of this item
    public int value;              // How much could this be sold for
    public List<ItemData> recipe;  // List of items that could be put together to make this item
    public Sprite scroll;          // What scroll should this item be displayed on
    public string description;     // description of the item
}
