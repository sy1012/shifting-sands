//using UnityEditor;
using GraphGrammars;
using UnityEngine;

public class TreasureNode: NodeComponent
{
    public int Value;
    private void Awake()
    {
        symbol = Symbol.Treasure;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = (Color.yellow + Color.white / (1f + Value));
        Gizmos.DrawSphere(transform.position, 0.05f + 0.1f * Value);
    }
}

