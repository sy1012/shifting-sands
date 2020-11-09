using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyGFX : MonoBehaviour
{
    private Animator animator;
    private AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aiPath = GetComponentInParent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(aiPath.desiredVelocity.y) > Mathf.Abs(aiPath.desiredVelocity.x))
        {
            if (aiPath.desiredVelocity.y >= 0.01f)
            {
                animator.Play("walk_up");
            }
            else if (aiPath.desiredVelocity.y <= 0.01f)
            {
                animator.Play("walk_down");
            }
        }
        else
        {
        if (aiPath.desiredVelocity.x >= 0.01f)
            {
                animator.Play("walk_right");
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                animator.Play("walk_left");
            }
        }


    }
}
