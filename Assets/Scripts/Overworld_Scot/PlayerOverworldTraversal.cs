using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldTraversal : MonoBehaviour
{
    public Graph oasisGraph;
    MapManager mapManager;

    OasisNode currentNode;

    OasisNode destinationNode;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        oasisGraph = mapManager.oasisGraph;
        currentNode = (OasisNode)mapManager.oasisGraph.GetNodes[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Oasis>() != null)
                {
                    destinationNode = hit.collider.gameObject.GetComponent<Oasis>().oasisNode;
                    List<Node> nodePath = graphTraversal(mapManager.oasisGraph, currentNode, destinationNode);
                    foreach(Node node in nodePath)
                    {
                        Debug.Log(node);
                    }
                }
            }

        }
    }

    List<Node> graphTraversal(Graph graph, Node root, OasisNode destination)
    {
        
        Stack<Node> nodeStack = new Stack<Node>();

        nodeStack.Push(root);
        List<Node> visited = new List<Node>();
        visited = graphTraversalUtility(graph, root, destination, visited);
        
        return visited;
        
    }

    List<Node> graphTraversalUtility(Graph graph, Node root, Node destination, List<Node> visitList)
    {
        visitList.Add(root);
        if(root == destination)
        {
            
            return visitList;
        }
        else
        {
            var adj = graph.GetAdjByNode(root).GetAdj();
            foreach(Node node in adj)
            {
                if (!visitList.Contains(node))
                {
                    var visited = graphTraversalUtility(graph, node, destination, new List<Node>(visitList));
                    if (visited != null)
                    {
                        return visited;
                    }
                }
                
            }
            return null;
        }
    }

}
