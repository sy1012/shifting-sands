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
    //public static event EventHandler onDungeonInventoryTrigger;
    public static event EventHandler onWeaponMerchant;
    public static event EventHandler onArmourMerchant;
    public static event EventHandler onRuneMerchant;
    public static event EventHandler onCrafting;
    public static event EventHandler onOpenInventory;
    public static event EventHandler onCloseInventory;
    public static event GameEvent onCoinPickedUp;
    public static event EventHandler<onEnteringDungeonEventArgs> onEnteringDungeon;
    public static event GameEvent OnPlayerHit;
    public static event GameEvent OnCastFireball;
    public static event GameEvent onAttack;
    public static event GameEvent onFireballCollision;
    public static event GameEvent onPlayerMovement;
    public static event GameEvent onUseShrine;
    public static event GameEvent onDash;
    public static event GameEvent onDungeonGenerated;
    public static event GameEvent onCraftingMade;
    public static event EventHandler onOverworldStart;
    public static event EventHandler onNewOasis;
    public static event EventHandler onOasisClicked;
    public static event EventHandler onPyramidClicked;
    public static event EventHandler onRoomFilled;
    public static event GameEvent OnCastSnowball;
    public static event GameEvent onSnowballCollision;
    public static event GameEvent onRelicCollected;
    public static event GameEvent onPlayerEnteredRoom;
    public static event GameEvent onMagicFailure;
    public static event GameEvent onArmorChange;
    public static event GameEvent onWeaponChange;
    public static event GameEvent onRuneChange;
    public static event GameEvent onInventorySwap;
    public static event GameEvent onBuy;
    public static event GameEvent onScarabAgro;
    public static event GameEvent onSkullAgro;
    public static event GameEvent onMummyAgro;
    public static event GameEvent onEnemyDeath;
    public static event GameEvent onPlayerDeath;
    public static event GameEvent onResubscribeOverworld;  // DO NOT CLEAN THESE
    public static event GameEvent onResubscribeDungeon;    // THEY SET UP RESUBSCIBING 
    public static event GameEvent onResubscribeMainMenu;   // AND ARE FOR PERMANANT OBJS ONLY
    // !!!!!!!!!!!!!!!!!! BENNNN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public static event GameEvent onSell;
    // IF YOU ARE MAKING A NEW GAMEEVENT,MAKE SURE YOU CLEAN IT IN TRIGGERDUNGEONEXIT
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public static GameEvent[] gameEvents = {DoorEntered, OnExitDungeon,OnPlayerHit,OnCastFireball,onAttack,onFireballCollision,onPlayerMovement,
                                            onUseShrine,onDash,onDungeonGenerated, onRelicCollected,onPlayerEnteredRoom};

    public class onEnteringDungeonEventArgs : EventArgs{ public int dungeonLevel; }
    public class onRoomFilledArgs : EventArgs{ public Room room; public List<MonoBehaviour> prefabs; }
    public static void TriggerDungeonExit()
    {
        //Clear the persisting Loot list
        DungeonMaster.loot = new List<GameObject>();

        //Clean the Game Events
        onScarabAgro = null;
        onSkullAgro = null;
        onMummyAgro = null;
        onRoomFilled = null;
        DoorEntered = null;
        OnPlayerHit = null;
        OnCastFireball = null;
        onAttack = null;
        onFireballCollision = null;
        onPlayerMovement = null;
        onUseShrine = null;
        onDash = null;
        onDungeonGenerated = null;
        OnCastSnowball = null;
        onSnowballCollision = null;
        onRelicCollected = null;
        onPlayerEnteredRoom = null;
        onMagicFailure = null;
        onArmorChange = null;
        onWeaponChange = null;
        onRuneChange = null;
        onInventorySwap = null;
        onBuy = null;
        onCraftingMade = null;
        onSell = null;
        onOpenInventory = null;
        onCloseInventory = null;
        onCoinPickedUp = null;
        onCraftingMade = null;
        onRuneChange = null;
        OnExitDungeon = null;
        onEnemyDeath = null;
        onPlayerDeath = null;
    }

    public static void TriggerDoorEntered(DoorComponent door)
    {
        if (DoorEntered != null)
        {
            DoorEntered(EventArgs.Empty);
        }
    }

    public static void TriggerOnResubscribeDungeon(DungeonGenArgs e)
    {
        onResubscribeDungeon?.Invoke(e);
    }

    public static void TriggerOnResubscribeOverworld(DungeonGenArgs e)
    {
        onResubscribeOverworld?.Invoke(e);
    }

    public static void TriggerOnResubscribeMainMenu(DungeonGenArgs e)
    {
        onResubscribeMainMenu?.Invoke(e);
    }

    public static void TriggerDungeonGenerated(DungeonGenArgs e)
    {
        onDungeonGenerated?.Invoke(e);
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

    public static void TriggerOnCoinPickedUp()
    {
        onCoinPickedUp?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnCraftingMade()
    {
        onCraftingMade?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnPlayerHit()
    {
        OnPlayerHit?.Invoke(EventArgs.Empty);
    }
    
    public static void TriggerOnInventoryTrigger()
    {
        onInventoryTrigger?.Invoke(null, EventArgs.Empty);
    }

    //public static void TriggerOnDungeonInventoryTrigger()
    //{
    //    onDungeonInventoryTrigger?.Invoke(null, EventArgs.Empty);
    //}

    public static void TriggerOnWeaponMerchant()
    {
        onWeaponMerchant?.Invoke(null, EventArgs.Empty);
    }

    public static void TriggerOnArmourMerchant()
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

    public static void TriggerOnCastSnowball()
    {
        OnCastSnowball?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnSnowballCollison()
    {
        onSnowballCollision?.Invoke(EventArgs.Empty);
    }

    public class RelicEventArgs : EventArgs { public Interactable relic; }
    public static void TriggerRelicGathered(Interactable relic)
    {
        var args = new RelicEventArgs();
        args.relic = relic;
        onRelicCollected?.Invoke(args);
    }
    public class PlayerEnterRoomArgs : EventArgs { public Room room; }
    public static void TriggerPlayerEneteredRoom(Room room)
    {
        var args = new PlayerEnterRoomArgs();
        args.room = room;
        onPlayerEnteredRoom?.Invoke(args);
    }

    public static void TriggerOnMagicFailure()
    {
        onMagicFailure?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnArmorChange()
    {
        onArmorChange?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnWeaponChange()
    {
        onWeaponChange?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnRuneChange()
    {
        onRuneChange?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnInventorySwap()
    {
        onInventorySwap?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnBuy()
    {
        onBuy?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnSell()
    {
        onSell?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnScarabAgro()
    {
        onScarabAgro?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnSkullAgro()
    {
        onSkullAgro?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnMummyAgro()
    {
        onMummyAgro?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnEnemyDeath()
    {
        onEnemyDeath?.Invoke(EventArgs.Empty);
    }

    public static void TriggerOnPlayerDeath()
    {
        onPlayerDeath?.Invoke(EventArgs.Empty);
    }
}

