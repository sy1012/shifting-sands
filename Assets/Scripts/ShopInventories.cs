using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Merchant Inventory", menuName = "Merchant Inventory")]
public class ShopInventories : ScriptableObject
{
    public List<ItemData> items;
}
