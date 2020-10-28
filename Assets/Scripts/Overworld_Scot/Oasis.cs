using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oasis : MonoBehaviour
{
    public Perimeter radVisualize;
    public Perimeter circle;

    public SpriteMask lineMaskPrefab;
    private SpriteMask lineMask;

    public float radius = 6f;
    public bool generated = false;
    public Pyramid pyramidPrefab;
    public List<Pyramid> pyramids = new List<Pyramid>();

    private void Start()
    {
        circle = Instantiate(radVisualize, transform);
        circle.transform.localScale = new Vector3(radius * 2 / 5, radius * 2 / 5, 1);

        lineMask = Instantiate(lineMaskPrefab, transform);
        lineMask.transform.localScale = circle.transform.localScale * 0.99f;


        generatePyramids();
    }

    public float getRadius()
    {
        return radius;
    }

    public void setRadius(float rad)
    {
        radius = rad;
    }

    public void generatePyramids()
    {
        int numPyrmaids = Random.Range(1, 4);
        for(int i = 1; i <= numPyrmaids; i++)
        {
            pyramids.Add(Instantiate(pyramidPrefab, transform.position + (Vector3)Random.insideUnitCircle * radius * 0.85f, transform.rotation));
            pyramids[pyramids.Count - 1].setParentOasis(this);
            pyramids[pyramids.Count - 1].reposition();
        }

        
    }

}
