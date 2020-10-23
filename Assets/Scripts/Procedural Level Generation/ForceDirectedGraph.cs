using System.Collections;
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
        foreach (var node in graphComponent.ncList)
        {
            //Calculate Applied Forces
            Vector2 fs = Vector2.zero;
            Vector2 fr = Vector2.zero;
            Vector2 nodePos = new Vector2(node.transform.position.x, node.transform.position.y);
            //Calculate Spring Force with other Connected Nodes
            foreach (var neighbour in node.neighbours)
            {
                // Find spring force for this neighbour
                Vector2 neighbourPos = new Vector2(neighbour.transform.position.x, neighbour.transform.position.y);
                Vector2 dir = (neighbourPos - nodePos).normalized;
                fs += dir * ks * ((neighbourPos - nodePos).magnitude - neutralLength);
            }
            //Calculate Repulsion Forces with all other nodes
            foreach (var otherNode in graphComponent.ncList)
            {
                if (node.transform == otherNode.transform || node.neighbours.Contains(otherNode))
                {
                    continue;
                    //Don't act on self
                }
                Vector2 otherNodePos = new Vector2(otherNode.transform.position.x, otherNode.transform.position.y);
                //Debug.Log(otherNodePos + "" + nodePos);
                Vector2 dir = (otherNodePos - nodePos).normalized;
                float dist = (otherNodePos - nodePos).magnitude;
                fr += dir * kr / (dist*dist);
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

    // Update is called once per frame
    void Update()
    {
        if (forcesOn)
        {
            SolveForceDrivenGraph();
        }
    }
}
