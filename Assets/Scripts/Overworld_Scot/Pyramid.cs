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
        pyramidManager.newOasis(transform.position, 4f);
        Destroy(gameObject);
        pyramidManager.generatePyramids();
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

        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.gameObject.GetComponent<Oasis>() != null)
            {
                if (overlap.gameObject != this.gameObject)
                {
                    transform.position = parentOasis.transform.position + (Vector3)Random.insideUnitCircle * parentOasis.radius;
                    reposition();
                    return;
                }
            }
        }

        overlaps = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.gameObject.GetComponent<Perimeter>() != null)
                if(overlap.transform != parentOasis.circle.transform)
                {
                    {
                        transform.position = parentOasis.transform.position + (Vector3)Random.insideUnitCircle * parentOasis.radius;
                        reposition();
                        return;
                    }
                }
            
        }
    }
}
