using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
public class ConsumableData : ItemData
{
    public GameObject prefab;
    public int healAmount;
}
