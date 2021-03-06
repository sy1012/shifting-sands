﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manages the value of the healthbar as well as its size when moused over
public class Healthbar : MonoBehaviour
{
    public Slider slider;
    private Transform canvasTransform;
    private Character character;

    private void Start()
    {
        canvasTransform = GetComponentInParent<RectTransform>(); //RectTransform of the UI Canvas
        character = GetComponentInParent<Character>();
        canvasTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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

    private void Update()
    {
        //Enlarge healthbar when enemy moused over
        if (GetComponentInParent<PlayerStateMachine>() == null)
        {

            if (character.isMousedOver)
            {
                canvasTransform.localScale = Vector3.Lerp(canvasTransform.localScale, new Vector3(0.5f, 0.5f, 0.5f), 10 * Time.deltaTime);
            }
            else
            {
                canvasTransform.localScale = Vector3.Lerp(canvasTransform.localScale, new Vector3(0.3f, 0.3f, 0.3f), 10 * Time.deltaTime);

            }
        }
    }

}
