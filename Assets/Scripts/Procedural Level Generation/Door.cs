using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Door:MonoBehaviour
{
    //doorA and B have to be 2 different transforms because the same door exists in 2 different prefab rooms
    private Transform doorA;
    private Transform doorB;
    private NodeComponent nodeA;
    private NodeComponent nodeB;
    private LineRenderer lr;

    // Heading of a door by node is the direction one must walk through the door to end up at the other node
    private Dictionary<NodeComponent,char> DoorHeadingByNode;

    public void Initialize(NodeComponent NodeA, NodeComponent NodeB)
    {
        nodeA = NodeA;
        nodeB = NodeB;
        DoorHeadingByNode = new Dictionary<NodeComponent, char>();
        FindDoorHeadings();
        //Debug.Log(nodeA.name + ": " + GetDoorHeadingByNode(nodeA) + "|-door-| " + GetDoorHeadingByNode(nodeB)+":"+ nodeB.name);
    }

    public Transform[] GetDoorTransforms()
    {
        return new Transform[] { doorA, doorB };
    }

    public NodeComponent GetNeighbourNodeOfDoorFor(NodeComponent node)
    {
        if (node == nodeA)
        {
            return nodeB;
        }
        return nodeA;
    }
    /// <summary>
    /// Returns true if door is linking the given node to any other node
    /// Returns false if door doesn't have anything to do with the given node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool Links(NodeComponent node)
    {
        if (nodeA == node)
        {
            return true;
        }
        else if (nodeB == node)
        {
            return true;
        }
        return false;
    }

    public char GetDoorHeadingByNode(NodeComponent node)
    {
        if (!DoorHeadingByNode.ContainsKey(node))
        {
            throw new ArgumentException("Given node: " + node.name +" was not related to this door:" + transform.name);
        }
        return DoorHeadingByNode[node];
    }

    public void SetParent(Transform doorHolder)
    {
        transform.SetParent(doorHolder);
    }

    private void FindDoorHeadings()
    {
        Dictionary<char, char> matchedHeadings = new Dictionary<char, char>() { { 'N', 'S' }, { 'E', 'W' }, { 'S', 'N' }, { 'W', 'E' } }; //for matching headings
        //Step through neighbours to find door heading needed for each neighbour
        char headingNodeADoor;
        Vector3 dir = nodeB.transform.position - nodeA.transform.position;
        dir = new Vector3(dir.x, dir.y, 0);
        float angle = Vector3.SignedAngle(Vector3.up, dir, -Vector3.forward);
        if (angle < 45 && angle > -45)
        {
            headingNodeADoor = 'N';
        }
        else if (angle >= 45 && angle < 135)
        {
            headingNodeADoor = 'E';
        }
        else if (angle <= -45 && angle > -135)
        {
            headingNodeADoor = 'W';
        }
        else
        {
            headingNodeADoor = 'S';
        }
        DoorHeadingByNode.Add(nodeA, headingNodeADoor);
        char headingNodeBDoor = matchedHeadings[headingNodeADoor];
        DoorHeadingByNode.Add(nodeB, headingNodeBDoor);
        return;
    }

    public void AssignRoomDoorTr(Transform transform, NodeComponent roomNode)
    {
        if (roomNode == nodeA)
        {
            doorA = transform;
        }
        else if (roomNode == nodeB)
        {
            doorB = transform;
        }
    }

    public void SetUpLineRenderer()
    {
        var templatesLR = FindObjectsOfType<LineRenderer>();
        LineRenderer template = null;
        foreach (var lr in templatesLR)
        {
            //Template could be null if someone renames this
            if (lr.name == "DoorLineRenderer")
            {
                template = lr;
            }
        }
        lr = gameObject.AddComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        Transform[] doorTr = GetDoorTransforms();
        lr.SetPosition(0, doorTr[0].transform.position);
        lr.SetPosition(1, doorTr[1].transform.position);
        lr.material = template.material;
        lr.startColor = Color.cyan;
        lr.endColor = Color.cyan;
        lr.startWidth = 0.35f;
        lr.endWidth = 0.35f;
    }

    public void Update()
    {
        if (doorA!=null && doorB!=null && lr==null)
        {
            SetUpLineRenderer();
        }
    }

}
