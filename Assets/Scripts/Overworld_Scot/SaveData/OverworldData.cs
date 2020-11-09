using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OverworldData
{
    public int numOases;
    public int numPyramids;
    public float[,] oasisPositions;
    public float[,] pyramidPositions;
    public float[] playerPosition;
    public int currentOasisIndex;

    public OverworldData(MapManager mapManager)
    {
        numOases = mapManager.oases.Count;
        numPyramids = mapManager.pyramids.Count;

        oasisPositions = new float[numOases, 2];
        pyramidPositions = new float[numPyramids, 2];

        playerPosition = new float[2];
        playerPosition[0] = mapManager.player.transform.position.x;
        playerPosition[1] = mapManager.player.transform.position.y;

        for (int i = 0; i < numOases; i++)
        {
            if(mapManager.oases[i] == mapManager.player.currentNode.getOasis())
            {
                currentOasisIndex = i;
            }
            oasisPositions[i, 0] = mapManager.oases[i].transform.position.x;
            oasisPositions[i, 1] = mapManager.oases[i].transform.position.y;
        }

        for (int i = 0; i < numPyramids; i++)
        {
            pyramidPositions[i, 0] = mapManager.pyramids[i].transform.position.x;
            pyramidPositions[i, 1] = mapManager.pyramids[i].transform.position.y;
        }
    }

}
