using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class RefreshPathfinder : MonoBehaviour
{
    int tick = 0;
    int max = 5;
    // Start is called before the first frame update
    void Awake()
    {
        tick = 0;
    }

    private void Update()
    {
        //Refresh after 60 frames once. Only once
        if (tick > max)
        {
            RefreshPathfindingGraph(EventArgs.Empty);
        }
        tick++;
    }

    private void OnDestroy()
    {
        
    }
    //FORCES THE PATHFINDER TO REFRESH
    private void RefreshPathfindingGraph(System.EventArgs e)
    {
        AstarPath astarPath = GetComponent<AstarPath>();
        astarPath.Scan();
        Destroy(this);
    }
}
