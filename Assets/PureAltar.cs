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
        DungeonDataKeeper.getInstance().levelsBeat += 1;

        FadeController.PlayFadeOutText("blessing");
        explosion.PlayExplosionForTime(4);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Overworld_Scot");
        EventManager.TriggerDungeonExit();
    }

    IEnumerator LockPlayer(Transform player, Vector3 LockPosition)
    {
        float tick = 0;
        while (tick < 3.6f)
        {
            tick += Time.deltaTime;
            player.position = LockPosition;
            yield return new WaitForEndOfFrame();
        }
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
        EventManager.TriggerOnPureAltar();
        DungeonDataKeeper.getInstance().blessingValue = DungeonDataKeeper.getInstance().blessingValue * 0.5f;
        StartCoroutine(LeaveDungeon());
        StartCoroutine(LockPlayer(player.transform, player.transform.position));
    }

    private void Start()
    {
        flameRoot.localScale = DungeonDataKeeper.getInstance().blessingValue * Vector3.one;
        explosion = GetComponentInChildren<FlameExplosion>();
    }
}
