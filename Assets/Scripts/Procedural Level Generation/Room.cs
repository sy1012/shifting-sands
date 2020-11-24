using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Transform> doorTransforms;
    public List<Door> roomDoors;
    //"NSW" would map to a door at north,south, and west
    public string doorHeadings;
    [SerializeField]
    private NodeComponent roomNode;
    public List<Transform> spawnLocations;
    public NodeComponent RoomNode{ get { return roomNode; } set { roomNode = value; } }


    public void PlaceObject(MonoBehaviour prefab)
    {
        Transform randomSpawnLocation = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Count)];
        prefab.transform.position = new Vector3(randomSpawnLocation.position.x, randomSpawnLocation.position.y,0);
        //Parent object to spawn location. Not needed.Only to clean up hierarchy. Also if a room is destroyed the enemy should be too
        prefab.transform.SetParent(randomSpawnLocation);
    }

    public void PlaceObject(GameObject prefab)
    {
        Transform randomSpawnLocation = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Count)];
        prefab.transform.position = new Vector3(randomSpawnLocation.position.x, randomSpawnLocation.position.y, 0);
        //Parent object to spawn location. Not needed.Only to clean up hierarchy. Also if a room is destroyed the enemy should be too
        prefab.transform.SetParent(randomSpawnLocation);
    }

    public List<Transform> OrderDoorsByHeading()
    {
        LinkedList<Transform> northDoors = new LinkedList<Transform>();
        LinkedList<Transform> eastDoors = new LinkedList<Transform>();
        LinkedList<Transform> southDoors = new LinkedList<Transform>();
        LinkedList<Transform> westDoors = new LinkedList<Transform>();
        foreach (var door in doorTransforms)
        {
            if (door.up == Vector3.up)
            {
                OrderDoorsUtil(northDoors, door, Vector3.left);
            }
            else if (door.up == Vector3.right)
            {
                OrderDoorsUtil(eastDoors, door, Vector3.up);
            }
            else if (door.up == Vector3.down)
            {
                OrderDoorsUtil(southDoors, door, Vector3.right);
            }
            else if (door.up == Vector3.left)
            {
                OrderDoorsUtil(westDoors, door, Vector3.down);
            }
        }
        LinkedList<Transform> ordered = new LinkedList<Transform>();
        foreach (var d in northDoors)
        {
            ordered.AddLast(d);
        }
        foreach (var d in eastDoors)
        {
            ordered.AddLast(d);
        }
        foreach (var d in southDoors) 
        {
            ordered.AddLast(d);
        }
        foreach (var d in westDoors)
        {
            ordered.AddLast(d);
        }
        /*Logging
        string result = "Room" + name+ " doors in order: ";
        foreach (var item in ordered)
        {
            result += item.name + " ,";
        }
        Debug.Log(result);
        End Logging*/
        return new List<Transform>(ordered); 
    }
    private void OrderDoorsUtil(LinkedList<Transform> ordered, Transform door, Vector3 refHeading)
    {
        if (ordered.Count == 0)
        {
            ordered.AddFirst(door);
            return;
        }
        LinkedListNode<Transform> walker = ordered.First;

        while (walker !=null)
        {
            //  Critical. This is the reference heading from which we calculate the headings. This parameter determines the door ordering by heading
            // Find the vec3 of owner node to the node the topDoor of the stack leads to. As if you were going from this node through a door to the next
            Vector3 doorVecFromCentroid = door.transform.position-transform.position;
            Vector3 headVecFromCentroid = walker.Value.transform.position-transform.position;
            
            float doorAngle = Utilities.PositiveCWAngleBetween(refHeading, doorVecFromCentroid);
            float walkerAngle = Utilities.PositiveCWAngleBetween(refHeading, headVecFromCentroid);
            if (doorAngle>walkerAngle)
            {
                if (walker.Next == null)
                {
                    ordered.AddLast(door);
                    return;
                }
                walker = walker.Next;
            }
            else
            {
                ordered.AddBefore(walker, door);
                return;
            }
        }
    }

    public void MatchUpDoors(List<Door> doors)
    {
        roomDoors = doors;
        doorTransforms = OrderDoorsByHeading();
        for (int i = 0; i < doors.Count; i++)
        {
            var door = doors[i];
            door.AssignRoomDoorTr(doorTransforms[i],roomNode);
        }
    }

    // -------------------Door Lock and Open Logic -----------------------------------//

    public List<MonoBehaviour> enemies;
    bool locked = false;
    bool roomCleared;
    private void HandlePlayerEnteredThisRoom(EventArgs e)
    {
        if (enemies.Count != 0)
        {
            LockDoors();
        }
    }

    void LockDoors()
    {
        SpriteSelector[] doorSpriteSelectors = GetComponentsInChildren<SpriteSelector>();
        foreach (SpriteSelector ss in doorSpriteSelectors)
        {
            ss.SetLocked();
        }
    }
    void UnlockDoors()
    {
        SpriteSelector[] doorSpriteSelectors = GetComponentsInChildren<SpriteSelector>();
        foreach (SpriteSelector ss in doorSpriteSelectors)
        {
            ss.SetUnlocked();
        }
    }


    //-----------------------End Lock and Open Logic----------------------------------//

    private void Awake()
    {
        enemies = new List<MonoBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.onPlayerEnteredRoom += HandlePlayerEnteredThisRoom;
    }


    // Update is called once per frame
    void Update()
    {
        enemies.RemoveAll(item => item == null);
        if (!roomCleared && enemies.Count == 0)
        {
            UnlockDoors();
            roomCleared = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Color[] colors = new Color[] { Color.blue, Color.green, Color.cyan, Color.magenta };
        int i = 0;
        foreach (var door in roomDoors)
        {
            Gizmos.color = colors[i];
            foreach (var doorTr in door.GetDoorTransforms())
            {
                Gizmos.DrawCube(doorTr.position,Vector3.one*0.5f);
            }
            i++;
        }
    }
}
