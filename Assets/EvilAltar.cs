using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilAltar : Interactable
{
    // Children in hierachry refs
    [SerializeField]
    Transform flameRoot;
    FlameExplosion explosion;

    IEnumerator LeaveDungeon()
    {
        DungeonDataKeeper.getInstance().beatLastDungeon = true;

        FadeController.PlayFadeOutText("curse");

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

        player.animator.Play("Fall");

        DungeonDataKeeper.getInstance().curseValue = DungeonDataKeeper.getInstance().curseValue * 0.5f;
        StartCoroutine(LeaveDungeon());
    }

    private void Start()
    {
        flameRoot.localScale = DungeonDataKeeper.getInstance().curseValue * Vector3.one;
        explosion = GetComponentInChildren<FlameExplosion>();
    }
}
