using System;
using System.Collections;
using System.Collections.Generic;
using GraphGrammars;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class GraphComponent : MonoBehaviour
{
    /*
     * Make Graph from heirarchy
     * Pass Graph on
     * Take in Graph and Apply to Heirarchy
     * */
    [SerializeField]
    public List<NodeComponent> ncList;
    public List<NodeComponent> RoomNodeComponents()
    {
        var roomNodes = new List<NodeComponent>();
        foreach (var nc in ncList)
        {
            if (!(nc.symbol == Symbol.Encounter || nc.symbol == Symbol.Treasure))
            {
                roomNodes.Add(nc);
            }
        }
        return roomNodes;
    }
    public int Size()
    {
        return ncList.Count;
    }
    public bool IsEmpty()
    {
        return ncList.Count == 0 ? true : false;
    }
    public void AddNode(NodeComponent node)
    {
        if (!ncList.Contains(node))
        {
            ncList.Add(node);
        }
        else
        {
            Debug.Log("Warning, node to add already in graph");
        }
    }
    public void AddConnection(NodeComponent fromNode, NodeComponent toNode)
    {
        fromNode.AddNeighbour(toNode);
        toNode.AddNeighbour(fromNode);
    }

    /// <summary>
    /// GetEdges without any edges to non room nodes
    /// </summary>
    /// <returns></returns>
    public List<List<NodeComponent>> GetRoomEdges()
    {
        List<List<NodeComponent>> edgesToRemove = new List<List<NodeComponent>>();
        List<List<NodeComponent>> edges = GetEdges();
        foreach (var edge in edges)
        {
            foreach (var vert in edge)
            {
                if (vert.symbol == Symbol.Encounter || vert.symbol== Symbol.Treasure)
                {
                    edgesToRemove.Add(edge);
                }
            }
        }
        foreach (var edgeToRem in edgesToRemove)
        {
            edges.Remove(edgeToRem);
        }
        return edges;
    }

    public void Reset()
    {
        while (ncList.Count>0)
        {
            RemoveNode(ncList[0]);
        }
    }

    /// <summary>
    /// Uses BFS to create a list of Bidirectional edges. Each edge is a list of Node Components
    /// </summary>
    /// <returns></returns>
    public List<List<NodeComponent>> GetEdges()
    {
        List<List<NodeComponent>> edges = new List<List<NodeComponent>>();
        bool[] visited = new bool[Size()];
        GetEdgesUtil(ncList[0], visited, edges);
        return edges;
    }

    /// <summary>
    /// Tool used by BFS to get a list of BiDirectional edges. Each Edge is a list of Nodes
    /// </summary>
    /// <param name="root"></param>
    /// <param name="visited"></param>
    /// <param name="edges"></param>
    private void GetEdgesUtil(NodeComponent root, bool[] visited, List<List<NodeComponent>> edges)
    {
        visited[ncList.IndexOf(root)]  = true;
        //Log this nodes edges if they do not exist
        foreach (var adj in root.neighbours)
        {
            // See if adj node has already been visited. If so it must have made an edge for this node already
            if (visited[ncList.IndexOf(adj)])
            {
                continue;
            }
            // Else Make new edge
            edges.Add(new List<NodeComponent>() { root, adj });
            // Search that node now for new edges
            
        }
        foreach (var adj in root.neighbours)
        {
            if (visited[ncList.IndexOf(adj)])
            {
                continue;
            }
            GetEdgesUtil(adj, visited, edges);
        }
    }

    public void SnapToGrid()
    {
        foreach (var nc in ncList)
        {
            nc.transform.position = new Vector3((int)nc.transform.position.x
                , (int)nc.transform.position.y
                , (int)nc.transform.position.z);
        }
    }

    public void UpdateToGraph(Graph graph)
    {
        if (ncList == null)
        {
            ncList = new List<NodeComponent>();
        }
        var newGraphNodes = graph.GetNodes;
        //Delete unused nc.
        List<Vector3> delNPositions = new List<Vector3>();
        List<NodeComponent> nc_remove = new List<NodeComponent>();
        foreach (var nc in ncList)
        {
            if (!newGraphNodes.Contains(nc.AssignedNode))
            {
                delNPositions.Add(nc.transform.position);
                nc_remove.Add(nc);
            }
        }
        foreach (var rem in nc_remove)
        {
            RemoveNode(rem);
        }
        Vector3 deletedNodesCentroid = Utilities.AverageVector3(delNPositions);
        //Assign new NC to new generated nodes
        foreach (var n in graph.GetNodes)
        {
            bool assigned = false;
            foreach (var nc in ncList)
            {
                if (nc.isAssignedTo(n))
                {
                    assigned = true;
                }
            }
            if (!assigned)
            {
                Transform newNC = new GameObject(n.ToString()).transform;
                NodeComponent nc;
                switch (n.GetSymbol)
                {
                    case Symbol.Encounter:
                        var nc2 = newNC.gameObject.AddComponent<EncounterNodeComponent>();
                        var n2 = (EncounterNode)n;
                        nc2.Difficulty = n2.difficulty;
                        nc = nc2;
                        break;
                    case Symbol.Treasure:
                        var nc3 = newNC.gameObject.AddComponent<TreasureNodeComponent>();
                        var n3 = (TreasureNode)n;
                        nc3.Value = n3.value;
                        nc = nc3;
                        break;
                    default:
                        nc = newNC.gameObject.AddComponent<NodeComponent>();
                        break;
                }
                newNC.transform.position = deletedNodesCentroid + new Vector3(n.Position.x, n.Position.y, 0);
                ncList.Add(newNC.GetComponent<NodeComponent>());
                nc.AssignNode(n);
                nc.symbol = n.GetSymbol;
                newNC.SetParent(this.transform);
            }
        }
        //Preserve graph edges
        foreach (var nc in ncList)
        {
            var n = nc.AssignedNode;
            var adjList = graph.GetAdjByNode(n).GetAdj();
            foreach (var a in adjList)
            {
                foreach (var nc2 in ncList)
                {
                    if (nc2.isAssignedTo(a))
                    {
                        AddConnection(nc, nc2);
                        AddConnection(nc2, nc);
                    }
                }
            }
        }
    }

    public NodeComponent PopNode()
    {
        if (IsEmpty())
        {
            Debug.Log("Baaad behaviour");
            return null;
        }
        NodeComponent node = ncList[0];
        ncList.RemoveAt(0);
        return node;
    }
    public void RemoveNode(NodeComponent node)
    {
        ncList.Remove(node);
        foreach (var neighbour in node.neighbours)
        {
            neighbour.RemoveNeighbour(node);
        }
        Destroy(node.gameObject);
    }
    public void UpdateGraphComponentByHeirarchy()
    {
        ncList.Clear();
        foreach (var node in gameObject.GetComponentsInChildren<NodeComponent>())
        {
            AddNode(node);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //nodes = new List<NodeComponent>();
        //UpdateGraphComponentByHeirarchy();
        //MakeGraphFromHeirarchy();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
