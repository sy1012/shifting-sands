using System.Collections;
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

    // to allow use in other scripts
    public static SoundManager current;

    void Start(){
        // initialize every sound manager and effect
        current = this;
        SoundPlayer = this.gameObject.AddComponent<AudioSource>();

        // swings
        Swing1 = Resources.Load<AudioClip>("SFX/swing");
        Swing2 = Resources.Load<AudioClip>("SFX/swing2");
        Swing3 = Resources.Load<AudioClip>("SFX/swing3");

        // door
        Door = Resources.Load<AudioClip>("SFX/door");

        // coins
        Coin1 = Resources.Load<AudioClip>("SFX/coin");
        Coin2 = Resources.Load<AudioClip>("SFX/coin2");
        Coin3 = Resources.Load<AudioClip>("SFX/coin3");

        // enemies
        Enemy1 = Resources.Load<AudioClip>("SFX/mnstr1");
        Enemy2 = Resources.Load<AudioClip>("SFX/mnstr2");
        Enemy3 = Resources.Load<AudioClip>("SFX/mnstr3");
        Enemy4 = Resources.Load<AudioClip>("SFX/mnstr4");
        Enemy5 = Resources.Load<AudioClip>("SFX/mnstr5");
        Enemy6 = Resources.Load<AudioClip>("SFX/mnstr6");
        Enemy7 = Resources.Load<AudioClip>("SFX/mnstr7");
        Enemy8 = Resources.Load<AudioClip>("SFX/mnstr8");
        Enemy9 = Resources.Load<AudioClip>("SFX/mnstr9");
        Enemy10 = Resources.Load<AudioClip>("SFX/mnstr10");
        Enemy11 = Resources.Load<AudioClip>("SFX/mnstr11");
        Enemy12 = Resources.Load<AudioClip>("SFX/mnstr12");
        Enemy13 = Resources.Load<AudioClip>("SFX/mnstr13");
        Enemy14 = Resources.Load<AudioClip>("SFX/mnstr14");
        Enemy15 = Resources.Load<AudioClip>("SFX/mnstr15");
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
