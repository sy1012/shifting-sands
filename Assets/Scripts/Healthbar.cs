using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manages the value of the healthbar as well as its size when moused over
public class Healthbar : MonoBehaviour
{
    public Slider slider;
    private Transform canvasTransform;
    private Enemy enemy;

    private void Start()
    {
        canvasTransform = GetComponentInParent<RectTransform>(); //RectTransform of the UI Canvas
        enemy = GetComponentInParent<Enemy>();
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
        if (enemy.isMousedOver)
        {
            canvasTransform.localScale = Vector3.Lerp(canvasTransform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 10 * Time.deltaTime);
        }
        else
        {
            canvasTransform.localScale = Vector3.Lerp(canvasTransform.localScale, new Vector3(1f, 1f, 1f), 10 * Time.deltaTime);
        }
    }

}
