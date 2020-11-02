﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // manages all the audio clips
    private AudioSource SoundPlayer;
    // make a clip for each sound effect
    // swings
    private AudioClip Swing1;
    private AudioClip Swing2;
    private AudioClip Swing3;

    // door
    private AudioClip Door;

    // coins
    private AudioClip Coin1;
    private AudioClip Coin2;
    private AudioClip Coin3;

    // enemies
    private AudioClip Enemy1;
    private AudioClip Enemy2;
    private AudioClip Enemy3;
    private AudioClip Enemy4;
    private AudioClip Enemy5;
    private AudioClip Enemy6;
    private AudioClip Enemy7;
    private AudioClip Enemy8;
    private AudioClip Enemy9;
    private AudioClip Enemy10;
    private AudioClip Enemy11;
    private AudioClip Enemy12;
    private AudioClip Enemy13;
    private AudioClip Enemy14;
    private AudioClip Enemy15;

    // player hit
    private AudioClip PlayerHit1;

    // enemy hit
    private AudioClip EnemyHit1;

    // to allow use in other scripts
    public static SoundManager current;

    void Start(){
        // initialize every sound manager and effect
        current = this;
        SoundPlayer = this.gameObject.AddComponent<AudioSource>();

        // swings
        Swing1 = Resources.Load<AudioClip>("SFX/Player/swing");
        Swing2 = Resources.Load<AudioClip>("SFX/Player/swing2");
        Swing3 = Resources.Load<AudioClip>("SFX/Player/swing3");

        // door
        Door = Resources.Load<AudioClip>("SFX/Object/door");

        // coins
        Coin1 = Resources.Load<AudioClip>("SFX/Object/coin");
        Coin2 = Resources.Load<AudioClip>("SFX/Object/coin2");
        Coin3 = Resources.Load<AudioClip>("SFX/Object/coin3");

        // enemies
        Enemy1 = Resources.Load<AudioClip>("SFX/Enemy/mnstr1");
        Enemy2 = Resources.Load<AudioClip>("SFX/Enemy/mnstr2");
        Enemy3 = Resources.Load<AudioClip>("SFX/Enemy/mnstr3");
        Enemy4 = Resources.Load<AudioClip>("SFX/Enemy/mnstr4");
        Enemy5 = Resources.Load<AudioClip>("SFX/Enemy/mnstr5");
        Enemy6 = Resources.Load<AudioClip>("SFX/Enemy/mnstr6");
        Enemy7 = Resources.Load<AudioClip>("SFX/Enemy/mnstr7");
        Enemy8 = Resources.Load<AudioClip>("SFX/Enemy/mnstr8");
        Enemy9 = Resources.Load<AudioClip>("SFX/Enemy/mnstr9");
        Enemy10 = Resources.Load<AudioClip>("SFX/Enemy/mnstr10");
        Enemy11 = Resources.Load<AudioClip>("SFX/Enemy/mnstr11");
        Enemy12 = Resources.Load<AudioClip>("SFX/Enemy/mnstr12");
        Enemy13 = Resources.Load<AudioClip>("SFX/Enemy/mnstr13");
        Enemy14 = Resources.Load<AudioClip>("SFX/Enemy/mnstr14");
        Enemy15 = Resources.Load<AudioClip>("SFX/Enemy/mnstr15");

        // player hit
        PlayerHit1 = Resources.Load<AudioClip>("SFX/Player/playerHit1");

        // enemy hit
        EnemyHit1 = Resources.Load<AudioClip>("SFX/Enemy/enemyHit1");
    }

    

    // play dungeon ambiance
    public void PlayDungeonAmbiance(){

    }

    // play desert wind on overworld
    public void PlayDesertWind(){

    }
    
    // play caravan music
    public void PlayCaravan(){

    }

    // play any one of enemy movement sound effects
    public void PlayEnemyMovement(){

    }

    // play any one of player movement sound effects
    public void PlayPlayerMovement(){

    }

    // play any one of shrine sound effects
    public void PlayShrine(){

    }

    // play any one of dash sound effects
    public void PlayDash(){

    }

    // play any one of wall hit sound effects
    public void PlayWall(){

    }

    // play any one of breakable sound effects
    public void PlayBreakable(){

    }

    // play any one of enemy hit sound effects
    public void PlayEnemyHit(){
        SoundPlayer.PlayOneShot(EnemyHit1);
    }

    // play any one of player hit sound effects
    public void PlayPlayerHit(){
        SoundPlayer.PlayOneShot(PlayerHit1);
    }

    // play any one of swing sound effects
    public void PlaySwing(){
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
    public void PlayCoins(){
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
    public void PlayEnemy(){
        int x = Random.Range(0, 15);
        if (x == 0){
            SoundPlayer.PlayOneShot(Enemy1);
        } else if (x == 1) {
            SoundPlayer.PlayOneShot(Enemy2);
        } else if (x == 2) {
            SoundPlayer.PlayOneShot(Enemy3);
        } else if (x == 3) {
            SoundPlayer.PlayOneShot(Enemy4);
        } else if (x == 4) {
            SoundPlayer.PlayOneShot(Enemy5);
        } else if (x == 5) {
            SoundPlayer.PlayOneShot(Enemy6);
        } else if (x == 6) {
            SoundPlayer.PlayOneShot(Enemy7);
        } else if (x == 7) {
            SoundPlayer.PlayOneShot(Enemy8);
        } else if (x == 8) {
            SoundPlayer.PlayOneShot(Enemy9);
        } else if (x == 9) {
            SoundPlayer.PlayOneShot(Enemy10);
        } else if (x == 10) {
            SoundPlayer.PlayOneShot(Enemy11);
        } else if (x == 11) {
            SoundPlayer.PlayOneShot(Enemy12);
        } else if (x == 12) {
            SoundPlayer.PlayOneShot(Enemy13);
        } else if (x == 13) {
            SoundPlayer.PlayOneShot(Enemy14);
        } else if (x == 14) {
            SoundPlayer.PlayOneShot(Enemy15);
        }
    }

    // play anyone of door sounds
    public void PlayDoor(){
        SoundPlayer.PlayOneShot(Door);
    }
    
}
