using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // manages all the audio clips
    private AudioSource SoundPlayer;
    // make a clip for each sound effect
    private AudioClip Swing1;
    private AudioClip Swing2;
    private AudioClip Swing3;
    public static SoundManager current;

    void Start(){
        // initialize every sound manager and effect
        current = this;
        SoundPlayer = this.gameObject.AddComponent<AudioSource>();
        Swing1 = Resources.Load<AudioClip>("SFX/swing");
        Swing2 = Resources.Load<AudioClip>("SFX/swing2");
        Swing3 = Resources.Load<AudioClip>("SFX/swing3");
    }

    // play any one of 3 swing sound effects
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
    
}
