using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    
    private Oasis parentOasis;
    private MapManager pyramidManager;
    bool mousedOver = false;

    // Start is called before the first frame update
    void Start()
    {
        pyramidManager = FindObjectOfType<MapManager>();
    }

    public void SetParentOasis(Oasis parent)
    {
        parentOasis = parent;
    }

    //for testing of transformation into a new oasis
    private void TransformToOasis()
    {
        //spawn a new oasis where pyramid is and get rid of the pyramid
        pyramidManager.NewOasis(transform.position, 6f, parentOasis);
        Destroy(gameObject);
    }

    private void Update()
    {
        
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
            if (hit.collider == GetComponent<Collider2D>())
                {        
                    //for testing transformation into oasis, rightclick pyramid to simulate beating the dungeon
                    if (Input.GetMouseButtonDown(1))
                    {
                        TransformToOasis();
                    }
                    mousedOver = true;
                    break;
                }
                mousedOver = false;
            }
            if (mousedOver)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.8f, 0.8f), 5 * Time.deltaTime);
            }
            else
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.5f, 0.5f), 5 * Time.deltaTime);

            }

        }
    }

    //Find a suitable position for the pyramid
    public void Reposition(int count = 0)
    {

        //check for oasis collisions

        //if it can't find a suitable spot after 100 iterations, assume it doesn't exist and abort
        if (count >= 100)
        {
            parentOasis.pyramids.Remove(this);
            Destroy(gameObject);
            return;
        }

        //Circlecast to find what the pyramid is close to
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D overlap in overlaps)
        {
            //check each overlap to see if it is an oasis -blocks below act the same but with different check radii and targets
            if (overlap.gameObject.GetComponent<Oasis>() != null)
            {
                transform.position = parentOasis.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * parentOasis.radius * 0.85f;
                
                //if too close, try again
                Reposition(count + 1);
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
                    Reposition(count + 1);
                    return;
                }
            }
        }

        //ensure that new pyramids are outside of previously discovered oasis radii

        overlaps = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        //check that the pyramid has spawned inbounds
        bool onMap = false;

        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.gameObject.GetComponent<Perimeter>() != null)
            {
                if (overlap.transform != parentOasis.circle.transform)
                {
                    {
                        transform.position = parentOasis.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * parentOasis.radius * 0.85f;
                        Reposition(count + 1);
                        return;
                    }
                }
            }

            if (overlap.gameObject.GetComponent<MapManager>() != null)
            {
                onMap = true;
            }


        }

        //if pyramid is off the map try again
        if (!onMap)
        {
            transform.position = parentOasis.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * parentOasis.radius * 0.85f;
            Reposition(count + 1);
            return;
        }

    }
}
