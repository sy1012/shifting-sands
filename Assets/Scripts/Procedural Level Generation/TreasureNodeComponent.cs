//using UnityEditor;
using GraphGrammars;
using UnityEngine;

public class TreasureNodeComponent: NodeComponent
{
    public int Value;
    private void Awake()
    {
        symbol = Symbol.Treasure;
        EventManager.onDungeonGenerated += SpawenTreasure;
    }

    private void SpawenTreasure(System.EventArgs e)
    {
        DungeonGenArgs de = (DungeonGenArgs)e;

        //Weird bug work around caused by previous dungeon nodes not being destroyed in time?
        if (neighbours.Count == 0)
        {
            //Debug.Log("Make Treasure Old Node: " + transform.name);
            return;
        }

        Room room = de.generator.GetRoom(neighbours[0]);
        GameObject newBox = (GameObject) Resources.Load("BreakableBox");
        for (int i = 0; i < Value; i++)
        {
            var instance = Instantiate(newBox);
            room.PlaceObject(instance);
            instance.transform.position += Vector3.up * UnityEngine.Random.Range(-0.75f, 0.75f) + Vector3.right * UnityEngine.Random.Range(-1, 1);
            instance = Instantiate(newBox);
            room.PlaceObject(instance);
            instance.transform.position += Vector3.up * UnityEngine.Random.Range(-0.75f, 0.75f) + Vector3.right * UnityEngine.Random.Range(-1, 1);
        }
    }
    private void OnDestroy()
    {
        EventManager.onDungeonGenerated -= SpawenTreasure;
    }
    public void Update()
    {
        float ks = 0.2f;
        float kr = 0.01f;
        float neutralLength = 3;
        Vector2 fs = Vector2.zero;
        Vector2 fr = Vector2.zero;
        Vector2 nodePos = new Vector2(transform.position.x, transform.position.y);
        //Calculate Spring Force with other Connected Nodes
        foreach (var neighbour in neighbours)
        {
            // Find spring force for this neighbour
            Vector2 neighbourPos = new Vector2(neighbour.transform.position.x, neighbour.transform.position.y);
            Vector2 dir = (neighbourPos - nodePos).normalized;
            fs += dir * ks * ((neighbourPos - nodePos).magnitude - neutralLength);
        }
        transform.position = transform.position + new Vector3(fs.x, fs.y, 0);
        foreach (var neighbourNeighbour in neighbours[0].neighbours)
        {
            if (transform == neighbourNeighbour.transform)
            {
                continue;
                //Don't act repulse self
            }
            Vector2 otherNodePos = new Vector2(neighbourNeighbour.transform.position.x, neighbourNeighbour.transform.position.y);
            //Debug.Log(otherNodePos + "" + nodePos);
            Vector2 dir = (otherNodePos - nodePos).normalized;
            float dist = (otherNodePos - nodePos).magnitude;
            fr += dir * kr / (dist * dist);
        }
        transform.position = transform.position + new Vector3(fr.x, fr.y, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = (Color.yellow + Color.white / (1f + Value));
        Gizmos.DrawSphere(transform.position, 0.05f + 0.1f * Value);
    }
}

