using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;
using System;

public class MapManager : MonoBehaviour
{
    public Pyramid pyramidPrefab;
    public Oasis oasisPrefab;
    private List<Oasis> oases = new List<Oasis>();
    public Graph oasisGraph = new Graph("oasisGraph");
    public LineRenderer pathPrefab;

    public Oasis currentOasis;


    public static event EventHandler<EventArgs> newOasesHandler;

    // Start is called before the first frame update
    void Awake()
    {
        //Find starting Oasis
        Oasis[] currentOases = FindObjectsOfType<Oasis>();
        foreach(Oasis oasis in currentOases)
        {
            oases.Add(oasis);
            OasisNode node = new OasisNode("startingOasis", oasis);
            oasisGraph.AddNode(node);
            oasis.oasisNode = node;
            
            currentOasis = oasis;
        }
    }

    public void NewOasis(Vector2 position, float radius, Oasis parent)
    {
        Oasis newOasis = Instantiate(oasisPrefab, position, Quaternion.identity);
        oases.Add(newOasis);
        newOasis.setRadius(radius);

        OasisNode node = new OasisNode(newOasis.transform.name + oases.Count, newOasis);
        oasisGraph.AddNode(node);
        newOasis.oasisNode = node;

        oasisGraph.AddConnection(node, parent.oasisNode);

        //create a line between the new oasis and its parent
        LineRenderer path = Instantiate(pathPrefab);
        Vector3[] points = new Vector3[2];
        points[0] = (Vector3)parent.transform.position;
        points[1] = points[0];
        path.startWidth = 0.2f;
        path.endWidth = 0.2f;
        path.SetPositions(points);
        newOasis.newLine = path;


        /* Would need to implement a breadth first search to make this work, or else the caravan takes illogical paths
         * Collider2D[] overlaps = Physics2D.OverlapCircleAll(newOasis.transform.position, newOasis.radius);
        foreach(Collider2D overlap in overlaps)
		{
            if(overlap.transform != newOasis.transform && overlap.transform.gameObject.GetComponent<Oasis>() != null)
			{
                Oasis sibling = overlap.transform.gameObject.GetComponent<Oasis>();

				if (!oasisGraph.GetAdjByNode(node).GetAdj().Contains(sibling.oasisNode))
				{
                    oasisGraph.AddConnection(node, sibling.oasisNode);
                }
            }
		}*/

        if (newOasesHandler != null)
        {
            newOasesHandler(this, EventArgs.Empty);
        }


    }

}
