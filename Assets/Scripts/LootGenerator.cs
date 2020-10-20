using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGenerator
{
    private static LootGenerator _current;
    public static LootGenerator current
    {
        get
        {
            if (_current == null) { _current = new LootGenerator(); }
            return _current;
        }
    }

    // ACTUAL GENERATION WONT REQUIRE PASSING A REFERENCE TO ITEM TO GENERATE
    public void generate(Vector2 position, ItemData item)
    {
        // THIS IS A MOCKUP, ACTUAL ITEM GENERATION WONT BE THIS SIMPLE
        GameObject.Instantiate(item, position, Quaternion.identity);
    }
}
