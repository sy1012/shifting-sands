using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Rune")]
public class RuneData : ItemData
{
    public RuneType.Type type;
    public float effectiveness;
}
