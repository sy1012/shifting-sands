using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster
{
    public static List<GameObject> loot = new List<GameObject>();

    public static List<GameObject> getLootInRange(Vector2 position, float range) {
        List<GameObject> stuff = new List<GameObject>();
        foreach (GameObject item in loot)
        {
            float hyp = Mathf.Sqrt(Mathf.Pow((item.transform.position.y - position.y), 2) + Mathf.Pow((item.transform.position.x - position.x), 2));
            if (hyp <= range) stuff.Add(item);
        }
        return stuff;
    }

    public static List<GameObject> getLootOuttaRange(Vector2 position, float range)
    {
        List<GameObject> stuff = new List<GameObject>();
        foreach (GameObject item in loot)
        {
            float hyp = Mathf.Sqrt(Mathf.Pow((item.transform.position.y - position.y), 2) + Mathf.Pow((item.transform.position.x - position.x), 2));
            if (hyp >= range) stuff.Add(item);
        }
        return stuff;
    }
}
