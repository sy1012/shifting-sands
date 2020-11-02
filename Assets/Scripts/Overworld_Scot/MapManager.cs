using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;
using System;

public class MapManager : MonoBehaviour
{
    public Pyramid pyramidPrefab;
    public Oasis oasisPrefab;
    public List<Oasis> oases = new List<Oasis>();
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

    public void NewOasis(Vector2 position, float radius, Oasis parent, Pyramid parentPyramid)
    {
        Oasis newOasis = Instantiate(oasisPrefab, position, Quaternion.identity);
        oases.Add(newOasis);
        newOasis.setRadius(radius);

        OasisNode node = new OasisNode(newOasis.transform.name + oases.Count, newOasis);
        oasisGraph.AddNode(node);
        newOasis.oasisNode = node;

        oasisGraph.AddConnection(node, parent.oasisNode);

        // Add other oases in the radius to connections
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(newOasis.transform.position, newOasis.radius * 0.8f);
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
            if (overlap.transform.gameObject.GetComponent<Pyramid>() != null && overlap.transform.gameObject.GetComponent<Pyramid>() != parentPyramid)
            {
                Pyramid pyramid = overlap.transform.gameObject.GetComponent<Pyramid>();

                newOasis.addExistingPyramid(pyramid);

            }
        }

        //create a line between the new oasis and its parent/nearby siblings
        foreach(Node nearbyNode in oasisGraph.GetAdjByNode(newOasis.oasisNode).GetAdj())
        {
            newOasis.oases.Add(((OasisNode)nearbyNode).getOasis());
            LineRenderer path = Instantiate(pathPrefab);
            Vector3[] points = new Vector3[2];
            points[0] = (Vector3)((OasisNode)nearbyNode).getOasis().transform.position;
            points[1] = points[0];
            path.startWidth = 0.2f;
            path.endWidth = 0.2f;
            path.SetPositions(points);
            newOasis.oasisLines.Add(path);
        }


        if (newOasesHandler != null)
        {
            newOasesHandler(this, EventArgs.Empty);
        }


    }

}
