using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;

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
        // generate between 1 and 3 pyramids
        int numPyramids = UnityEngine.Random.Range(1, 4);
        for(int i = 1; i <= numPyramids; i++)
        {
            pyramids.Add(Instantiate(pyramidPrefab, transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * radius * 0.85f, transform.rotation));
            pyramids[pyramids.Count - 1].SetParentOasis(this);
            pyramids[pyramids.Count - 1].Reposition();
        }
    }

}
