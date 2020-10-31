using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void GameEvent(EventArgs e);
    public static event GameEvent onDoorEntered;
    public static event GameEvent onExitDungeon;
    public static event EventHandler onPlayerMovement;
    public static event EventHandler<onEnemyEventArgs> onEnemyMovement;
    public class onEnemyEventArgs : EventArgs { public string enemyName; }
    public static event EventHandler onOpenInventory;
    public static event EventHandler onCloseInventory;
    public static event EventHandler onPlayerHit;
    public static event EventHandler<onEnemyEventArgs> onEnemyHit;
    public static event EventHandler onPlayerHealed;
    public static event EventHandler onPlayerDashed;
    public static event EventHandler onWallHit;
    public static event EventHandler onAttack;
    public static event EventHandler onCoinsDrop;
    public static event EventHandler onOtherLootDrop;
    public static event EventHandler<onEnemyEventArgs> onEnemyMakesSound;
    public static event EventHandler<onBreakableDestroyedEventArgs> onBreakableDestroyed;
    public class onBreakableDestroyedEventArgs : EventArgs { public string breakableName; }
    public static event EventHandler<onEnteringDungeonEventArgs> onEnteringDungeon;
    public class onEnteringDungeonEventArgs : EventArgs{ public int dungeonLevel; }
    public static event GameEvent onDungeonGenerated;

    public static void TriggerDoorEntered(DoorComponent door)
    {
        Debug.Log("Door Entered");
        if (onDoorEntered != null)
        {
            onDoorEntered(EventArgs.Empty);
        }
    }

    public static void TriggerDungeonExit()
    {
        onExitDungeon?.Invoke(EventArgs.Empty);
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
        onPlayerHit?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnEnemeyHit(string enemyName)
    {
        onEnemyHit?.Invoke(null, new onEnemyEventArgs { enemyName = enemyName });
    }

    public static void TriggerOnPlayerHealed()
    {
        onPlayerHealed?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnPlayerDashed()
    {
        onPlayerDashed?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnCoinsDrop()
    {
        onCoinsDrop?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnAttack()
    {
        onAttack?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnOtherLootDrop()
    {
        onOtherLootDrop?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnEnemyMakesSound(string enemyName)
    {
        onEnemyMakesSound?.Invoke(null, new onEnemyEventArgs { enemyName = enemyName });
    }
    

    public static void TriggerOnEnemyMovement(string enemyName)
    {
        onEnemyMovement?.Invoke(null, new onEnemyEventArgs { enemyName = enemyName });
    }

    public static void TriggerOnBreakableDestroyed(string enemyName)
    {
        onEnemyMovement?.Invoke(null, new onEnemyEventArgs { enemyName = enemyName });
    }
}
