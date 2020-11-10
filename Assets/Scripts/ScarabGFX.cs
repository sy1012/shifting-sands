using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabGFX : MonoBehaviour
{
    private Animator animator;
    private AIPath aiPath;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aiPath = GetComponentInParent<AIPath>();
        rb = GetComponentInParent<Rigidbody2D>();
        animator.Play("scarab_walk");
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.LookRotation(Vector3.forward, aiPath.desiredVelocity);
        
    }
}
