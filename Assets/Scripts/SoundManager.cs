﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // manages all the audio clips
    private AudioSource SoundPlayer;
    // make a clip for each sound effect

    // anubis 
    private AudioClip AnubisAttack;
    private AudioClip AnubisDeath;
    private AudioClip AnubisMusic;

    // ross
    private AudioClip RossAttack1;
    private AudioClip RossAttack2;
    private AudioClip RossAttack3;
    private AudioClip RossDeath;
    private AudioClip RossMusic;

    // reveal overworld
    private AudioClip Reveal;

    // end shrine fire
    private AudioClip Endfire1;
    private AudioClip Endfire2;

    // column destruction
    private AudioClip Crumble1;
    private AudioClip Crumble2;
    private AudioClip Crumble3;

    // caravan hub music
    private AudioClip CaravanHub;

    // dungeon music
    private AudioClip DungeonMusic;

    // overworld music
    private AudioClip OverworldMusic;

    // inventory armor
    private AudioClip Armor1;
    private AudioClip Chainmail1;
    private AudioClip Chainmail2;

    // inventory beads
    private AudioClip Beads1;
    
    // inventory belt
    private AudioClip Belt1;
    private AudioClip Belt2;
    private AudioClip Belt3;
    private AudioClip Belt4;

    // inventory book
    private AudioClip BookClose;
    private AudioClip BookFlip1;
    private AudioClip BookFlip2;
    private AudioClip BookFlip3;
    private AudioClip BookOpen;
    private AudioClip BookPlace1;
    private AudioClip BookPlace2;
    private AudioClip BookPlace3;

    // inventory bottle
    private AudioClip Bottle1;

    // inventory clicks
    private AudioClip Click1;
    private AudioClip Click2;
    private AudioClip Click3;
    private AudioClip Click4;
    private AudioClip Click5;

    // inventory cloth
    private AudioClip Cloth1;
    private AudioClip Cloth2;
    private AudioClip Cloth3;
    private AudioClip Cloth4;
    private AudioClip Cloth5;
    private AudioClip Cloth6;
    private AudioClip Cloth7;
    private AudioClip Cloth8;
    private AudioClip Cloth9;

    // inventory weapon
    private AudioClip Weapon1;
    private AudioClip Weapon2;
    private AudioClip Weapon3;
    private AudioClip Weapon4;
    private AudioClip Weapon5;

    // metal/wood
    private AudioClip Metal1;
    private AudioClip Metal2;
    private AudioClip Metal3;
    private AudioClip Wood1;

    // breakable
    private AudioClip Break1;
    private AudioClip Break2;
    private AudioClip Break3;
    private AudioClip Break4;
    private AudioClip Break5;

    // swings
    private AudioClip Swing1;
    private AudioClip Swing2;
    private AudioClip Swing3;
    private AudioClip Swing4;
    private AudioClip Swing5;

    // enemy swings
    private AudioClip EnemySwing1;

    // door
    private AudioClip Door1;
    private AudioClip Door2;
    private AudioClip Door3;
    private AudioClip Door4;
    private AudioClip Door5;
    private AudioClip Door6;
    private AudioClip Door7;

    // shrine healing
    private AudioClip Shrine1;

    // coins
    private AudioClip Coin1;
    private AudioClip Coin2;
    private AudioClip Coin3;
    private AudioClip Coin4;
    private AudioClip Coin5;

    // enemies
    private AudioClip Monster1;
    private AudioClip Monster2;
    private AudioClip Monster3;
    private AudioClip Monster4;
    private AudioClip Monster5;
    private AudioClip Monster6;
    private AudioClip Monster7;
    private AudioClip Monster8;
    private AudioClip Monster9;
    private AudioClip Monster10;
    private AudioClip Monster11;
    private AudioClip Monster12;
    private AudioClip Monster13;
    private AudioClip Monster14;
    private AudioClip Monster15;
    private AudioClip Bite1;
    private AudioClip Bite2;
    private AudioClip Bite3;
    private AudioClip Giant1;
    private AudioClip Giant2;
    private AudioClip Giant3;
    private AudioClip Giant4;
    private AudioClip Giant5;
    private AudioClip Orge1;
    private AudioClip Orge2;
    private AudioClip Orge3;
    private AudioClip Orge4;
    private AudioClip Orge5;
    private AudioClip Shade1;
    private AudioClip Shade2;
    private AudioClip Shade3;
    private AudioClip Shade4;
    private AudioClip Shade5;
    private AudioClip Shade6;
    private AudioClip Shade7;
    private AudioClip Shade8;
    private AudioClip Shade9;
    private AudioClip Shade10;
    private AudioClip Shade11;
    private AudioClip Shade12;
    private AudioClip Shade13;
    private AudioClip Shade14;
    private AudioClip Shade15;
    private AudioClip Slime1;
    private AudioClip Slime2;
    private AudioClip Slime3;
    private AudioClip Slime4;
    private AudioClip Slime5;
    private AudioClip Slime6;
    private AudioClip Slime7;
    private AudioClip Slime8;
    private AudioClip Slime9;
    private AudioClip Slime10;
    private AudioClip WolfMan1;

    // player hit
    private AudioClip PlayerHit1;
    private AudioClip PlayerPain1;
    private AudioClip PlayerPain2;
    private AudioClip PlayerPain3;
    private AudioClip PlayerPain4;
    private AudioClip PlayerPain5;
    private AudioClip PlayerPain6;
    private AudioClip PlayerPain7;
    private AudioClip PlayerPain8;

    // player death
    private AudioClip PlayerDeath1;
    private AudioClip PlayerDeath2;

    // footsteps
    private AudioClip Footstep1;
    private AudioClip Footstep2;
    private AudioClip Footstep3;
    private AudioClip Footstep4;
    private AudioClip Footstep5;
    private AudioClip Footstep6;
    private AudioClip Footstep7;
    private AudioClip Footstep8;
    private AudioClip Footstep9;
    private AudioClip Footstep10;

    // magic
    private AudioClip Fire;
    private AudioClip FireExplosion;
    private AudioClip Snowball;
    private AudioClip MagicFail1;
    private AudioClip MagicFail2;
    private AudioClip SnowBallExplosion1;
    private AudioClip SnowBallExplosion2;

    // crafting
    private AudioClip Craft;

    // dash
    private AudioClip Dash1;
    private AudioClip Dash2;

    // enemy hit
    private AudioClip EnemyHit1;

    // to allow use in other scripts
    public static SoundManager current;

    void Start(){
        EventManager.onResubscribeOverworld += EnteringOverworld;
        EventManager.onResubscribeDungeon += EnteringDungeon;
        EventManager.onResubscribeMainMenu += EnteringMainMenu;

        // initialize sound manager
        current = this;
        SoundPlayer = this.gameObject.AddComponent<AudioSource>();
        SoundPlayer.loop = true;
        SoundPlayer.volume = 1.0f;

        GameObject.DontDestroyOnLoad(this.gameObject);

        // initialize every audio clip

        // anubis
        AnubisAttack = Resources.Load<AudioClip>("SFX/Boss/anubis-attack");
        AnubisDeath = Resources.Load<AudioClip>("SFX/Boss/anubis-death");
        AnubisMusic = Resources.Load<AudioClip>("SFX/Boss/anubis-music");

        // ross
        RossAttack1 = Resources.Load<AudioClip>("SFX/Boss/ross-attack1");
        RossAttack2 = Resources.Load<AudioClip>("SFX/Boss/ross-attack2");
        RossAttack3 = Resources.Load<AudioClip>("SFX/Boss/ross-attack3");
        RossDeath = Resources.Load<AudioClip>("SFX/Boss/ross-start-death");
        RossMusic = Resources.Load<AudioClip>("SFX/Boss/ross-music");

        // reveal overworld
        Reveal = Resources.Load<AudioClip>("SFX/Overworld/reveal-oasis");

        // end shrine fire
        Endfire1 = Resources.Load<AudioClip>("SFX/Dungeon/endfire1");
        Endfire2 = Resources.Load<AudioClip>("SFX/Dungeon/endfire2");

        // column destruction
        Crumble1 = Resources.Load<AudioClip>("SFX/Dungeon/crumble1");
        Crumble2 = Resources.Load<AudioClip>("SFX/Dungeon/crumble2");
        Crumble3 = Resources.Load<AudioClip>("SFX/Dungeon/crumble3");

        // caravan hub music
        CaravanHub = Resources.Load<AudioClip>("SFX/Caravan/CaravanHub");

        // dungeon music
        DungeonMusic = Resources.Load<AudioClip>("SFX/Dungeon/ThisUsedToBeACity");

        // overworld music
        OverworldMusic = Resources.Load<AudioClip>("SFX/Overworld/WINDY");

        // inventory armor
        Armor1 = Resources.Load<AudioClip>("SFX/Inventory/armor-light");
        Chainmail1 = Resources.Load<AudioClip>("SFX/Inventory/chainmail1");
        Chainmail2 = Resources.Load<AudioClip>("SFX/Inventory/chainmail2");

        // inventory beads
        Beads1 = Resources.Load<AudioClip>("SFX/Inventory/beads");

        // inventory belt
        Belt1 = Resources.Load<AudioClip>("SFX/Inventory/beltHandle1");
        Belt2 = Resources.Load<AudioClip>("SFX/Inventory/beltHandle2");
        Belt3 = Resources.Load<AudioClip>("SFX/Inventory/clothBelt");
        Belt4 = Resources.Load<AudioClip>("SFX/Inventory/clothBelt2");

        // inventory book
        BookClose = Resources.Load<AudioClip>("SFX/Inventory/bookClose");
        BookFlip1 = Resources.Load<AudioClip>("SFX/Inventory/bookFlip1");
        BookFlip2 = Resources.Load<AudioClip>("SFX/Inventory/bookFlip2");
        BookFlip3 = Resources.Load<AudioClip>("SFX/Inventory/bookFlip3");
        BookOpen = Resources.Load<AudioClip>("SFX/Inventory/bookOpen");
        BookPlace1 = Resources.Load<AudioClip>("SFX/Inventory/bookPlace1");
        BookPlace2 = Resources.Load<AudioClip>("SFX/Inventory/bookPlace2");
        BookPlace3 = Resources.Load<AudioClip>("SFX/Inventory/bookPlace3");

        // inventory bottle
        Bottle1 = Resources.Load<AudioClip>("SFX/Inventory/bottle");

        // inventory clicks
        Click1 = Resources.Load<AudioClip>("SFX/Inventory/click1");
        Click2 = Resources.Load<AudioClip>("SFX/Inventory/click2");
        Click3 = Resources.Load<AudioClip>("SFX/Inventory/click3");
        Click4 = Resources.Load<AudioClip>("SFX/Inventory/click4");
        Click5 = Resources.Load<AudioClip>("SFX/Inventory/click5");

        // inventory cloth
        Cloth1 = Resources.Load<AudioClip>("SFX/Inventory/cloth");
        Cloth2 = Resources.Load<AudioClip>("SFX/Inventory/cloth1");
        Cloth3 = Resources.Load<AudioClip>("SFX/Inventory/cloth2");
        Cloth4 = Resources.Load<AudioClip>("SFX/Inventory/cloth3");
        Cloth5 = Resources.Load<AudioClip>("SFX/Inventory/cloth4");
        Cloth6 = Resources.Load<AudioClip>("SFX/Inventory/cloth-heavy");
        Cloth7 = Resources.Load<AudioClip>("SFX/Inventory/dropLeather");
        Cloth8 = Resources.Load<AudioClip>("SFX/Inventory/handleSmallLeather");
        Cloth9 = Resources.Load<AudioClip>("SFX/Inventory/handleSmallLeather2");

        // inventory weapon
        Weapon1 = Resources.Load<AudioClip>("SFX/Player/sword-unsheathe");
        Weapon2 = Resources.Load<AudioClip>("SFX/Player/sword-unsheathe2");
        Weapon3 = Resources.Load<AudioClip>("SFX/Player/sword-unsheathe3");
        Weapon4 = Resources.Load<AudioClip>("SFX/Player/sword-unsheathe4");
        Weapon5 = Resources.Load<AudioClip>("SFX/Player/sword-unsheathe5");

        // metal/wood
        Metal1 = Resources.Load<AudioClip>("SFX/Inventory/metal-small1");
        Metal2 = Resources.Load<AudioClip>("SFX/Inventory/metal-small2");
        Metal3 = Resources.Load<AudioClip>("SFX/Inventory/metal-small3");
        Wood1 = Resources.Load<AudioClip>("SFX/Inventory/wood-small");

        // breakable
        Break1 = Resources.Load<AudioClip>("SFX/Object/break1");
        Break2 = Resources.Load<AudioClip>("SFX/Object/break2");
        Break3 = Resources.Load<AudioClip>("SFX/Object/break3");
        Break4 = Resources.Load<AudioClip>("SFX/Object/break4");
        Break5 = Resources.Load<AudioClip>("SFX/Object/break5");

        // swings
        Swing1 = Resources.Load<AudioClip>("SFX/Player/swing");
        Swing2 = Resources.Load<AudioClip>("SFX/Player/swing2");
        Swing3 = Resources.Load<AudioClip>("SFX/Player/swing3");
        Swing4 = Resources.Load<AudioClip>("SFX/Player/melee sound");
        Swing5 = Resources.Load<AudioClip>("SFX/Player/sword sound");

        // enemy swings
        EnemySwing1 = Resources.Load<AudioClip>("SFX/Enemy/animal melee sound");

        // door
        Door1 = Resources.Load<AudioClip>("SFX/Object/door");
        Door2 = Resources.Load<AudioClip>("SFX/Object/doorClose_1");
        Door3 = Resources.Load<AudioClip>("SFX/Object/doorClose_2");
        Door4 = Resources.Load<AudioClip>("SFX/Object/doorClose_3");
        Door5 = Resources.Load<AudioClip>("SFX/Object/doorClose_4");
        Door6 = Resources.Load<AudioClip>("SFX/Object/doorOpen_1");
        Door7 = Resources.Load<AudioClip>("SFX/Object/doorOpen_2");

        // shrine healing
        Shrine1 = Resources.Load<AudioClip>("SFX/Object/spell");

        // coins
        Coin1 = Resources.Load<AudioClip>("SFX/Object/coin");
        Coin2 = Resources.Load<AudioClip>("SFX/Object/coin2");
        Coin3 = Resources.Load<AudioClip>("SFX/Object/coin3");
        Coin4 = Resources.Load<AudioClip>("SFX/Coins/handleCoins");
        Coin5 = Resources.Load<AudioClip>("SFX/Coins/handleCoins2");

        // enemies
        Monster1 = Resources.Load<AudioClip>("SFX/Enemy/mnstr1");
        Monster2 = Resources.Load<AudioClip>("SFX/Enemy/mnstr2");
        Monster3 = Resources.Load<AudioClip>("SFX/Enemy/mnstr3");
        Monster4 = Resources.Load<AudioClip>("SFX/Enemy/mnstr4");
        Monster5 = Resources.Load<AudioClip>("SFX/Enemy/mnstr5");
        Monster6 = Resources.Load<AudioClip>("SFX/Enemy/mnstr6");
        Monster7 = Resources.Load<AudioClip>("SFX/Enemy/mnstr7");
        Monster8 = Resources.Load<AudioClip>("SFX/Enemy/mnstr8");
        Monster9 = Resources.Load<AudioClip>("SFX/Enemy/mnstr9");
        Monster10 = Resources.Load<AudioClip>("SFX/Enemy/mnstr10");
        Monster11 = Resources.Load<AudioClip>("SFX/Enemy/mnstr11");
        Monster12 = Resources.Load<AudioClip>("SFX/Enemy/mnstr12");
        Monster13 = Resources.Load<AudioClip>("SFX/Enemy/mnstr13");
        Monster14 = Resources.Load<AudioClip>("SFX/Enemy/mnstr14");
        Monster15 = Resources.Load<AudioClip>("SFX/Enemy/mnstr15");
        Bite1 = Resources.Load<AudioClip>("SFX/Enemy/bite-small");
        Bite2 = Resources.Load<AudioClip>("SFX/Enemy/bite-small2");
        Bite3 = Resources.Load<AudioClip>("SFX/Enemy/bite-small3");
        Giant1 = Resources.Load<AudioClip>("SFX/Enemy/giant1");
        Giant2 = Resources.Load<AudioClip>("SFX/Enemy/giant2");
        Giant3 = Resources.Load<AudioClip>("SFX/Enemy/giant3");
        Giant4 = Resources.Load<AudioClip>("SFX/Enemy/giant4");
        Giant5 = Resources.Load<AudioClip>("SFX/Enemy/giant5");
        Orge1 = Resources.Load<AudioClip>("SFX/Enemy/orge1");
        Orge2 = Resources.Load<AudioClip>("SFX/Enemy/orge2");
        Orge3 = Resources.Load<AudioClip>("SFX/Enemy/orge3");
        Orge4 = Resources.Load<AudioClip>("SFX/Enemy/orge4");
        Orge5 = Resources.Load<AudioClip>("SFX/Enemy/orge5");
        Shade1 = Resources.Load<AudioClip>("SFX/Enemy/shade1");
        Shade2 = Resources.Load<AudioClip>("SFX/Enemy/shade2");
        Shade3 = Resources.Load<AudioClip>("SFX/Enemy/shade3");
        Shade4 = Resources.Load<AudioClip>("SFX/Enemy/shade4");
        Shade5 = Resources.Load<AudioClip>("SFX/Enemy/shade5");
        Shade6 = Resources.Load<AudioClip>("SFX/Enemy/shade6");
        Shade7 = Resources.Load<AudioClip>("SFX/Enemy/shade7");
        Shade8 = Resources.Load<AudioClip>("SFX/Enemy/shade8");
        Shade9 = Resources.Load<AudioClip>("SFX/Enemy/shade9");
        Shade10 = Resources.Load<AudioClip>("SFX/Enemy/shade10");
        Shade11 = Resources.Load<AudioClip>("SFX/Enemy/shade11");
        Shade12 = Resources.Load<AudioClip>("SFX/Enemy/shade12");
        Shade13 = Resources.Load<AudioClip>("SFX/Enemy/shade13");
        Shade14 = Resources.Load<AudioClip>("SFX/Enemy/shade14");
        Shade15 = Resources.Load<AudioClip>("SFX/Enemy/shade15");
        Slime1 = Resources.Load<AudioClip>("SFX/Enemy/slime1");
        Slime2 = Resources.Load<AudioClip>("SFX/Enemy/slime2");
        Slime3 = Resources.Load<AudioClip>("SFX/Enemy/slime3");
        Slime4 = Resources.Load<AudioClip>("SFX/Enemy/slime4");
        Slime5 = Resources.Load<AudioClip>("SFX/Enemy/slime5");
        Slime6 = Resources.Load<AudioClip>("SFX/Enemy/slime6");
        Slime7 = Resources.Load<AudioClip>("SFX/Enemy/slime7");
        Slime8 = Resources.Load<AudioClip>("SFX/Enemy/slime8");
        Slime9 = Resources.Load<AudioClip>("SFX/Enemy/slime9");
        Slime10 = Resources.Load<AudioClip>("SFX/Enemy/slime10");
        WolfMan1 = Resources.Load<AudioClip>("SFX/Enemy/wolfman");

        // player hit
        PlayerHit1 = Resources.Load<AudioClip>("SFX/Player/playerHit1");
        PlayerPain1 = Resources.Load<AudioClip>("SFX/Player/pain1");
        PlayerPain2 = Resources.Load<AudioClip>("SFX/Player/pain2");
        PlayerPain3 = Resources.Load<AudioClip>("SFX/Player/pain3");
        PlayerPain4 = Resources.Load<AudioClip>("SFX/Player/pain4");
        PlayerPain5 = Resources.Load<AudioClip>("SFX/Player/pain5");
        PlayerPain6 = Resources.Load<AudioClip>("SFX/Player/pain6");
        PlayerPain7 = Resources.Load<AudioClip>("SFX/Player/pain7");
        PlayerPain8 = Resources.Load<AudioClip>("SFX/Player/pain8");

        // player death
        PlayerDeath1 = Resources.Load<AudioClip>("SFX/Player/die1");
        PlayerDeath2 = Resources.Load<AudioClip>("SFX/Player/die2");

        // footsteps
        Footstep1 = Resources.Load<AudioClip>("SFX/Player/footstep00");
        Footstep2 = Resources.Load<AudioClip>("SFX/Player/footstep01");
        Footstep3 = Resources.Load<AudioClip>("SFX/Player/footstep02");
        Footstep4 = Resources.Load<AudioClip>("SFX/Player/footstep03");
        Footstep5 = Resources.Load<AudioClip>("SFX/PLayer/footstep04");
        Footstep6 = Resources.Load<AudioClip>("SFX/Player/footstep05");
        Footstep7 = Resources.Load<AudioClip>("SFX/Player/footstep06");
        Footstep8 = Resources.Load<AudioClip>("SFX/Player/footstep07");
        Footstep9 = Resources.Load<AudioClip>("SFX/Player/footstep08");
        Footstep10 = Resources.Load<AudioClip>("SFX/Player/footstep09");

        // magic
        Fire = Resources.Load<AudioClip>("SFX/Player/foom_0");
        FireExplosion = Resources.Load<AudioClip>("SFX/Player/synthetic_explosion_1");
        Snowball = Resources.Load<AudioClip>("SFX/Player/freeze2");
        MagicFail1 = Resources.Load<AudioClip>("SFX/Player/magicfail");
        MagicFail2 = Resources.Load<AudioClip>("SFX/Player/magicfail2");
        SnowBallExplosion1 = Resources.Load<AudioClip>("SFX/Player/ice");
        SnowBallExplosion2 = Resources.Load<AudioClip>("SFX/Player/coldsnap");

        // crafting
        Craft = Resources.Load<AudioClip>("SFX/Inventory/bing1");
        
        // dash
        Dash1 = Resources.Load<AudioClip>("SFX/Player/whoosh");
        Dash2 = Resources.Load<AudioClip>("SFX/Player/whoosh2");

        // enemy hit
        EnemyHit1 = Resources.Load<AudioClip>("SFX/Enemy/enemyHit1");

        // play overworld start
        EventManager.onInventorySwap += PlayInventoryClick;
        SoundPlayer.clip = CaravanHub;
        SoundPlayer.Play();
    }

    private void EnteringMainMenu(System.EventArgs e)
    {
        SoundPlayer.Stop();
        SoundPlayer.clip = CaravanHub;
        SoundPlayer.Play();
    }

    private void EnteringOverworld(System.EventArgs e)
    {
        // subscribe to events
        EventManager.onAttack += PlaySwing;
        EventManager.OnPlayerHit += PlayPlayerHit;
        EventManager.DoorEntered += PlayDoor;
        EventManager.OnCastFireball += PlayFireball;
        EventManager.onFireballCollision += PlayFireExplosion;
        EventManager.onPlayerMovement += PlayPlayerMovement;
        //EventManager.onUseShrine += PlayShrine;
        EventManager.onDash += PlayDash;
        //EventManager.onDungeonGenerated += PlayDungeonAmbiance;
        EventManager.OnCastSnowball += PlaySnowball;
        EventManager.onSnowballCollision += PlaySnowballExplosion;
        EventManager.onOpenInventory += PlayInventoryCloth;
        EventManager.onCloseInventory += PlayInventoryCloth;
        EventManager.onMagicFailure += PlayMagicFailure;
        EventManager.onArmorChange += PlayInventoryArmor;
        EventManager.onWeaponChange += PlayInventoryChangeWeapon;
        EventManager.onInventorySwap += PlayInventoryClick;
        EventManager.onBuy += PlayCoins;
        EventManager.onSell += PlayCoins;
        EventManager.onCoinPickedUp += PlayCoins;
        EventManager.onCraftingMade += PlayCraftSound;
        EventManager.onRuneChange += PlayShrine;
        //EventManager.OnExitDungeon += PlayDesertWind;
        EventManager.onScarabAgro += PlayEnemyBite;
        EventManager.onSkullAgro += PlayEnemyShade;
        EventManager.onMummyAgro += PlayEnemyMonster;
        EventManager.onPlayerDeath += PlayPlayerDeath;
        EventManager.onBreakBox += PlayBreak;
        EventManager.onRossStart += PlayRossStart;
        EventManager.onRossEnd += PlayRossEnd;
        EventManager.onRossCharge += PlayRossCharge;
        EventManager.onRossHitPillar += PlayRossHitPillar;
        EventManager.onAnubisStart += PlayAnubisStart;
        EventManager.onAnubisEnd += PlayAnubisEnd;
        EventManager.onAnubisAttack += PlayAnubisAttack;
        EventManager.onAnubisAttackExplosion += PlayAnubisExplosion;
        EventManager.onAnubisTeleport += PlayDash;
        EventManager.onAnubisTeleport += PlayDash;
        EventManager.onEvilAltar += PlayEvilAltar;
        EventManager.onPureAltar += PlayPureAltar;
        EventManager.onOasisReveal += PlayOasisReveal;
        EventManager.onPyramidCrumbleRise += PlayPyramidCrumbleRise;
        EventManager.onOverworldMovement += PlayOverworldMovement;
        EventManager.onHealthPickup += PlayShrine;
        EventManager.onDrinkPotion += PlayPotion;
        EventManager.onEnemyDeath += PlayEnemyDeath;
        SoundPlayer.Stop();
        SoundPlayer.clip = OverworldMusic;
        SoundPlayer.Play();
    }

    private void PlayEnemyDeath(System.EventArgs e)
    {
        SoundPlayer.PlayOneShot(FireExplosion, 0.05f);
    }

    private void PlayPotion(System.EventArgs e)
    {
        SoundPlayer.PlayOneShot(Bottle1, 0.6f);
    }

    private void PlayOverworldMovement(System.EventArgs e)
    {
        float volume = 0.02f;
        int x = Random.Range(0, 10);
        if (x == 0){
            SoundPlayer.PlayOneShot(Footstep1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Footstep2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Footstep3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Footstep4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Footstep5, volume);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(Footstep6, volume);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(Footstep7, volume);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(Footstep8, volume);
        } else if (x == 8){
            SoundPlayer.PlayOneShot(Footstep9, volume);
        } else if (x == 9){
            SoundPlayer.PlayOneShot(Footstep10, volume);
        }
    }

    private void PlayPyramidCrumbleRise(System.EventArgs e)
    {
        float volume = 0.1f;
        int x = Random.Range(0, 3);
        if (x == 0){
            SoundPlayer.PlayOneShot(Crumble1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Crumble2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Crumble3, volume);
        }
    }

    private void PlayOasisReveal(System.EventArgs e)
    {
        SoundPlayer.PlayOneShot(Reveal, 0.4f);
    }

    private void PlayEvilAltar(System.EventArgs e)
    {
        SoundPlayer.PlayOneShot(Endfire1, 0.75f);
    }

    private void PlayPureAltar(System.EventArgs e)
    {
        SoundPlayer.PlayOneShot(Endfire2, 0.75f);
    }

    private void PlayAnubisExplosion(System.EventArgs e)
    {
        SoundPlayer.PlayOneShot(FireExplosion, 0.1f);
    }

    private void PlayAnubisStart(System.EventArgs e)
    {
        SoundPlayer.Stop();
        SoundPlayer.clip = AnubisMusic;
        SoundPlayer.Play();
    }

    private void PlayAnubisEnd(System.EventArgs e)
    {
        SoundPlayer.Stop();
        // stop bug (multiple death sounds on swings)
        EventManager.onAnubisEnd -= PlayAnubisEnd;
        SoundPlayer.PlayOneShot(AnubisDeath, 0.6f);
        StartCoroutine(anubisEnd());
    }

    IEnumerator anubisEnd()
    {
        yield return new WaitForSeconds(4.0f);
        SoundPlayer.clip = DungeonMusic;
        SoundPlayer.Play();
    }

    private void PlayAnubisAttack(System.EventArgs e)
    {
        SoundPlayer.PlayOneShot(AnubisAttack, 1.0f);
    }

    private void PlayRossStart(System.EventArgs e)
    {
        SoundPlayer.Stop();
        SoundPlayer.PlayOneShot(RossDeath, 0.4f);
        StartCoroutine(rossStart());
    }

    IEnumerator rossStart()
    {
        yield return new WaitForSeconds(8.0f);
        SoundPlayer.clip = RossMusic;
        SoundPlayer.Play();
    }

    private void PlayRossEnd(System.EventArgs e)
    {
        SoundPlayer.Stop();
        SoundPlayer.PlayOneShot(RossDeath, 0.4f);
        StartCoroutine(rossEnd());
    }

    IEnumerator rossEnd()
    {
        yield return new WaitForSeconds(11.0f);
        SoundPlayer.clip = DungeonMusic;
        SoundPlayer.Play();
    }

    private void PlayRossCharge(System.EventArgs e)
    {
        float volume = 1.0f;
        int x = Random.Range(0, 3);
        if (x == 0){
            SoundPlayer.PlayOneShot(RossAttack1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(RossAttack2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(RossAttack3, volume);
        }
    }

    private void PlayRossHitPillar(System.EventArgs e)
    {
        float volume = 0.5f;
        int x = Random.Range(0, 3);
        if (x == 0){
            SoundPlayer.PlayOneShot(Crumble1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Crumble2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Crumble3, volume);
        }
    }

    private void PlayPlayerDeath(System.EventArgs e)
    {
        float volume = 1.0f;
        int x = Random.Range(0, 2);
        if (x == 0){
            SoundPlayer.PlayOneShot(PlayerDeath1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(PlayerDeath2, volume);
        }
    }

    private void PlayBreak(System.EventArgs e)
    {
        float volume = 0.15f;
        int x = Random.Range(0, 5);
        if (x == 0){
            SoundPlayer.PlayOneShot(Break1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Break2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Break3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Break4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Break5, volume);
        }
    }

    private void EnteringDungeon(System.EventArgs e)
    {
        SoundPlayer.Stop();
        SoundPlayer.clip = DungeonMusic;
        SoundPlayer.Play();
    }

    // play dungeon ambiance
    //private void PlayDungeonAmbiance(System.EventArgs e){
        
    //}

    // play desert wind on overworld
    //private void PlayDesertWind(System.EventArgs e){
        
    //}
    
    // play caravan music
    //private void PlayCaravan(System.EventArgs e){
    //    SoundPlayer.PlayOneShot(CaravanHub);
    //}

    // play any one of enemy movement sound effects
    private void PlayEnemyMovement(System.EventArgs e){
        int x = Random.Range(0, 10);
        if (x == 0){
            SoundPlayer.PlayOneShot(Footstep1);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Footstep2);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Footstep3);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Footstep4);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Footstep5);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(Footstep6);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(Footstep7);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(Footstep8);
        } else if (x == 8){
            SoundPlayer.PlayOneShot(Footstep9);
        } else if (x == 9){
            SoundPlayer.PlayOneShot(Footstep10);
        }
    }

    // play any one of player movement sound effects
    private void PlayPlayerMovement(System.EventArgs e){
        float volume = 0.05f;
        int x = Random.Range(0, 10);
        if (x == 0){
            SoundPlayer.PlayOneShot(Footstep1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Footstep2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Footstep3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Footstep4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Footstep5, volume);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(Footstep6, volume);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(Footstep7, volume);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(Footstep8, volume);
        } else if (x == 8){
            SoundPlayer.PlayOneShot(Footstep9, volume);
        } else if (x == 9){
            SoundPlayer.PlayOneShot(Footstep10, volume);
        }
    }

    // play any one of shrine sound effects
    private void PlayShrine(System.EventArgs e){
        SoundPlayer.PlayOneShot(Shrine1, 0.15f);
    }

    // play any one of dash sound effects
    private void PlayDash(System.EventArgs e){
        float volume = 0.4f;
        int x = Random.Range(0, 2);
        if (x == 0){
            SoundPlayer.PlayOneShot(Dash1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Dash2, volume);
        }
    }

    // play any one of wall hit sound effects
    private void PlayWall(System.EventArgs e){
        int x = Random.Range(0, 3);
        if (x == 0){
            SoundPlayer.PlayOneShot(Metal1);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Metal2);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Metal3);
        }
    }

    // play any one of breakable sound effects
    private void PlayBreakable(System.EventArgs e){
        SoundPlayer.PlayOneShot(Wood1);
    }

    // play any one of enemy hit sound effects
    private void PlayEnemyHit(System.EventArgs e){
        SoundPlayer.PlayOneShot(EnemyHit1);
    }

    // play any one of player hit sound effects
    private void PlayPlayerHit(System.EventArgs e){
        SoundPlayer.PlayOneShot(PlayerHit1, 0.4f);
        StartCoroutine(playerhit());
    }

    IEnumerator playerhit(){
        float volume = 0.6f;
        int x = Random.Range(0, 8);
        yield return new WaitForSeconds(0.2f);
        if (x == 0){
            SoundPlayer.PlayOneShot(PlayerPain1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(PlayerPain2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(PlayerPain3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(PlayerPain4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(PlayerPain5, volume);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(PlayerPain6, volume);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(PlayerPain7, volume);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(PlayerPain8, volume);
        }
    }

    // play any one of swing sound effects
    private void PlaySwing(System.EventArgs e){
        float volume = 0.5f;
        int x = Random.Range(0, 5);
        if (x == 0){
            SoundPlayer.PlayOneShot(Swing1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Swing2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Swing3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Swing4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Swing5, volume);
        }
    }

    // play any one of coin sounds
    private void PlayCoins(System.EventArgs e){
        float volume = 0.2f;
        int x = Random.Range(0, 5);
        if (x == 0) {
            SoundPlayer.PlayOneShot(Coin1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Coin2, volume);
        } else if (x == 2) {
            SoundPlayer.PlayOneShot(Coin3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Coin4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Coin5, volume);
        }
    }

    // play anyone of enemy sounds
    private void PlayEnemyMonster(System.EventArgs e){
        float volume = 0.7f;
        int x = Random.Range(0, 15);
        if (x == 0){
            SoundPlayer.PlayOneShot(Monster1, volume);
        } else if (x == 1) {
            SoundPlayer.PlayOneShot(Monster2, volume);
        } else if (x == 2) {
            SoundPlayer.PlayOneShot(Monster3, volume);
        } else if (x == 3) {
            SoundPlayer.PlayOneShot(Monster4, volume);
        } else if (x == 4) {
            SoundPlayer.PlayOneShot(Monster5, volume);
        } else if (x == 5) {
            SoundPlayer.PlayOneShot(Monster6, volume);
        } else if (x == 6) {
            SoundPlayer.PlayOneShot(Monster7, volume);
        } else if (x == 7) {
            SoundPlayer.PlayOneShot(Monster8, volume);
        } else if (x == 8) {
            SoundPlayer.PlayOneShot(Monster9, volume);
        } else if (x == 9) {
            SoundPlayer.PlayOneShot(Monster10, volume);
        } else if (x == 10) {
            SoundPlayer.PlayOneShot(Monster11, volume);
        } else if (x == 11) {
            SoundPlayer.PlayOneShot(Monster12, volume);
        } else if (x == 12) {
            SoundPlayer.PlayOneShot(Monster13, volume);
        } else if (x == 13) {
            SoundPlayer.PlayOneShot(Monster14, volume);
        } else if (x == 14) {
            SoundPlayer.PlayOneShot(Monster15, volume);
        }
    }

    // play anyone of door sounds
    private void PlayDoor(System.EventArgs e){
        float volume = 0.3f;
        int x = Random.Range(0, 7);
        if (x == 0){
            SoundPlayer.PlayOneShot(Door1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Door2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Door3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Door4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Door5, volume);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(Door6, volume);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(Door7, volume);
        }
    }

    // play any one of armor inventory sounds
    private void PlayInventoryArmor(System.EventArgs e){
        float volume = 0.5f;
        int x = Random.Range(0, 3);
        if (x == 0){
            SoundPlayer.PlayOneShot(Armor1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Chainmail1, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Chainmail2, volume);
        }
    }

    // play any one of eating sounds
    private void PlayInventoryEat(System.EventArgs e){
        SoundPlayer.PlayOneShot(Beads1);
    }

    // play any one of changing armor sounds
    private void PlayInventoryChangeArmor(System.EventArgs e){
        int x = Random.Range(0, 4);
        if (x == 0){
            SoundPlayer.PlayOneShot(Belt1);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Belt2);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Belt3);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Belt4);
        }
    }

    // play any one of book inventory sounds
    private void PlayInventoryBook(System.EventArgs e){
        int x = Random.Range(0, 8);
        if (x == 0){
            SoundPlayer.PlayOneShot(BookClose);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(BookFlip1);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(BookFlip2);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(BookFlip3);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(BookOpen);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(BookPlace1);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(BookPlace2);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(BookPlace3);
        }
    }

    // play any one of drink potion sounds
    //private void PlayDrinkPotion(System.EventArgs e){
    //    SoundPlayer.PlayOneShot(Bottle1);
    //}
    
    // play any one of inventory click sounds
    private void PlayInventoryClick(System.EventArgs e){
        float volume = 0.5f;
        int x = Random.Range(0, 5);
        if (x == 0){
            SoundPlayer.PlayOneShot(Click1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Click2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Click3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Click4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Click5, volume);
        }
    }

    // play any one of cloth inventory sounds
    private void PlayInventoryCloth(object sender, System.EventArgs e){
        float volume = 0.3f;
        int x = Random.Range(0, 9);
        if (x == 0){
            SoundPlayer.PlayOneShot(Cloth1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Cloth2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Cloth3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Cloth4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Cloth5, volume);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(Cloth6, volume);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(Cloth7, volume);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(Cloth8, volume);
        } else if (x == 8){
            SoundPlayer.PlayOneShot(Cloth9, volume);
        }
    }

    // play any one of changing weapons sounds
    private void PlayInventoryChangeWeapon(System.EventArgs e){
        float volume = 0.4f;
        int x = Random.Range(0, 5);
        if (x == 0){
            SoundPlayer.PlayOneShot(Weapon1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Weapon2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Weapon3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Weapon4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Weapon5, volume);
        }
    }

    // play any one of enemy swing sounds
    private void PlayEnemySwing(System.EventArgs e){
        SoundPlayer.PlayOneShot(EnemySwing1);
    }

    // play any one of enemy bite sounds
    private void PlayEnemyBite(System.EventArgs e){
        float volume = 0.65f;
        int x = Random.Range(0, 3);
        if (x == 0){
            SoundPlayer.PlayOneShot(Bite1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Bite2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Bite3, volume);
        }
    }

    // play any one of enemy giant sounds
    private void PlayEnemyGiant(System.EventArgs e){
        int x = Random.Range(0, 5);
        if (x == 0){
            SoundPlayer.PlayOneShot(Giant1);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Giant2);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Giant3);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Giant4);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Giant5);
        }
    }

    // play any one of enemy shade sounds
    private void PlayEnemyShade(System.EventArgs e){
        float volume = 0.55f;
        int x = Random.Range(0, 15);
        if (x == 0){
            SoundPlayer.PlayOneShot(Shade1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Shade2, volume);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Shade3, volume);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Shade4, volume);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Shade5, volume);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(Shade6, volume);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(Shade7, volume);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(Shade8, volume);
        } else if (x == 8){
            SoundPlayer.PlayOneShot(Shade9, volume);
        } else if (x == 9){
            SoundPlayer.PlayOneShot(Shade10, volume);
        } else if (x == 10){
            SoundPlayer.PlayOneShot(Shade11, volume);
        } else if (x == 11){
            SoundPlayer.PlayOneShot(Shade12, volume);
        } else if (x == 12){
            SoundPlayer.PlayOneShot(Shade13, volume);
        } else if (x == 13){
            SoundPlayer.PlayOneShot(Shade14, volume);
        } else if (x == 14){
            SoundPlayer.PlayOneShot(Shade15, volume);
        }
    }

    // play any one of enemy slime sounds
    private void PlayEnemySlime(System.EventArgs e){
        int x = Random.Range(0, 10);
        if (x == 0){
            SoundPlayer.PlayOneShot(Slime1);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Slime2);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Slime3);
        } else if (x == 3){
            SoundPlayer.PlayOneShot(Slime4);
        } else if (x == 4){
            SoundPlayer.PlayOneShot(Slime5);
        } else if (x == 5){
            SoundPlayer.PlayOneShot(Slime6);
        } else if (x == 6){
            SoundPlayer.PlayOneShot(Slime7);
        } else if (x == 7){
            SoundPlayer.PlayOneShot(Slime8);
        } else if (x == 8){
            SoundPlayer.PlayOneShot(Slime9);
        } else if (x == 9){
            SoundPlayer.PlayOneShot(Slime10);
        }
    }

    // play any one of enemy wolf man sounds
    private void PlayEnemyWolfman(System.EventArgs e){
        SoundPlayer.PlayOneShot(WolfMan1);
    }

    // play any one of magic fireball sounds
    private void PlayFireball(System.EventArgs e){
        SoundPlayer.PlayOneShot(Fire, 0.55f);
    }

    // play any one of magic fire explosion sounds
    private void PlayFireExplosion(System.EventArgs e){
        SoundPlayer.PlayOneShot(FireExplosion, 0.37f);
    }

    // play any one of magic snowball sounds
    private void PlaySnowball(System.EventArgs e){
        SoundPlayer.PlayOneShot(Snowball, 0.75f);
    }

    // play any one of magic snowball explosion sounds
    private void PlaySnowballExplosion(System.EventArgs e){
        float volume = 0.85f;
        int x = Random.Range(0, 2);
        if (x == 0){
            SoundPlayer.PlayOneShot(SnowBallExplosion1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(SnowBallExplosion2, volume);
        }
    }

    // play anyone of magic failure sounds
    private void PlayMagicFailure(System.EventArgs e){
        float volume = 0.8f;
        int x = Random.Range(0, 2);
        if (x == 0){
            SoundPlayer.PlayOneShot(MagicFail1, volume);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(MagicFail2, volume);
        }
    }

    // play anyone of crafting sounds
    private void PlayCraftSound(System.EventArgs e){
        StartCoroutine(waiter());
    }

    IEnumerator waiter(){
        SoundPlayer.PlayOneShot(Craft);
        yield return new WaitForSeconds(0.35f);
        SoundPlayer.PlayOneShot(Craft);
    }

}
