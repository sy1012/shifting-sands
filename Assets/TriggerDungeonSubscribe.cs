﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDungeonSubscribe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.TriggerOnResubscribeDungeon(null);
    }
}
