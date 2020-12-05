using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camelAnimationController : MonoBehaviour
{
    public Follower camel;
    public Animator animator;
    private Transform destination;
    private Vector3 prevPosition;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        prevPosition = camel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (camel.path.Count > 0)
        {
            destination = ((OasisNode)camel.path[0]).getOasis().transform;
            direction = destination.position - camel.transform.position;
        }
        


        
        Debug.Log(direction.x);
        if (camel.next != null)
        {
            if (direction == Vector2.zero || Vector2.Distance(camel.transform.position, camel.next.transform.position) <= camel.followDistance)
            {
                animator.Play("idle");
            }
            else
            {
                animator.Play("walk");
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }
                else
                {
                    transform.localScale = new Vector3(-0.15f, 0.15f, 0.15f);
                }

            }
        }
        else
        {
            if (direction == Vector2.zero || Vector2.Distance(camel.transform.position, camel.leader.transform.position) <= camel.followDistance)
            {
                animator.Play("idle");
            }
            else
            {
                animator.Play("walk");
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                }
                else
                {
                    transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
                }

            }
        }


        prevPosition = camel.transform.position;
    }
}
