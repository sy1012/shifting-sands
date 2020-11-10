//using UnityEditor;
using GraphGrammars;
using UnityEngine;
public class EncounterNode : NodeComponent
{
    public int Difficulty;
    private void Awake()
    {
        symbol = Symbol.Encounter;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = (Color.red+Color.white/(1f+Difficulty));
        Gizmos.DrawSphere(transform.position, 0.11f+0.1f*Difficulty);
    }
}

