using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInDungeon : MonoBehaviour
{
    public Camera cam;
    public AudioListener listenter;
    public MonoBehaviour script;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.onDungeonGenerated += EnteringDungeon;
        EventManager.OnExitDungeon += ExitingDungeon;
    }

    private void ExitingDungeon(System.EventArgs e)
    {
        cam.enabled = true;
        listenter.enabled = true;
        script.enabled = true;
    }

    private void EnteringDungeon(System.EventArgs e)
    {
        Debug.Log("hello");
        cam.enabled = false;
        listenter.enabled = false;
        script.enabled = false;
    }
}
