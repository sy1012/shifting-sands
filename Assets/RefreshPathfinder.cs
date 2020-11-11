using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RefreshPathfinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.onDungeonGenerated += RefreshPathfindingGraph;
    }


    private void OnDestroy()
    {
        EventManager.onDungeonGenerated -= RefreshPathfindingGraph;
        
    }
    private void RefreshPathfindingGraph(System.EventArgs e)
    {
        AstarPath astarPath = GetComponent<AstarPath>();
        astarPath.Scan();
        Destroy(this);
    }
}
