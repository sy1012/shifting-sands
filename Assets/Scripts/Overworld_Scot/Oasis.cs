using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;

public class Oasis : MonoBehaviour
{
    //The circle that shows the radius you can travel from an oasis
    public Perimeter radVisualize;
    public Perimeter circle;

    public OasisNode oasisNode;



    public float radius = 6f;

    //whether pyramids have been generated for this oasis or not
    public bool generated = false;

    //Pyramid for spawning
    public Pyramid pyramidPrefab;
    public List<Pyramid> pyramids = new List<Pyramid>();

    private void Start()
    {
        //Create the travel radius visual
        circle = Instantiate(radVisualize, transform);
        circle.oasis = this;

        //create pyramids
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
            //create within travel radius
            pyramids.Add(Instantiate(pyramidPrefab, transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * radius * 0.85f, transform.rotation));
            pyramids[pyramids.Count - 1].SetParentOasis(this);
            pyramids[pyramids.Count - 1].Reposition(); //check to make sure it is in a valid spot
        }
    }

}
