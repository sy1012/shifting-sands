using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System;

public class RoomGenerator : MonoBehaviour
{
    //Maybe protect these later
    public GraphComponent gc;
    public RoomPicker roomPicker;
    public List<Transform> rooms;
    public List<Door> doors;
    private Transform doorHolder;
    private Transform roomHolder;

    // A table of Node to Node To Direction of Door. e.g. Room A's door to Room B faces "S" for south.
    [SerializeField]
    private Dictionary<NodeComponent, Transform> _levelRoomByNode;


    private void Start()
    {
    }
    public void Refresh()
    {
        for (int i = rooms.Count - 1; i >= 0; i--)
        {
            Destroy(rooms[i].gameObject);
        }
        for (int i = doors.Count - 1; i >= 0; i--)
        {
            Destroy(doors[i].gameObject);
        }
        if (doorHolder != null)
        {
            Destroy(doorHolder.gameObject);
        }
        if (roomHolder != null)
        {
            Destroy(roomHolder.gameObject);
        }
        doors = new List<Door>();
        rooms = new List<Transform>();
    }
    public void MakeRooms()
    {
        roomHolder = new GameObject("Room Holder").transform;
        rooms = new List<Transform>();
        _levelRoomByNode = new Dictionary<NodeComponent, Transform>();
        foreach (var node in gc.RoomNodeComponents())
        {
            MakeRoom(node);
        }
    }

    public GameObject MakeRoom(NodeComponent node)
    {

        List<Door> roomDoors = GetNodeDoors(node);
        DoorT doorTree = new DoorT(roomDoors, node);
        GameObject newRoom = roomPicker.GetRoomMatch(DoorT.ToOrderedStack(doorTree, null), node);
        newRoom.name = "Room :" + node.name;
        if (newRoom != null)
        {
            rooms.Add(newRoom.transform);
            _levelRoomByNode.Add(node, newRoom.transform);
            newRoom.GetComponent<Room>().RoomNode = node;
            newRoom.transform.SetParent(roomHolder);
        }
        //Connect doors to room door transforms
        List<Door> orderedDoors = DoorT.ToOrderedList(doorTree, null);

        /*Logging
        string orderStr = node.name + " has these doors in order:";
        foreach (var door in orderedDoors)
        {
            orderStr += " " + door.GetNeighbourNodeOfDoorFor(node).name + ",";
        }
        Debug.Log(orderStr);
        */// End logging

        newRoom.GetComponent<Room>().MatchUpDoors(orderedDoors);


        return null;
    }


    /// <summary>
    /// Makes and returns a list of all Door objects connecting a given node to the rest of the level
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public List<Door> GetNodeDoors(NodeComponent node)
    {
        List<Door> roomDoors = new List<Door>();
        foreach (var door in doors)
        {
            if (door.Links(node))
            {
                roomDoors.Add(door);
            }
        }
        //Sanity check
        if (roomDoors.Count == 0)
        {
            throw new ArgumentException("Node does not have any connection! : " + node.name);
        }
        return roomDoors;
    }


    /// <summary>
    /// Makes a string representing the order of heading for the room node's doors.
    /// North first, sorted West to East most
    /// East second, sorted North to South most;
    /// South third, sorted East to West most;
    /// West fourth, sorted South to North most;
    /// </summary>
    /// <param name="node"></param> The room node in question
    /// <param name="nDoors"></param> The node's doors
    /// <returns></returns>

    public void MakeDoor(NodeComponent nodeA, NodeComponent nodeB)
    {
        GameObject newDoorGO = new GameObject("Door: " + nodeA.name + "-" + nodeB.name);
        Door newDoor = newDoorGO.AddComponent<Door>();
        newDoor.Initialize(nodeA, nodeB);
        doors.Add(newDoor);
    }

    /// <summary>
    /// Make a door for every bidirectional edge in graph. Places gameobjects in parent object folder
    /// </summary>
    public void MakeDoors()
    {
        doors = new List<Door>();
        doorHolder = new GameObject("Door Holder").transform;
        foreach (var edge in gc.GetRoomEdges())
        {
            MakeDoor(edge[0], edge[1]);
        }
        foreach (var door in doors)
        {
            door.SetParent(doorHolder);
        }
    }

    public void SetGraphComp(GraphComponent graphComponent)
    {
        if (gc == null)
        {
            throw new NullReferenceException("Graph comp is null.");
        }
        gc = graphComponent;
    }

