using System.Collections;
using UnityEngine;
//using UnityEditor;
public class Entrance : Interactable
{
    GameObject interactor;
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
        yield return new WaitForSeconds(1f);
        EndInteraction(interactor);
    }

    public override void EndInteraction(GameObject interactor)
    {
        EventManager.TriggerDungeonExit();
    }
}
