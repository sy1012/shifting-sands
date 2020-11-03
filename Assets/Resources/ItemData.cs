using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public Vector2 scrollOffset;      // Offset for how much the scroll should be away form the player
    public Vector2 spriteScaling;
    public float relativeWeight;      // How far should they fall when being dropped
    public Sprite sprite;             // this weapons resting sprite
    public Sprite frame;              // this weapons resting sprite
    public ItemTypes.Type itemType;   // Type of the item
    public string itemName;           // name of this item
    public int value;                 // How much could this be sold for
    public List<ItemData> recipe;     // List of items that could be put together to make this item
    public Sprite scroll;             // What scroll should this item be displayed on
    public string description;        // description of the item
    private GameObject text;          // Couple gameObjects used to display info to the screen
    private GameObject background;
}
