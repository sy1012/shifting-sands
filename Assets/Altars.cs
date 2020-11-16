using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Purpose is to place the altars at the goal room
/// </summary>
public class Altars : MonoBehaviour
{
    public void Awake()
    {
        EventManager.onDungeonGenerated += HandleDungeonGenerated;

    }

    /// <summary>
    /// Positions the entrance at either the entrance or goal depending on this components state after the dungeon has been generated
    /// </summary>
    /// <param name="e"></param> DungeonGenArgs
    private void HandleDungeonGenerated(System.EventArgs e)
    {
        //Cast to correct args type
        DungeonGenArgs de = (DungeonGenArgs)e;
        //Find Entrance Room if isEntrance
        Room room = null;

        foreach (Transform r in de.Rooms)
        {
            if (r.GetComponent<Room>().RoomNode.symbol == GraphGrammars.Symbol.Goal)
            {
                room = r.GetComponent<Room>();
                break;
            }
        }
        if (room != null)
        {
            transform.position = room.spawnLocations[0].position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    private void OnDestroy()
    {
        EventManager.onDungeonGenerated -= HandleDungeonGenerated;
    }
}
