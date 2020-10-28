using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oasis : MonoBehaviour
{
    public Perimeter radVisualize;
    public Perimeter circle;
    public float radius = 4f;
    public bool generated = false;
    public Pyramid pyramidPrefab;
    public List<Pyramid> pyramids = new List<Pyramid>();

    private void Start()
    {
        circle = Instantiate(radVisualize, transform);
        circle.transform.localScale = new Vector3(radius * 2 / 5, radius * 2 / 5, 1);

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
        pyramids.Add(Instantiate(pyramidPrefab, transform.position + (Vector3)Random.insideUnitCircle * radius, transform.rotation));
        pyramids[pyramids.Count - 1].setParentOasis(this);
        pyramids[pyramids.Count - 1].reposition();
        
    }

}
