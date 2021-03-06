﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;

public class ForceDirectedGraph : MonoBehaviour
{
    public GraphComponent graphComponent;
    public bool forcesOn;
    public float neutralLength = 5;
    public float ks = 0.2f; //Force Spring Constant
    public float kr = 10; //Force Repulsion Constant
    // Start is called before the first frame update
    void Start()
    {
        neutralLength *= Random.Range(0.7f, 1.2f);
    }

    public void SetGraphComponent(GraphComponent gc)
    {
        graphComponent = gc ?? throw new UnassignedReferenceException("Graph Component must not be null");
    }

    public void SolveForceDrivenGraph()
    {
        Dictionary<NodeComponent, Vector2> NodeToForce = new Dictionary<NodeComponent, Vector2>();
        foreach (var node in graphComponent.RoomNodeComponents())
        {
            //Calculate Applied Forces
            Vector2 fs = Vector2.zero;
            Vector2 fr = Vector2.zero;
            Vector2 nodePos = new Vector2(node.transform.position.x, node.transform.position.y);
            //Calculate Spring Force with other Connected Nodes
            foreach (var neighbour in node.neighbours)
            {
                if (neighbour is EncounterNodeComponent || neighbour is TreasureNodeComponent)
                {
                    // Dont act on non room nodes
                    continue;
                }
                // Find spring force for this neighbour
                Vector2 neighbourPos = new Vector2(neighbour.transform.position.x, neighbour.transform.position.y);
                Vector2 dir = (neighbourPos - nodePos).normalized;
                fs += dir * ks * ((neighbourPos - nodePos).magnitude - neutralLength);
            }
            //Calculate Repulsion Forces with all other room nodes
            foreach (var otherNode in graphComponent.RoomNodeComponents())
            {
                if (node.transform == otherNode.transform || node.neighbours.Contains(otherNode))
                {
                    continue;
                    //Don't act repulse self or neighbours
                }
                Vector2 otherNodePos = new Vector2(otherNode.transform.position.x, otherNode.transform.position.y);
                //Debug.Log(otherNodePos + "" + nodePos);
                Vector2 dir = (otherNodePos - nodePos).normalized;
                float dist = (otherNodePos - nodePos).magnitude;
                fr += dir * kr / (dist * dist);
            }
            NodeToForce.Add(node, fs - fr);
        }
        // Move Nodes with total forces

        foreach (KeyValuePair<NodeComponent, Vector2> kvp in NodeToForce)
        {
            kvp.Key.transform.position = kvp.Key.transform.position + new Vector3(kvp.Value.x, kvp.Value.y, 0);
        }

    }

    public void SolveForceDrivenGraph(float _neutralLength, float _ks, float _kr)
    {
        neutralLength = _neutralLength;
        ks = _ks;
        kr = _kr;
        SolveForceDrivenGraph();
    }

    //Carefully tuned recipe for fdg. Alter at own risk or make another.
    public void SolveFDG_Recipe()
    {
        for (int i = 0; i < 15; i++)
        {
            SolveForceDrivenGraph(1f, 0.2f, 0.4f);
        }
        for (int i = 0; i < 15; i++)
        {
            SolveForceDrivenGraph(0.9f, 0.3f, 0.5f);
        }
        for (int i = 0; i < 10; i++)
        {
            SolveForceDrivenGraph(6f, 0.2f, 1f);
        }
        for (int i = 0; i < 10; i++)
        {
            SolveForceDrivenGraph(14f, 0.2f, 50f);
        }
        for (int i = 0; i < 10; i++)
        {
            SolveForceDrivenGraph(18f, 0.2f, 500f);
        }
        for (int i = 0; i < 10; i++)
        {
            SolveForceDrivenGraph(24f, 0.2f, 1300f);
        }
        for (int i = 0; i < 10; i++)
        {
            SolveForceDrivenGraph(34f, 0.2f, 2300f);
        }
        for (int i = 0; i < 10; i++)
        {
            SolveForceDrivenGraph(44f, 0.2f, 3600f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (forcesOn)
        {
            SolveForceDrivenGraph();
        }
    }
}
