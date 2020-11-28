using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PureAltar : Interactable
{
    // Children in hierachry refs
    [SerializeField]
    Transform flameRoot;
    FlameExplosion explosion;

    IEnumerator LeaveDungeon()
    {
        DungeonDataKeeper.getInstance().beatLastDungeon = true;
        FadeController.PlayFadeOutText("blessing");
        explosion.PlayExplosionForTime(4);
        yield return new WaitForSeconds(4f);
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
        DungeonDataKeeper.getInstance().blessingValue = DungeonDataKeeper.getInstance().blessingValue * 0.5f;
        StartCoroutine(LeaveDungeon());
    }

    private void Start()
    {
        flameRoot.localScale = DungeonDataKeeper.getInstance().blessingValue * Vector3.one;
        explosion = GetComponentInChildren<FlameExplosion>();
    }
}
