using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void GameEvent(EventArgs e);
    public static event GameEvent DoorEntered;
    public static event GameEvent OnExitDungeon;
    public static event EventHandler onOpenInventory;
    public static event EventHandler onCloseInventory;
    public static event EventHandler<onEnteringDungeonEventArgs> onEnteringDungeon;
    public static event GameEvent OnPlayerHit;
    public class onEnteringDungeonEventArgs : EventArgs{ public int dungeonLevel; }
    public static event GameEvent onDungeonGenerated;

    public static void TriggerDoorEntered(DoorComponent door)
    {
        Debug.Log("Door Entered");
        if (DoorEntered != null)
        {
            DoorEntered(EventArgs.Empty);
        }
    }

    public static void TriggerDungeonExit()
    {
        OnExitDungeon?.Invoke(EventArgs.Empty);
        Debug.Log("Exit Dungeon Event");
    }
    public static void TriggerEnteringDungeon(int dungeonLevel)
    {
        onEnteringDungeon?.Invoke(null, new onEnteringDungeonEventArgs { dungeonLevel = dungeonLevel });
    }

    public static void TriggerDungeonGenerated()
    {
        onDungeonGenerated?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnOpenInventory()
    {
        onOpenInventory?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnCloseInventory()
    {
        onCloseInventory?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnPlayerHit()
    {
        OnPlayerHit?.Invoke(EventArgs.Empty);
    }
}
