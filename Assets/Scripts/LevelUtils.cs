using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;

public static class LevelUtils
{
    public static Transform FindEntrance(Graph g, List<Transform> rooms)
    {
        Node ent = null;
        //Linear search for entrance node
        foreach (var node in g.GetNodes)
        {
            if (node.GetSymbol == Symbol.Entrance)
            {
                ent = node;
            }
        }
        if (ent == null)
        {
            throw new System.Exception("Error. No entrance node found in graph");
        }
        //Find transform that matches node
        Room curRoom;
        foreach (var room in rooms)
        {
            curRoom = room.GetComponent<Room>();
            //Sanity check
            if (curRoom == null){throw new System.Exception("Transform should have a room. Does it's children?");}
            if (curRoom.RoomNode.AssignedNode == ent)
            {
                return curRoom.transform;
            }

        }
        throw new System.Exception("No matching entrance found between graph entrance and rooms list");
    }
}
