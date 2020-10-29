using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using GraphGrammars;

[ExecuteInEditMode]
[System.Serializable]
public class NodeComponent : MonoBehaviour
{
    [SerializeField]
    public List<NodeComponent> neighbours;
    private Node assignedNode;
    public Symbol symbol;
    public bool AddNeighbour(NodeComponent node)
    {
        if (neighbours == null)
        {
            neighbours = new List<NodeComponent>();
        }
        if (neighbours.Contains(node))
        {
            return false;
        }
        neighbours.Add(node);
        return true;
    }
    public bool RemoveNeighbour(NodeComponent node)
    {
        if (neighbours.Contains(node))
        {
            neighbours.Remove(node);
            return true;
        }
        Debug.Log("warning. Trying to remove neighbour that is not a neighbour of this node");
        return false;
    }
    public bool isAssignedTo(Node node) { return assignedNode == node ? true : false; }
    public void AssignNode(Node node) { assignedNode = node; }
    public Node AssignedNode { get { return assignedNode; } }
    private void Awake()
    {
        //neighbours = new List<NodeComponent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        foreach (var neighbour in neighbours)
        {
            Color color = Color.blue;
            if (neighbour!=null)
            {
                DrawArrow.ForDebug(transform.position, neighbour.transform.position-transform.position,color);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (symbol == Symbol.Entrance)
        {
            Gizmos.color = Color.white;
        }
        else if (symbol == Symbol.NT)
        {
            Gizmos.color = Color.green;
        }
        else if (symbol == Symbol.t)
        {
            Gizmos.color = Color.grey;
        }
        else if (symbol == Symbol.Goal)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position,0.11f);
        //Handles.Label(transform.position - Vector3.up * 0.25f, symbol.ToString() + " "+ transform.name);
    }
    // Update is called once per frame
}
