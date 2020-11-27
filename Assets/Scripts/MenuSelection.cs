using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    private static MenuSelection instance = null;

    public bool load = false;

    public static MenuSelection GetInstance()
    {
        if (instance == null)
        {
            instance = new GameObject("MenuSelection").AddComponent<MenuSelection>();
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

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
