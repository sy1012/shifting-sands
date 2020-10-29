using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    public Animator animator;
    private int wait = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("windblow"))
        { 
            transform.position = new Vector3(Random.Range(-20, 21), Random.Range(-15, 16), 0);
            animator.Play("windblow");
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

    }
}
