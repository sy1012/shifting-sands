//using UnityEditor;
using GraphGrammars;
using UnityEngine;
public class EncounterNodeComponent : NodeComponent
{
    public int Difficulty;
    private void Awake()
    {
        symbol = Symbol.Encounter;
        EventManager.onDungeonGenerated += SpawnMonstersInRoom;
    }

    private void SpawnMonstersInRoom(System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        float ks = 0.2f;
        float kr = .01f;
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
        //Repluse agains all first neighbour's neighbours
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
        Gizmos.color = (Color.red+Color.white/(1f+Difficulty));
        Gizmos.DrawSphere(transform.position, 0.11f+0.1f*Difficulty);
    }
}

