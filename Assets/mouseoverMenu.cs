using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseoverMenu : MonoBehaviour
{

    public Sprite moused;
    public Sprite regular;

    private void OnMouseEnter()
    {
        GetComponent<Image>().sprite = moused;
    }

    private void OnMouseExit()
    {
        GetComponent<Image>().sprite = regular;
    }
}
