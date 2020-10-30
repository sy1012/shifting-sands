using UnityEngine;
//using UnityEditor;
public abstract class Interactable:MonoBehaviour
{
    protected int priority;
    protected KeyCode key;
    public virtual void Interact(GameObject interactor)
    {
        return;
    }

    public virtual void EndInteraction(GameObject interactor)
    {
        return;
    }
}

/*
[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RoomGenerator roomGenerator = (RoomGenerator)target;
        if (GUILayout.Button("Test 'Make Doors'"))
        {
            roomGenerator.MakeDoors();
        }
        if (GUILayout.Button("Test 'Make Rooms'"))
        {
            roomGenerator.MakeDoors();
            roomGenerator.MakeRooms();
            roomGenerator.MoveRoomsToNodes();
        }
        if (GUILayout.Button("Test 'Door Tree'"))
        {
            roomGenerator.MakeDoors();
            NodeComponent node = Selection.gameObjects[0].GetComponent<NodeComponent>();
            DoorT doorT = new DoorT(roomGenerator.GetNodeDoors(node),node);
            if (doorT == null)
            {
                throw new ArgumentException("Tree of doors not made");
            }
            Stack<Door> orderedDoors = DoorT.ToOrderedStack(doorT,null);
            while (orderedDoors.Count != 0)
            {
                var doort = orderedDoors.Pop();
                Debug.Log(doort.GetNeighbourNodeOfDoorFor(node).transform.name);
            }
        }
    }
}
*/