using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGenerator
{
    private static WeaponData woodSword;
    private static LootGenerator _current;
    public static LootGenerator current
    {
        get
        {
            if (_current == null) { _current = new LootGenerator(); Start(); }
            return _current;
        }
    }

    static void Start()
    {
        
        woodSword = Resources.Load<WeaponData>("Weapons/WoodenTrainingSword");
    }

    public void generate(Vector2 position)
    {
        // THIS IS A MOCKUP, ACTUAL ITEM GENERATION WONT BE THIS SIMPLE
        GameObject item = new GameObject("lootDrop");
        DungeonMaster.loot.Add(item);
        item.AddComponent<Weapon>().data = woodSword;
        item.transform.position = position;
    }
}
