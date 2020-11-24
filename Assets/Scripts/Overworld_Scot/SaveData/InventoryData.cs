using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
	string[] inventoryItems;
	string rune;
	string weapon;
	string armour;

	int gold;

	public InventoryData(Inventory inventory, EquipmentManager equipment)
	{
		List<ItemData> items = inventory.GetInventoryList();
		inventoryItems = new string[items.Count];

		for(int i = 0; i < items.Count; i++)
		{
			inventoryItems[i] = items[i].itemName;
		}

		armour = equipment.GetArmour().data.itemName;
		weapon = equipment.GetWeapon().data.itemName;
		rune = equipment.GetRune().data.itemName;

		gold = inventory.GetCoinAmount();

	}
}
