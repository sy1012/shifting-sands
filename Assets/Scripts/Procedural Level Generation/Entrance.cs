using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;
public class Entrance : Interactable
{
    GameObject interactor;
    public bool isGoalExit;
    public bool isEntrance;

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
            if (isEntrance &&
                r.GetComponent<Room>().RoomNode.symbol == GraphGrammars.Symbol.Entrance)
            {
                room = r.GetComponent<Room>();
                break;
            }
            else if (isGoalExit &&
                 r.GetComponent<Room>().RoomNode.symbol == GraphGrammars.Symbol.Goal)
            {
                room = r.GetComponent<Room>();
                break;
            }
        }
        if (room!= null)
        {
            room.PlaceObject(this);
        }
    }

    /// <summary>
    /// Start a coroutine that after x seconds (alloted time for leaving animation). An exit dungeon event is fired.
    /// </summary>
    /// <param name="interactor"></param>
    public override void Interact(GameObject interactor)
    {
        this.interactor = interactor;
        StartCoroutine(LeaveDungeon());
    }

    IEnumerator LeaveDungeon()
    {
        if (isGoalExit)
        {
            FindObjectOfType<DungeonDataKeeper>().beatLastDungeon = false;
        }
        FadeController.PlayFadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Overworld_Scot");
        EndInteraction(interactor);
    }

    public override void EndInteraction(GameObject interactor)
    {
        EventManager.TriggerDungeonExit();
    }
    private void OnDestroy()
    {
        EventManager.onDungeonGenerated -= HandleDungeonGenerated;
    }
}
