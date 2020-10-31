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

    public LineRenderer pyramidLinePrefab;



    public float radius = 6f;

    //whether pyramids have been generated for this oasis or not
    public bool generated = false;

    //Pyramid for spawning
    public Pyramid pyramidPrefab;
    public List<Pyramid> pyramids = new List<Pyramid>();
    public LineRenderer newLine;

    public List<LineRenderer> pyramidLines = new List<LineRenderer>();
    private Caravan caravan;

    private void Start()
    {
        //Create the travel radius visual
        circle = Instantiate(radVisualize, transform);
        circle.oasis = this;

        caravan = FindObjectOfType<Caravan>();

        //create pyramids
        generatePyramids();

    }

    private void Update()
    {
        if (newLine != null && newLine.GetPosition(1) != transform.position)
        {
            newLine.SetPosition(1, Vector2.MoveTowards(newLine.GetPosition(1), transform.position, 10f * Time.deltaTime));
        }

        if (caravan.currentNode == oasisNode)
        {
            for (int i = 0; i < pyramidLines.Count; i++)
            {
                if (pyramidLines[i] != null && pyramidLines[i].GetPosition(1) != pyramids[i].transform.position)
                {
                    pyramidLines[i].SetPosition(1, Vector2.MoveTowards(pyramidLines[i].GetPosition(1), pyramids[i].transform.position, 10f * Time.deltaTime));
                }
            }
        }
		else
		{
            for (int i = 0; i < pyramidLines.Count; i++)
            {
                if (pyramidLines[i] != null && pyramidLines[i].GetPosition(1) != transform.position)
                {
                    pyramidLines[i].SetPosition(1, Vector2.MoveTowards(pyramidLines[i].GetPosition(1), transform.position, 10f * Time.deltaTime));
                }
            }
        }

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
        for(int i = 0; i < numPyramids; i++)
        {
            //create within travel radius
            pyramids.Add(Instantiate(pyramidPrefab, transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * radius * 0.85f, transform.rotation));
            pyramids[i].SetParentOasis(this);
            pyramids[i].Reposition(); //check to make sure it is in a valid spot

            if (pyramids.Count == i + 1)
            {
                pyramidLines.Add(Instantiate(pyramidLinePrefab));

                Vector3[] points = new Vector3[2];
                points[0] = (Vector3)transform.position;
                points[1] = points[0];
                pyramidLines[i].startWidth = 0.2f;
                pyramidLines[i].endWidth = 0.2f;
                pyramidLines[i].SetPositions(points);
            }
        }
    }

}
