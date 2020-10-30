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

    private Oasis currentOasis;


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
        oases[oases.Count - 1].setRadius(radius);

        OasisNode node = new OasisNode(oases[oases.Count -1].transform.name + oases.Count, oases[oases.Count - 1]);
        oasisGraph.AddNode(node);
        oases[oases.Count - 1].oasisNode = node;

        oasisGraph.AddConnection(node, parent.oasisNode);
        
        if(newOasesHandler != null)
        {
            newOasesHandler(this, EventArgs.Empty);
        }


    }

}
