using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manages the value of the healthbar as well as its size when moused over
public class HealthBar_UI : MonoBehaviour
{
    public Slider slider;
    


    void Start()
    {

        EventManager.OnPlayerHit += EventManager_OnPlayerHit;
        EventManager.onEnteringDungeon += EventManager_onEnteringDungeon;
        PlayerStateMachine psm = FindObjectOfType<PlayerStateMachine>();
        // psm.SetHealthBar(this);
    }

    private void EventManager_onEnteringDungeon(object sender, EventManager.onEnteringDungeonEventArgs e)
    {
        SetMaxHealth(100);
    }

    private void EventManager_OnPlayerHit(System.EventArgs e)
    {
        SetHealth(25);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void SetHealth(int health)
    {
        float previous = slider.value;
        slider.value = health;
    }

}
