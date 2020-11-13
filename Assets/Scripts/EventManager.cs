using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EventManager
{
    public delegate void GameEvent(EventArgs e);
    public static event GameEvent DoorEntered;
    public static event GameEvent OnExitDungeon;
    public static event EventHandler onInventoryTrigger;
    public static event EventHandler onWeaponMerchant;
    public static event EventHandler onArmourMerchant;
    public static event EventHandler onRuneMerchant;
    public static event EventHandler onCrafting;
    public static event EventHandler onOpenInventory;
    public static event EventHandler onCloseInventory;
    public static event EventHandler<onEnteringDungeonEventArgs> onEnteringDungeon;
    public static event GameEvent OnPlayerHit;
    public static event GameEvent OnCastFireball;
    public static event GameEvent onAttack;
    public static event GameEvent onFireballCollision;
    public static event GameEvent onPlayerMovement;
    public static event GameEvent onUseShrine;
    public static event GameEvent onDash;
    public static event GameEvent onDungeonGenerated;
    public static event EventHandler onOverworldStart;
    public static event EventHandler onNewOasis;
    public static event EventHandler onOasisClicked;
    public static event EventHandler onPyramidClicked;
    public static event EventHandler onRoomFilled;
    // IF YOU ARE MAKING A NEW GAMEEVENT,MAKE SURE YOU CLEAN IT IN TRIGGERDUNGEONEXIT

    public static GameEvent[] gameEvents = {DoorEntered, OnExitDungeon,OnPlayerHit,OnCastFireball,onAttack,onFireballCollision,onPlayerMovement,
                                            onUseShrine,onDash,onDungeonGenerated};

    public class onEnteringDungeonEventArgs : EventArgs{ public int dungeonLevel; }
    public class onRoomFilledArgs : EventArgs{ public Room room; public List<MonoBehaviour> prefabs; }
    public static void TriggerDungeonExit()
    {
        OnExitDungeon?.Invoke(EventArgs.Empty);
        Debug.Log("Exit Dungeon Event");
        //Clean the Game Events
        onRoomFilled = null;
        DoorEntered = null;
        OnExitDungeon = null;
        OnPlayerHit = null;
        OnCastFireball = null;
        onAttack = null;
        onFireballCollision = null;
        onPlayerMovement = null;
        onUseShrine = null;
        onDash = null;
        onDungeonGenerated = null;
    }

    public static void TriggerDoorEntered(DoorComponent door)
    {
        Debug.Log("Door Entered");
        if (DoorEntered != null)
        {
            DoorEntered(EventArgs.Empty);
        }
    }

    public static void TriggerDungeonGenerated(DungeonGenArgs e)
    {
        onDungeonGenerated?.Invoke(e);
        Debug.Log("Dungeon Generated");
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
    
    public static void TriggerOnInventoryTrigger()
    {
        onInventoryTrigger?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnWeaponMerchant()
    {
        onWeaponMerchant?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnArmournMerchant()
    {
        onArmourMerchant?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnRuneMerchant()
    {
        onRuneMerchant?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnCrafting()
    {
        onCrafting?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnCastFireball()
    {
        OnCastFireball?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnAttack()
    {
        onAttack?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnFireballCollison()
    {
        onFireballCollision?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnPlayerMovement()
    {
        onPlayerMovement?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnUseShrine()
    {
        onUseShrine?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnDash()
    {
        onDash?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnOverworldStart()
    {
        onOverworldStart?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnNewOasis()
    {
        onNewOasis?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnOasisClicked()
    {
        onOasisClicked?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnPyramidClicked()
    {
        onPyramidClicked?.Invoke(null, EventArgs.Empty);
    }
    public static void TriggerRoomFilled(Room room, List<MonoBehaviour> prefabs)
    {
        onRoomFilledArgs filledArgs = new onRoomFilledArgs();
        filledArgs.room = room;
        filledArgs.prefabs = prefabs;
        onRoomFilled?.Invoke(null,filledArgs);
    }
}

