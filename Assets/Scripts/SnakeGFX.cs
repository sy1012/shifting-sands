using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGFX : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aiPath.desiredVelocity);
        transform.rotation *= Quaternion.Euler(0, 0, -90);
        
    }
}
