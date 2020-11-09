﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;
using System;
using System.Threading;

public class MapManager : MonoBehaviour
{
    public Caravan player;
    public Pyramid pyramidPrefab;
    public Oasis oasisPrefab;
    public List<Oasis> oases = new List<Oasis>();
    public List<Pyramid> pyramids = new List<Pyramid>();
    public Graph oasisGraph = new Graph("oasisGraph");
    public LineRenderer pathPrefab;

    public Oasis currentOasis;

    // Start is called before the first frame update
    void Awake()
    {
        //Find starting Oasis
        Oasis[] currentOases = FindObjectsOfType<Oasis>();
        foreach(Oasis oasis in currentOases)
        {
            oases.Add(oasis);
            OasisNode node = new OasisNode("startingOasis", oasis);
            oasisGraph.AddNode(node);
            oasis.oasisNode = node;
            
            currentOasis = oasis;
        }
        EventManager.TriggerOnOverworldStart();

        player = FindObjectOfType<Caravan>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveOverworld();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadOverworld();
        }
    }

    public void NewOasis(Vector2 position, float radius, Pyramid parentPyramid)
    {
        Oasis newOasis = Instantiate(oasisPrefab, position, Quaternion.identity);
        oases.Add(newOasis);
        newOasis.setRadius(radius);

        OasisNode node = new OasisNode(newOasis.transform.name + oases.Count, newOasis);
        oasisGraph.AddNode(node);
        newOasis.oasisNode = node;

        EventManager.TriggerOnNewOasis();

        BuildRelationships(newOasis);
    }

    public void BuildRelationships(Oasis oasis)
    {
        // Add other oases in the radius to connections
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(oasis.transform.position, oasis.radius * 0.8f);
        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.transform != oasis.transform && overlap.transform.gameObject.GetComponent<Oasis>() != null)
            {
                Oasis sibling = overlap.transform.gameObject.GetComponent<Oasis>();

                if (!oasisGraph.GetAdjByNode(oasis.oasisNode).GetAdj().Contains(sibling.oasisNode))
                {
                    oasisGraph.AddConnection(oasis.oasisNode, sibling.oasisNode);
                }
            }
            //add relationship to pyramids as well
            if (overlap.transform.gameObject.GetComponent<Pyramid>() != null && !oasis.pyramids.Contains(overlap.transform.gameObject.GetComponent<Pyramid>()))
            {
                Pyramid pyramid = overlap.transform.gameObject.GetComponent<Pyramid>();

                oasis.addExistingPyramid(pyramid);

            }
        }
        //create a line between the new oasis and its parent/nearby siblings
        foreach (Node nearbyNode in oasisGraph.GetAdjByNode(oasis.oasisNode).GetAdj())
        {
            oasis.oases.Add(((OasisNode)nearbyNode).getOasis());
            LineRenderer path = Instantiate(pathPrefab);
            Vector3[] points = new Vector3[2];
            points[0] = (Vector3)((OasisNode)nearbyNode).getOasis().transform.position;
            points[1] = points[0];
            path.startWidth = 0.2f;
            path.endWidth = 0.2f;
            path.SetPositions(points);
            oasis.oasisLines.Add(path);
        }
    }

    public void SaveOverworld()
    {
        SaveSystem.SaveOverworld(this);
    }

    public void LoadOverworld()
    {
        OverworldData loadedData = SaveSystem.LoadOverworld();

        foreach (Oasis oasis in oases)
        {
            while (oasis.oasisLines.Count > 0)
            {
                Destroy(oasis.oasisLines[0].gameObject);
                oasis.oasisLines.RemoveAt(0);
            }
            while (oasis.pyramidLines.Count > 0)
            {
                Destroy(oasis.pyramidLines[0].gameObject);
                oasis.pyramidLines.RemoveAt(0);
            }
        }

        LineRenderer[] lines = FindObjectsOfType<LineRenderer>();
        for(int i = 0; i < lines.Length; i++)
        {
            Destroy(lines[i].gameObject);
        }


        while (oases.Count > 0)
        {
            Destroy(oases[0].gameObject);
            oases.RemoveAt(0);
        }

        while (pyramids.Count > 0)
        {
            Destroy(pyramids[0].gameObject);
            pyramids.RemoveAt(0);
        }

        oasisGraph = new Graph("oasisGraph");

        for (int i = 0; i < loadedData.numOases; i++)
        {
            oases.Add(Instantiate(oasisPrefab, new Vector2(loadedData.oasisPositions[i, 0], loadedData.oasisPositions[i, 1]), Quaternion.identity));
            oases[oases.Count - 1].generated = true;
            OasisNode node = new OasisNode(oases[oases.Count - 1].transform.name + oases.Count, oases[oases.Count - 1]);
            oasisGraph.AddNode(node);
            oases[oases.Count - 1].oasisNode = node;
            if(i == loadedData.currentOasisIndex)
            {
                currentOasis = oases[oases.Count - 1];
                player.currentNode = node;
                player.traversal.currentNode = node;
                player.traversal.destinationNode = node;
            }
        }

        for (int i = 0; i < loadedData.numPyramids; i++)
        {
            pyramids.Add(Instantiate(pyramidPrefab, new Vector2(loadedData.pyramidPositions[i, 0], loadedData.pyramidPositions[i, 1]), Quaternion.identity));
        }

        foreach (Oasis oasis in oases)
        {
            BuildRelationships(oasis);
        }

        player.transform.position = new Vector2(loadedData.playerPosition[0], loadedData.playerPosition[1]);
        
        player.path.Clear();

        foreach(Follower follower in player.followers)
        {
            follower.path.Clear();
        }


    }

}
