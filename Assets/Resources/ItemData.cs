using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public Vector2 scrollOffset;      // Offset for how much the scroll should be away form the player
    public Vector2 spriteScaling;
    public Sprite sprite;             // this weapons resting sprite
    public ItemTypes.Type itemType;   // Type of the item
    public string itemName;           // name of this item
    public int value;                 // How much could this be sold for
    public GameObject display;     // the text pop up describing the item
}
