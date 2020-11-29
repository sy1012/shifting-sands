using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanAnimationController : MonoBehaviour

{
    public Caravan caravan;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero;
        if (caravan.currentNode != null)
        {
            direction = caravan.currentNode.getOasis().transform.position - caravan.transform.position;
           
        }
        if(direction == Vector2.zero)
        {
            animator.Play("Player_Idle_Down");
        }
        else
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    animator.Play("Player_Walk_Right");
                }
                else
                {
                    animator.Play("Player_Walk_Left");
                }
            }

            else if (Mathf.Abs(direction.y) >= Mathf.Abs(direction.x))
            {
                if(direction.y > 0)
                {
                    animator.Play("Player_Walk_Up");
                }
                else
                {
                    animator.Play("Player_Walk_Down");
                }
            }
        }
    }
}
