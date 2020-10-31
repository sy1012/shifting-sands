using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan : MonoBehaviour
{
    Vector2 targetposition;
    public List<Node> path;
    public OasisNode currentNode;
    PlayerOverworldTraversal traversal;

    // Start is called before the first frame update
    void Start()
    {
        traversal = FindObjectOfType<PlayerOverworldTraversal>();
    }

    // Update is called once per frame
    void Update()
    {
        if(path.Count != 0)
		{
            currentNode = (OasisNode)path[0];
            traversal.currentNode = (OasisNode)currentNode;
            if (transform.position != ((OasisNode)path[0]).getOasis().transform.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, ((OasisNode)path[0]).getOasis().transform.position, 10f * Time.deltaTime);
            }
			else
			{
                path.RemoveAt(0);
            }
        }
        
    }
}
