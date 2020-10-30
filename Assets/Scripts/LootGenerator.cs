using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGenerator
{
    private static WeaponData woodSword;
    private static ItemData gold;

    private static List<float> dropQuality0;
    private static List<float> dropQuality1;

    private static List<ItemData> thisTierCommon = new List<ItemData>();
    private static List<ItemData> thisTierUncommon = new List<ItemData>();
    private static List<ItemData> thisTierRare = new List<ItemData>();
    private static List<ItemData> thisTierExotic = new List<ItemData>();
    private static List<ItemData> firstTierCommon = new List<ItemData>();
    private static List<ItemData> firstTierUncommon = new List<ItemData>();
    private static List<ItemData> firstTierRare = new List<ItemData>();
    private static List<ItemData> firstTierExotic = new List<ItemData>();

    private static List<float> dropRate0;
    private static List<float> dropRate1;

    private static int dungeonLevel = 0;

    static void Start()
    {
        /* These variables control what rarity an item is for any tier it is present in, (tier, rarity) */
        List<(int, int)> woodSwords = new List<(int, int)> { (1, 2), (2, 2) };
        List<(int, int)> gold1 = new List<(int, int)> { (1, 1), (2, 1)};
        List<(int, int)> gold5 = new List<(int, int)> { (1, 2), (2, 1)};

        /* These variables are used to control drop chances of classes of items for a particular quality */
        dropQuality0 = new List<float> { 0.0f, 0.8f };
        dropQuality1 = new List<float> { 0.0f, 0.2f, 0.7f };

        /* These variables are used to control drop numbers for a particular quality */
        dropRate0 = new List<float> { 0.0f, 0.8f };
        dropRate1 = new List<float> { 0.0f, 0.2f, 0.7f };

        EventManager.onEnteringDungeon += (object sender, EventManager.onEnteringDungeonEventArgs e) => {
            dungeonLevel = e.dungeonLevel;

            if (dungeonLevel == 1)
            {
                thisTierCommon = firstTierCommon;
                thisTierUncommon = firstTierUncommon;
                thisTierRare = firstTierRare;
                thisTierExotic = firstTierExotic;
            }
            else { Debug.Log("NOT IMPLEMENTED YET"); }
        };

        woodSword = Resources.Load<WeaponData>("Weapons/WoodenTrainingSword");
        gold = Resources.Load<ItemData>("Items/GoldPiece");

        /* set up the tier Lists */
        firstTierUncommon.Add(woodSword);
        firstTierCommon.Add(gold);
        thisTierCommon.Add(gold);
        thisTierUncommon.Add(woodSword);
    }

    private static List<ItemData> GenerateDropsList(int quality)
    {
        List<ItemData> dropped = new List<ItemData>();
        List<float> dropQuality = new List<float>();  // REMOVE THIS ONCE ALL DROP QUALITIES HAVE BEEN CREATED AND HANDLED
        List<float> dropRate = new List<float>();

        // What quality of items will we be pulling from
        if (quality == 0) { dropQuality = dropQuality0; dropRate = dropRate0; }
        else if (quality == 1) { dropQuality = dropQuality1; dropRate = dropRate1; }

        // figure out how many items should drop
        float randomNumberOfDrops = Random.Range(0f, 1f);
        int count = 0;
        while (dropRate.Count-1 >= count && randomNumberOfDrops >= dropRate[count]) { count += 1; }  
        int numberOfDrops = count;

        // figure out what exact items will drop
        while (numberOfDrops > 0)
        {
            numberOfDrops -= 1;
            float qualityReached = Random.Range(0f, 1f);
            int counter = 0;
            while (dropQuality.Count-1 >= counter && qualityReached >= dropQuality[counter]) { counter += 1; }
            ItemData item;
            if (counter == 1) { item = thisTierCommon[Random.Range(0, thisTierCommon.Count - 1)]; }
            else if (counter == 2) { item = thisTierUncommon[Random.Range(0, thisTierUncommon.Count - 1)]; }
            else if (counter == 3) { item = thisTierRare[Random.Range(0, thisTierRare.Count - 1)]; }
            else { item = thisTierExotic[Random.Range(0, thisTierExotic.Count - 1)]; }
            dropped.Add(item);  
        }

        return dropped;
    }

    private static void InstantiateDrops(Vector2 position, List<ItemData> items)
    {
        foreach (ItemData item in items)
        {
            GameObject drop = new GameObject("lootDrop");
            //GameObject.Destroy(drop.GetComponent<SpriteRenderer>());
            DungeonMaster.loot.Add(drop);
            if (item.itemType is ItemTypes.Type.weapon) { drop.AddComponent<Weapon>().data = (WeaponData)item; drop.GetComponent<Weapon>().Dropped(); }
            else if (item.itemType is ItemTypes.Type.consumable) { } // TODO
            else if (item.itemType is ItemTypes.Type.item) { drop.AddComponent<Item>().data = item; drop.GetComponent<Item>().Dropped(); }
            drop.transform.position = position;
        }
    }

    public static void Generate(Vector2 position, int quality)
    {
        Start();
        List<ItemData> items = GenerateDropsList(quality);
        InstantiateDrops(position, items);
    }
}
