using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.OnPlayerHit += EventManager_OnPlayerHit;
    }

    private void EventManager_OnPlayerHit(System.EventArgs e)
    {
        animator.Play(0);
    }
}
    
