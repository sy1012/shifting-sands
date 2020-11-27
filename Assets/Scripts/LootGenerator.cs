using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LootGenerator
{
    private static int initialized = 1;
    private static RuneData patheticFire;
    private static RuneData patheticEarth;
    private static RuneData patheticWater;
    private static RuneData patheticAir;
    private static ItemData silver;
    private static ItemData silverx2;
    private static ItemData silverx3;
    private static ItemData silverx4;
    private static ItemData silverx5;
    private static ItemData silverx10;

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
        initialized = 0;

        /* These variables control what rarity an item is for any tier it is present in, (tier, rarity) */
        List<(int, int)> woodSwords = new List<(int, int)> { (1, 2), (2, 2) };
        List<(int, int)> silver1 = new List<(int, int)> { (1, 1), (2, 1)};
        List<(int, int)> gold5 = new List<(int, int)> { (1, 2), (2, 1)};
        List<(int, int)> sticks = new List<(int, int)> { (1, 2), (2, 1) };

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
        
        silver = Resources.Load<ItemData>("Items/SilverPiece");
        silverx2 = Resources.Load<ItemData>("Items/SilverPiecex2");
        silverx3 = Resources.Load<ItemData>("Items/SilverPiecex3");
        silverx4 = Resources.Load<ItemData>("Items/SilverPiecex4");
        silverx5 = Resources.Load<ItemData>("Items/SilverPiecex5");
        silverx10 = Resources.Load<ItemData>("Items/SilverPiecex10");
        patheticFire = Resources.Load<RuneData>("Runes/patheticFireRune");
        patheticEarth = Resources.Load<RuneData>("Runes/patheticEarthRune");
        patheticAir = Resources.Load<RuneData>("Runes/patheticAirRune");
        patheticWater = Resources.Load<RuneData>("Runes/patheticWaterRune");

        /* set up the tier Lists */
        thisTierCommon.Add(silver);
        thisTierCommon.Add(silverx2);
        thisTierUncommon.Add(silver);
        thisTierUncommon.Add(silverx2);
        thisTierUncommon.Add(silverx3);
        thisTierUncommon.Add(silverx4);
        thisTierRare.Add(silverx4);
        thisTierRare.Add(silverx5);
        thisTierRare.Add(patheticFire);
        thisTierRare.Add(patheticEarth);
        thisTierRare.Add(patheticWater);
        thisTierRare.Add(patheticAir);
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
            if (counter == 1) { item = thisTierCommon[(int)Mathf.Round(Random.Range(0, thisTierCommon.Count - 1))]; }
            else if (counter == 2) { item = thisTierUncommon[(int)Mathf.Round(Random.Range(0, thisTierUncommon.Count - 1))]; }
            else if (counter == 3) { item = thisTierRare[(int)Mathf.Round(Random.Range(0, thisTierRare.Count - 1))]; }
            else { item = thisTierExotic[(int)Mathf.Round(Random.Range(0, thisTierExotic.Count - 1))]; }
            dropped.Add(item);  
        }
        return dropped;
    }

    private static void InstantiateDrops(Vector2 position, List<ItemData> items)
    {
        if (items[0] == null) { return; }
        foreach (ItemData item in items)
        {
            ItemData itemData = item;
            int times = 1;
            if (item.sprite == null)  // is it one of the psuedo siver coins
            {
                itemData = silver;
                times = item.value;
            }
            while (times > 0)
            {
                GameObject drop = new GameObject("lootDrop");
                DungeonMaster.loot.Add(drop);
                if (itemData.itemType is ItemTypes.Type.weapon) { drop.AddComponent<ItemArchtype>().data = (ItemData)itemData; drop.GetComponent<ItemArchtype>().Dropped(); }
                else if (itemData.itemType is ItemTypes.Type.consumable) { } // TODO
                else if (itemData.itemType is ItemTypes.Type.item) { drop.AddComponent<ItemArchtype>().data = itemData; drop.GetComponent<ItemArchtype>().Dropped(); }
                drop.transform.position = position;
                drop.layer = 11;
                times -= 1;
            }
        }
    }

    public static void Generate(Vector2 position, int quality)
    {
        if (initialized == 1) { Start(); }
        List<ItemData> items = GenerateDropsList(quality);
        InstantiateDrops(position, items);
    }
}
