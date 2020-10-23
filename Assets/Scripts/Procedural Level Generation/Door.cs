using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Door
{
    //doorA and B have to be 2 different transforms because the same door exists in 2 different prefab rooms
    private Transform doorA;
    private Transform doorB;
    private NodeComponent nodeA;
    private NodeComponent nodeB;
    private DoorComponent dc;
    private Transform dcTransform;
    // Heading of a door by node is the direction one must walk through the door to end up at the other node
    private Dictionary<NodeComponent,char> DoorHeadingByNode;

    public Door(NodeComponent NodeA, NodeComponent NodeB)
    {
        nodeA = NodeA;
        nodeB = NodeB;
        dcTransform = new GameObject("Door: " + nodeA.name + "-" + nodeB.name).transform;
        dcTransform.gameObject.AddComponent<DoorComponent>();
        dc = dcTransform.GetComponent<DoorComponent>();
        dc.SetDoor(this);
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
            throw new ArgumentException("Given node: " + node.name +" was not related to this door:" + dcTransform.name);
        }
        return DoorHeadingByNode[node];
    }

    public void SetParent(Transform doorHolder)
    {
        dcTransform.SetParent(doorHolder);
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
    public void EnableRenderLines()
    {
        dc.SetUpLineRenderer();
    }
    public void DestroyDoor()
    {
        dc.DestroyDoor();
    }
}