    internal void MoveRoomsToNodes()
    {
        foreach (var pair in _levelRoomByNode)
        {
            Transform from = pair.Value;
            Transform to = pair.Key.transform;
            from.position = to.position;
        }
    }
}


/// <summary>
/// A bianary tree of doors sorted by angle. Extend to sort by whatever you need
/// </summary>
public class DoorT
{
    public Door door;
    public float angle;
    public DoorT left;
    public DoorT right;
    NodeComponent ownerNode;
    public DoorT(List<Door> doors, NodeComponent nc)
    {
        ownerNode = nc;
        Stack<Door> doorStack = new Stack<Door>();
        foreach (var door in doors)
        {
            doorStack.Push(door);
        }

        Door top = doorStack.Pop();
        door = top;
        angle = getAngleOfDoorToSisterNode(top, ownerNode);
        while (doorStack.Count != 0)
        {
            top = doorStack.Pop();
            AddByAngle(this, new DoorT(top, getAngleOfDoorToSisterNode(top, ownerNode), ownerNode));
        }

    }
    public DoorT(Door _door, float _angle, NodeComponent owner)
    {
        door = _door;
        angle = _angle;
        left = null;
        right = null;
        ownerNode = owner;
    }
    /// <summary>
    /// Given a root of a tree adds doorT to a tree sorted by angle. If root is null, returns new tree
    /// Left is low, right is high angle.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="doorT"></param>
    /// <returns></returns> The root of the tree.
    public static DoorT AddByAngle(DoorT root, DoorT doorT)
    {
        //1.If the tree is empty, return a new tree with the value stored
        //2.If the root stores the same data value, return the root
        //3.If the data value is smaller than the root, update the left subtree, recursively
        //4.If the data value is larger than the root, update the right subtree recursively
        if (root == null)
        {
            return doorT;
        }
        if (doorT.angle < root.angle)
        {
            if (root.left == null)
            {
                root.left = doorT;
            }
            else
            {
                AddByAngle(root.left, doorT);
            }
        }
        else
        {
            if (root.right == null)
            {
                root.right = doorT;
            }
            else
            {
                AddByAngle(root.right, doorT);
            }
        }
        return root;

    }
    public int Count(DoorT doorT)
    {
        if (doorT == null)
        {
            return 0;
        }
        else
        {
            return 1 + Count(doorT.left) + Count(doorT.right);
        }
    }

    private static float getAngleOfDoorToSisterNode(Door door, NodeComponent ownerNode)
    {
        //  Critical. This is the reference heading from which we calculate the headings. This parameter determines the door ordering by heading
        //  Currently set to North West
        Vector3 northWest = new Vector3(-1, 1, 0).normalized;
        // Find the vec3 of owner node to the node the topDoor of the stack leads to. As if you were going from this node through a door to the next
        Vector3 nodeToNode = door.GetNeighbourNodeOfDoorFor(ownerNode).transform.position - ownerNode.transform.position;
        float angle = Utilities.PositiveCWAngleBetween(northWest, nodeToNode);
        return angle;
    }
    public static void PrintInOrder(DoorT doorTree, NodeComponent owner)
    {
        if (doorTree == null)
        {
            return;
        }
        else
        {
            PrintInOrder(doorTree.left, owner);
            Debug.Log(doorTree.door.GetNeighbourNodeOfDoorFor(owner).transform.name + " angle of: " + doorTree.angle);
            PrintInOrder(doorTree.right, owner);
        }
    }
    public static Stack<Door> ToOrderedStack(DoorT doorT, Stack<Door> ordered)
    {
        if (ordered == null)
        {
            ordered = new Stack<Door>();
        }
        if (doorT == null)
        {
            return null;
        }
        else
        {
            ToOrderedStack(doorT.right, ordered);
            ordered.Push(doorT.door);
            ToOrderedStack(doorT.left, ordered);
            return ordered;
        }
    }
    public static List<Door> ToOrderedList(DoorT doorT, List<Door> ordered)
    {
        if (ordered == null)
        {
            ordered = new List<Door>();
        }
        if (doorT == null)
        {
            return null;
        }
        else
        {
            ToOrderedList(doorT.left, ordered);
            ordered.Add(doorT.door);
            ToOrderedList(doorT.right, ordered);
            return ordered;
        }
    }
}
