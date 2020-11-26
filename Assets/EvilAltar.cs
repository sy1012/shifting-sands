using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilAltar : Interactable
{
    IEnumerator LeaveDungeon()
    {
        DungeonDataKeeper.getInstance().beatLastDungeon = true;
        FadeController.PlayFadeOut();
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Overworld_Scot");
        EventManager.TriggerDungeonExit();
    }
    public override void Interact(GameObject interactor)
    {
        //  Validate interactor. Sanity check
        var player = interactor.GetComponent<PlayerStateMachine>();
        if (player == null)
        {
            // Interactor must be a player
            return;
        }
        EventManager.TriggerRelicGathered(this);
        StartCoroutine(LeaveDungeon());
    }
}
