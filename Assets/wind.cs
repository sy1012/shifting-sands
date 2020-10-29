using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    public Animator animator;
    private bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator RandomWait()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 2f));
        this.transform.position = new Vector3(Random.Range(-20, 21), Random.Range(-15, 16), 0);
        this.animator.Play("windblow");
        waiting = false;
    }

        // Update is called once per frame
        void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("windblow") && !waiting)
        {
            waiting = true;
            transform.position = new Vector3(-50, -50, 0);
            StartCoroutine("RandomWait");
        }

    }
}
