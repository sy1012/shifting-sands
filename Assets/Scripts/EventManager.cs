using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void GameEvent(EventArgs e);
    public static event GameEvent DoorEntered;
    // Start is called before the first frame update
    public static void TriggerDoorEntered(Door door)
    {
        if (DoorEntered != null)
        {
            DoorEntered(EventArgs.Empty);
        }
    }
}
