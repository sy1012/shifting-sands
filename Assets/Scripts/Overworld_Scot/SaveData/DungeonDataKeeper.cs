﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDataKeeper : MonoBehaviour
{
    public bool beatLastDungeon = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
