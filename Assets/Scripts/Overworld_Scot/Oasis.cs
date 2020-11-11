using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Oasis : MonoBehaviour
{
    //The circle that shows the radius you can travel from an oasis
    public Perimeter radVisualize;
    public Perimeter circle;

    public OasisNode oasisNode;

    public LineRenderer pyramidLinePrefab;

    public List<Oasis> oases = new List<Oasis>();
    public List<LineRenderer> oasisLines = new List<LineRenderer>();

    public float radius = 6f;

    //whether pyramids have been generated for this oasis or not
    public bool generated = false;

    //Pyramid for spawning
    public Pyramid pyramidPrefab;
    public List<Pyramid> pyramids = new List<Pyramid>();
    public Dictionary<Pyramid, LineRenderer> pyramidDict = new Dictionary<Pyramid, LineRenderer>();
    

    public List<LineRenderer> pyramidLines = new List<LineRenderer>();
    private Caravan caravan;
    private MapManager mapManager;


    private void Start()
    {
        //Create the travel radius visual
        circle = Instantiate(radVisualize, transform);
        circle.oasis = this;

        caravan = FindObjectOfType<Caravan>();
        mapManager = FindObjectOfType<MapManager>();

        //create pyramids
        if (!generated)
        {
            generatePyramids();
        }
        

    }

    private void Update()
    {

        //Scale linerenderers out to other Oases
        for (int i = 0; i < oasisLines.Count; i++)
        {
            if (oasisLines[i] != null && oasisLines[i].GetPosition(1) != transform.position)
            {
                oasisLines[i].SetPosition(1, Vector2.MoveTowards(oasisLines[i].GetPosition(1), transform.position, 10f * Time.deltaTime));
            }
        }


        //scale linerenders out to pyramids if caravan is at this oasis
        if (caravan.currentNode == oasisNode)
        {
            foreach(LineRenderer line in pyramidLines)
            {
                if (line.GetPosition(1) != pyramids[pyramidLines.IndexOf(line)].transform.position)
                {
                    line.SetPosition(1, Vector2.MoveTowards(line.GetPosition(1), pyramids[pyramidLines.IndexOf(line)].transform.position, 10f * Time.deltaTime));
                }
            }
        }
		else
		{
            foreach (LineRenderer line in pyramidLines)
            {
                if (line.GetPosition(1) != transform.position)
                {
                    line.SetPosition(1, Vector2.MoveTowards(line.GetPosition(1), transform.position, 10f * Time.deltaTime));
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
        for (int i = 0; i < numPyramids; i++)
        {
            //create within travel radius
            pyramids.Add(Instantiate(pyramidPrefab, transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * radius * 0.85f, transform.rotation));
            pyramids[pyramids.Count - 1].SetParentOasis(this);
            pyramids[pyramids.Count - 1].Reposition(); //check to make sure it is in a valid spot

            //create a line if a new pyramid was successfully generated
            if (pyramids.Count > pyramidLines.Count)
            {
                pyramidLines.Add(Instantiate(pyramidLinePrefab));

                Vector3[] points = new Vector3[2];
                points[0] = (Vector3)transform.position;
                points[1] = points[0];
                pyramidLines[pyramidLines.Count - 1].startWidth = 0.2f;
                pyramidLines[pyramidLines.Count - 1].endWidth = 0.2f;
                pyramidLines[pyramidLines.Count - 1].SetPositions(points);

                mapManager.pyramids.Add(pyramids[pyramids.Count - 1]);
            }
            

        }
    }

    //add pyramids from other oases to this oasis
    public void addExistingPyramid(Pyramid pyramid)
    {
        //check that the pyramid doesnt already belong to this oasis
        if (!pyramids.Contains(pyramid))
        {
            pyramids.Add(pyramid);
            pyramidLines.Add(Instantiate(pyramidLinePrefab));

            Vector3[] points = new Vector3[2];
            points[0] = (Vector3)transform.position;
            points[1] = points[0];
            pyramidLines[pyramidLines.Count - 1].startWidth = 0.2f;
            pyramidLines[pyramidLines.Count - 1].endWidth = 0.2f;
            pyramidLines[pyramidLines.Count - 1].SetPositions(points);
        }
    }

    public void addNearbyOases()
    {

    }

}
