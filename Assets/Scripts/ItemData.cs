﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{ 
    public string itemName;            // name of this item
    public int value;              // How much could this be sold for
    public List<ItemData> recipe;  // List of items that could be put together to make this item
}
