using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    public bool load;

    private void Start()
    {
        // need to use an event that doesnt get wiped between scenes
        EventManager.onResubscribeOverworld += DeliverResult;
    }

    private void DeliverResult(System.EventArgs e)
    {
        // Tell something if we should load a game or start a new one
        GameObject.Find("something");
    }
}
