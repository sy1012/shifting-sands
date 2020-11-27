using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
	public string[] inventoryItems;
	public string rune;
	public string weapon;
	public string armour;

	public int gold;

	public InventoryData(Inventory inventory, EquipmentManager equipment)
	{
		List<ItemData> items = inventory.GetInventoryList();
		inventoryItems = new string[items.Count];

		for(int i = 0; i < items.Count; i++)
		{
			if (items[i] != null)
			{
				inventoryItems[i] = items[i].itemName;
			}
		}

		if(equipment.GetArmour().data != null)
		{
			armour = equipment.GetArmour().data.itemName;
		}
		else
		{
			armour = null;
		}

		if (equipment.GetWeapon().data != null)
		{
			weapon = equipment.GetWeapon().data.itemName;
		}
		else
		{
			weapon = null;
		}

		if (equipment.GetRune().data != null)
		{
			rune = equipment.GetRune().data.itemName;
		}
		else
		{
			rune = null;
		}


		gold = inventory.GetCoinAmount();

	}
}
