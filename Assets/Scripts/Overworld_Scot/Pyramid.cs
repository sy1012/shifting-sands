using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{

    private Oasis parentOasis;
    public Oasis oasis;
    private PyramidManager pyramidManager;

    // Start is called before the first frame update
    void Start()
    {
        pyramidManager = FindObjectOfType<PyramidManager>();
    }

    public void setParentOasis(Oasis parent)
    {
        parentOasis = parent;
    }

    //for testing of transformation into a new oasis
    private void transformToOasis()
    {
        pyramidManager.newOasis(transform.position, 6f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
            if (hit.collider == GetComponent<Collider2D>())
                {
                    transformToOasis();
                }
            }
            
        }
    }

    public void reposition()
    {

        //check for oasis collisions
        try
        {
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, 1f);

            foreach (Collider2D overlap in overlaps)
            {
                if (overlap.gameObject.GetComponent<Oasis>() != null)
                {
                    transform.position = parentOasis.transform.position + (Vector3) UnityEngine.Random.insideUnitCircle * parentOasis.radius * 0.85f;
                    reposition();
                    return;
                }
            }

            //check that pyramids are not too close to each other
            overlaps = Physics2D.OverlapCircleAll(transform.position, 2.5f);
            Physics2D.SyncTransforms();

            foreach (Collider2D overlap in overlaps)
            {
                if (overlap.gameObject.GetComponent<Pyramid>() != null)
                {
                    if (overlap.gameObject != this.gameObject)
                    {
                        transform.position = parentOasis.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * parentOasis.radius * 0.85f;
                        reposition();
                        return;
                    }
                }
            }

            //ensure that new pyramids are outside of previously discovered oasis radii

            overlaps = Physics2D.OverlapCircleAll(transform.position, 0.1f);

            bool onMap = false;

            foreach (Collider2D overlap in overlaps)
            {
                if (overlap.gameObject.GetComponent<Perimeter>() != null)
                {
                    if (overlap.transform != parentOasis.circle.transform)
                    {
                        {
                            transform.position = parentOasis.transform.position + (Vector3) UnityEngine.Random.insideUnitCircle * parentOasis.radius * 0.85f;
                            reposition();
                            return;
                        }
                    }
                }

                if (overlap.gameObject.GetComponent<PyramidManager>() != null)
                {
                    onMap = true;
                }


            }

            //make sure the pyramid was actually placed on the map
            if (!onMap)
            {
                transform.position = parentOasis.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * parentOasis.radius * 0.85f;
                reposition();
                return;
            }




        }
        catch (StackOverflowException)
        {
            Destroy(gameObject);
        }
    }
}
