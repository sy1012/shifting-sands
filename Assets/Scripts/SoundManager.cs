using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // manages all the audio clips
    private AudioSource SoundPlayer;
    // make a clip for each sound effect

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

    // inventory chop
    private AudioClip Chop1;

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

    // interface
    private AudioClip Interface1;
    private AudioClip Interface2;
    private AudioClip Interface3;
    private AudioClip Interface4;
    private AudioClip Interface5;
    private AudioClip Interface6;

    // metal/wood
    private AudioClip MetalClick;
    private AudioClip MetalLatch;
    private AudioClip MetalPot1;
    private AudioClip MetalPot2;
    private AudioClip MetalPot3;
    private AudioClip MetalRinging;
    private AudioClip Metal1;
    private AudioClip Metal2;
    private AudioClip Metal3;
    private AudioClip Wood1;

    // bubbles
    private AudioClip Bubbles1;
    private AudioClip Bubbles2;
    private AudioClip Bubbles3;

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

    // creaks
    private AudioClip Creak1;
    private AudioClip Creak2;
    private AudioClip Creak3;

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
    private AudioClip Magic1;
    private AudioClip Explosion1;

    // enemy hit
    private AudioClip EnemyHit1;

    // to allow use in other scripts
    public static SoundManager current;

    void Start(){
        // initialize sound manager
        current = this;
        SoundPlayer = this.gameObject.AddComponent<AudioSource>();

        // initialize every audio clip

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

        // inventory chop
        Chop1 = Resources.Load<AudioClip>("SFX/Inventory/chop");

        // inventory clicks
        Click1 = Resources.Load<AudioClip>("SFX/Inventory/click1");
        Click2 = Resources.Load<AudioClip>("SFX/Inventory/click2");
        Click3 = Resources.Load<AudioClip>("SFX/Inventory/click3");
        Click4 = Resources.Load<AudioClip>("SFX/Inventort/click4");
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

        // interface
        Interface1 = Resources.Load<AudioClip>("SFX/Inventory/interface1");
        Interface2 = Resources.Load<AudioClip>("SFX/Inventory/interface2");
        Interface3 = Resources.Load<AudioClip>("SFX/Inventory/interface3");
        Interface4 = Resources.Load<AudioClip>("SFX/Inventory/interface4");
        Interface5 = Resources.Load<AudioClip>("SFX/Inventory/interface5");
        Interface6 = Resources.Load<AudioClip>("SFX/Inventory/interface6");

        // metal/wood
        MetalClick = Resources.Load<AudioClip>("SFX/Inventory/metalClick");
        MetalLatch = Resources.Load<AudioClip>("SFX/Inventory/metalLatch");
        MetalPot1 = Resources.Load<AudioClip>("SFX/Inventory/metalPot1");
        MetalPot2 = Resources.Load<AudioClip>("SFX/Inventory/metalPot2");
        MetalPot3 = Resources.Load<AudioClip>("SFX/Inventory/metalPot3");
        MetalRinging = Resources.Load<AudioClip>("SFX/Inventory/metal-ringing");
        Metal1 = Resources.Load<AudioClip>("SFX/Inventory/metal-small1");
        Metal2 = Resources.Load<AudioClip>("SFX/Inventory/metal-small2");
        Metal3 = Resources.Load<AudioClip>("SFX/Inventory/metal-small3");
        Wood1 = Resources.Load<AudioClip>("SFX/Inventory/wood-small");

        // bubbles
        Bubbles1 = Resources.Load<AudioClip>("SFX/Inventory/bubble");
        Bubbles2 = Resources.Load<AudioClip>("SFX/Inventory/bubble2");
        Bubbles3 = Resources.Load<AudioClip>("SFX/Inventory/bubble3");

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

        // creaks
        Creak1 = Resources.Load<AudioClip>("SFX/Object/creak1");
        Creak2 = Resources.Load<AudioClip>("SFX/Object/crack2");
        Creak3 = Resources.Load<AudioClip>("SFX/Object/crack3");

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
        Magic1 = Resources.Load<AudioClip>("SFX/Player/magic1");
        Explosion1 = Resources.Load<AudioClip>("SFX/Player/synthetic_explosion_1");

        // enemy hit
        EnemyHit1 = Resources.Load<AudioClip>("SFX/Enemy/enemyHit1");

        // subscribe to events
        EventManager.onAttack += PlaySwing;
        EventManager.OnPlayerHit += PlayPlayerHit;
        EventManager.DoorEntered += PlayDoor;
    }

    

    // play dungeon ambiance
    private void PlayDungeonAmbiance(System.EventArgs e){

    }

    // play desert wind on overworld
    private void PlayDesertWind(System.EventArgs e){

    }
    
    // play caravan music
    private void PlayCaravan(System.EventArgs e){

    }

    // play any one of enemy movement sound effects
    private void PlayEnemyMovement(System.EventArgs e){

    }

    // play any one of player movement sound effects
    private void PlayPlayerMovement(System.EventArgs e){

    }

    // play any one of shrine sound effects
    private void PlayShrine(System.EventArgs e){

    }

    // play any one of dash sound effects
    private void PlayDash(System.EventArgs e){

    }

    // play any one of wall hit sound effects
    private void PlayWall(System.EventArgs e){

    }

    // play any one of breakable sound effects
    private void PlayBreakable(System.EventArgs e){

    }

    // play any one of enemy hit sound effects
    private void PlayEnemyHit(System.EventArgs e){
        SoundPlayer.PlayOneShot(EnemyHit1);
    }

    // play any one of player hit sound effects
    private void PlayPlayerHit(System.EventArgs e){
        SoundPlayer.PlayOneShot(PlayerHit1);
    }

    // play any one of swing sound effects
    private void PlaySwing(System.EventArgs e){
        int x = Random.Range(0, 3);
        if (x == 0){
            SoundPlayer.PlayOneShot(Swing1);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Swing2);
        } else if (x == 2){
            SoundPlayer.PlayOneShot(Swing3);
        }
    }

    // play any one of coin sounds
    private void PlayCoins(System.EventArgs e){
        int x = Random.Range(0, 3);
        if (x == 0) {
            SoundPlayer.PlayOneShot(Coin1);
        } else if (x == 1){
            SoundPlayer.PlayOneShot(Coin2);
        } else if (x == 2) {
            SoundPlayer.PlayOneShot(Coin3);
        }
    }

    // play anyone of enemy sounds
    private void PlayEnemy(System.EventArgs e){
        int x = Random.Range(0, 15);
        if (x == 0){
            SoundPlayer.PlayOneShot(Monster1);
        } else if (x == 1) {
            SoundPlayer.PlayOneShot(Monster2);
        } else if (x == 2) {
            SoundPlayer.PlayOneShot(Monster3);
        } else if (x == 3) {
            SoundPlayer.PlayOneShot(Monster4);
        } else if (x == 4) {
            SoundPlayer.PlayOneShot(Monster5);
        } else if (x == 5) {
            SoundPlayer.PlayOneShot(Monster6);
        } else if (x == 6) {
            SoundPlayer.PlayOneShot(Monster7);
        } else if (x == 7) {
            SoundPlayer.PlayOneShot(Monster8);
        } else if (x == 8) {
            SoundPlayer.PlayOneShot(Monster9);
        } else if (x == 9) {
            SoundPlayer.PlayOneShot(Monster10);
        } else if (x == 10) {
            SoundPlayer.PlayOneShot(Monster11);
        } else if (x == 11) {
            SoundPlayer.PlayOneShot(Monster12);
        } else if (x == 12) {
            SoundPlayer.PlayOneShot(Monster13);
        } else if (x == 13) {
            SoundPlayer.PlayOneShot(Monster14);
        } else if (x == 14) {
            SoundPlayer.PlayOneShot(Monster15);
        }
    }

    // play anyone of door sounds
    private void PlayDoor(System.EventArgs e){
        SoundPlayer.PlayOneShot(Door1);
    }
    
}
