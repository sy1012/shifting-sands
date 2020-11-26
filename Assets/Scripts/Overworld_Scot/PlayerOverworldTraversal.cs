using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldTraversal : MonoBehaviour
{
    private bool inventoryOpen;

    public Graph oasisGraph;
    MapManager mapManager;

    public OasisNode currentNode;

    public OasisNode destinationNode;

    public Caravan caravan;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        oasisGraph = mapManager.oasisGraph;
        currentNode = mapManager.currentOasis.oasisNode;
        destinationNode = currentNode;
        caravan = FindObjectOfType<Caravan>();
        List<Node> nodePath = graphTraversal(mapManager.oasisGraph, currentNode, destinationNode);
        caravan.path = nodePath;
    }

    // Update is called once per frame
    void Update()
    {
        // check for oasis being clicked on
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Oasis>() != null)
                {
                    EventManager.TriggerOnOasisClicked();
                    destinationNode = hit.collider.gameObject.GetComponent<Oasis>().oasisNode;
                    List<Node> nodePath = BFSPath(mapManager.oasisGraph, currentNode, destinationNode);
                    caravan.path = nodePath;
                    foreach (Follower follower in caravan.followers)
                    {
                        follower.path = new List<Node>(nodePath);
                    }
                }
            }
        }
    }

    public void EnterPyramid(Pyramid pyramid)
    {
        caravan.enterPyramid = pyramid;
        caravan.EnterDungeon();
        mapManager.SaveOverworld();
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

    public List<Node> BFSPath(Graph graph, Node start, Node destination)
    {
        var previous = new Dictionary<Node, Node>();

        var queue = new Queue<Node>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            foreach (var neighbor in graph.GetAdjByNode(vertex).GetAdj())
            {
                if (previous.ContainsKey(neighbor))
                    continue;

                previous[neighbor] = vertex;
                queue.Enqueue(neighbor);
            }
        }

        var path = new List<Node>();

        var current = destination;
        while (!current.Equals(start))
        {
            path.Add(current);
            current = previous[current];
        };

        path.Add(start);
        path.Reverse();

        return path;
    }   

}
