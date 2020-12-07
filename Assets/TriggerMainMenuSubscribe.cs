using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMainMenuSubscribe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.TriggerOnResubscribeMainMenu(null);
    }
}