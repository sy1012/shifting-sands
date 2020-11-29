using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LootGenerator
{
    private static int initialized = 1;
    private static ItemData smallHealthOrb;
    private static ItemData mediumHealthOrb;
    private static ItemData largeHealthOrb;
    private static ItemData silver;
    private static ItemData silverx2;
    private static ItemData silverx3;
    private static ItemData silverx4;
    private static ItemData silverx5;
    private static ItemData silverx10;

    private static List<float> dropQuality0;
    private static List<float> dropQuality1;
    private static List<float> dropQuality2;
    private static List<float> dropQuality3;

    private static List<ItemData> tier0 = new List<ItemData>();
    private static List<ItemData> tier1 = new List<ItemData>();
    private static List<ItemData> tier2 = new List<ItemData>();
    private static List<ItemData> tier3 = new List<ItemData>();

    private static List<float> dropRate0;
    private static List<float> dropRate1;
    private static List<float> dropRate2;
    private static List<float> dropRate3;

    //private static int dungeonLevel = 0;

    static void Start()
    {
        initialized = 0;

        /* These variables are used to control drop chances of classes of items for a particular quality */
        dropQuality0 = new List<float> { 0.0f, 0.8f };
        dropQuality1 = new List<float> { 0.0f, 0.2f, 0.7f };
        dropQuality2 = new List<float> { 0.0f, 0.2f, 0.5f, 0.8f };
        dropQuality3 = new List<float> { 0.0f, 0.0f, 0.2f, 0.4f };

        /* These variables are used to control drop numbers for a particular quality */
        dropRate0 = new List<float> { 0.0f, 0.8f };
        dropRate1 = new List<float> { 0.0f, 0.2f, 0.7f };
        dropRate2 = new List<float> { 0.0f, 0.1f, 0.5f, 0.8f };
        dropRate3 = new List<float> { 0.0f, 0.1f, 0.3f, 0.5f, 0.8f };

        //EventManager.onEnteringDungeon += (object sender, EventManager.onEnteringDungeonEventArgs e) => {
        //    dungeonLevel = e.dungeonLevel;

        //    if (dungeonLevel == 1)
        //    {
        //        thisTierCommon = firstTierCommon;
        //        thisTierUncommon = firstTierUncommon;
        //        thisTierRare = firstTierRare;
        //        thisTierExotic = firstTierExotic;
        //    }
        //    else { Debug.Log("NOT IMPLEMENTED YET"); }
        //};

        // load in everything we will drop
        smallHealthOrb = Resources.Load<ConsumableData>("Consumables/SmallHealthOrb");
        mediumHealthOrb = Resources.Load<ConsumableData>("Consumables/MediumHealthOrb");
        largeHealthOrb = Resources.Load<ConsumableData>("Consumables/LargeHealthOrb");
        silver = Resources.Load<ItemData>("Items/SilverPiece");
        silverx2 = Resources.Load<ItemData>("Items/SilverPiecex2");
        silverx3 = Resources.Load<ItemData>("Items/SilverPiecex3");
        silverx4 = Resources.Load<ItemData>("Items/SilverPiecex4");
        silverx5 = Resources.Load<ItemData>("Items/SilverPiecex5");
        silverx10 = Resources.Load<ItemData>("Items/SilverPiecex10"); 


        /* set up the tier Lists */
        tier0.Add(silver);
        tier0.Add(silverx2);
        tier0.Add(silverx2);
        tier0.Add(smallHealthOrb);
        tier1.Add(silver);
        tier1.Add(silverx2);
        tier1.Add(silverx3);
        tier1.Add(silverx4);
        tier0.Add(smallHealthOrb);
        tier0.Add(mediumHealthOrb);
        tier2.Add(silverx4);
        tier2.Add(silverx5);
        tier2.Add(silverx10);
        tier2.Add(mediumHealthOrb);
        tier2.Add(largeHealthOrb);
        tier3.Add(silverx10);
    }

    private static List<ItemData> GenerateDropsList(int quality)
    {
        List<ItemData> dropped = new List<ItemData>();
        List<float> dropQuality = new List<float>();  // REMOVE THIS ONCE ALL DROP QUALITIES HAVE BEEN CREATED AND HANDLED
        List<float> dropRate = new List<float>();

        // What quality of items will we be pulling from
        switch (quality)
        {
            case 0:
                dropQuality = dropQuality0; dropRate = dropRate0; break;
            case 1:
                dropQuality = dropQuality1; dropRate = dropRate1; break;
            case 2:
                dropQuality = dropQuality2; dropRate = dropRate2; break;
            case 3:
                dropQuality = dropQuality3; dropRate = dropRate3; break;
            default:
                throw new System.ArgumentOutOfRangeException("No item quality of: " + quality);
        }

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
            if (counter == 1) { item = tier0[(int)Mathf.Round(Random.Range(0, tier0.Count - 1))]; }
            else if (counter == 2) { item = tier1[(int)Mathf.Round(Random.Range(0, tier1.Count - 1))]; }
            else if (counter == 3) { item = tier2[(int)Mathf.Round(Random.Range(0, tier2.Count - 1))]; }
            else { item = tier3[(int)Mathf.Round(Random.Range(0, tier3.Count - 1))]; }
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

                // health orbs
                if (itemData.itemType is ItemTypes.Type.consumable) {
                    drop.AddComponent<ConsumableObject>().data = (ConsumableData)itemData; drop.GetComponent<ConsumableObject>().Dropped(); } // TODO

                // coins
                else { drop.AddComponent<ItemArchtype>().data = itemData; drop.GetComponent<ItemArchtype>().Dropped(); }
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
