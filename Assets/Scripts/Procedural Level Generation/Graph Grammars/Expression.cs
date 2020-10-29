using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphGrammars {

    /// <summary>
    /// Precondition that gameobjects in heirarchy have Node Components with adjacencies.
    /// Take heirarchy as input and transform into a graph instance.
    /// </summary>
    [System.Serializable]
    [ExecuteInEditMode]
    public class Expression : MonoBehaviour
    {
        [SerializeField]
        Graph graph;
        public Graph Graph { get { return graph; } }    
        // Start is called before the first frame update
        void Awake()
        {
            Refresh();
        }
        public override string ToString()
        {
            return "Expression " + transform.name + "\n" + graph.ToString();
        }

        /// <summary>
        /// Reinitialized Nodes from prefab rule Node Component information. Takes Symbol,Position and Adjacencies.
        /// </summary>
        public void Refresh()
        {
            // Precondition of gameobjects in heirarchy that have Node Components with adjacencies.
            var ruleNodeComps = gameObject.GetComponentsInChildren<NodeComponent>();
            Dictionary<NodeComponent, Node> nc_to_n = new Dictionary<NodeComponent, Node>(); // For transfering adj from component to class instance
            graph = new Graph();
            graph.name = transform.name;
            //Instantiate a new node based on Symbol
            foreach (NodeComponent nodeComponent in ruleNodeComps)
            {
                string nodeName = nodeComponent.transform.name;
                Symbol nodeType = nodeComponent.symbol;
                Node newNode = null;
                switch (nodeType)
                {
                    case Symbol.t:
                        newNode = new Node(nodeName);
                        break;
                    case Symbol.Entrance:
                        newNode = new EntranceNode();
                        break;
                    case Symbol.Goal:
                        newNode = new GoalNode();
                        break;
                    case Symbol.NT:
                        newNode = new NTNode(nodeName);
                        break;
                    case Symbol.Start:
                        newNode = new StartNode();
                        break;
                    case Symbol.Key:
                        newNode = new KeyNode("Key");
                        break;
                    case Symbol.Lock:
                        newNode = new LockNode("Lock");
                        break;
                    default:
                        throw new System.Exception("Node Component not assigned proper symbol");
                }
                //Set default start position of node to be its position relative to the prefab rule.
                Vector2 defaultPosition = new Vector2(nodeComponent.transform.localPosition.x, nodeComponent.transform.localPosition.y);
                newNode.SetPosition(defaultPosition);
                graph.AddNode(newNode);
                //Add new node to NC to N record
                nc_to_n.Add(nodeComponent, newNode);
            }
            // Map adjacencies from NodeComponent to Node
            foreach (NodeComponent nodeComponent in ruleNodeComps)
            {
                foreach (var neighbour in nodeComponent.neighbours)
                {
                    if (nodeComponent == null)
                    {
                        Debug.Log("NC is null for: " + name);
                    }
                    if (neighbour == null)
                    {
                        Debug.Log("Neighbour is null for NC: " + name);
                    }
                    if (!nc_to_n.ContainsKey(nodeComponent)||!nc_to_n.ContainsKey(neighbour))
                    {
                        //Debug.Log("Ensure all rule templates are disabled in scene.");
                    }
                    graph.AddConnection(nc_to_n[nodeComponent], nc_to_n[neighbour]);
                }
            }
        }
    }
}