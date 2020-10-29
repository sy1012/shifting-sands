using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oasis : MonoBehaviour
{
    public Perimeter radVisualize;
    public Perimeter circle;

    public float radius = 6f;
    public bool generated = false;
    public Pyramid pyramidPrefab;
    public List<Pyramid> pyramids = new List<Pyramid>();

    private void Start()
    {
        circle = Instantiate(radVisualize, transform);
        circle.oasis = this;
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
            pyramids[pyramids.Count - 1].SetParentOasis(this);
            pyramids[pyramids.Count - 1].Reposition();
        }

        
    }

}
