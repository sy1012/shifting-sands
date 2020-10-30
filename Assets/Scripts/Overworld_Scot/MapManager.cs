using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Pyramid pyramidPrefab;
    public Oasis oasisPrefab;
    private List<Oasis> oases = new List<Oasis>();

    // Start is called before the first frame update
    void Start()
    {
        //Find starting Oasis
        Oasis[] currentOases = FindObjectsOfType<Oasis>();
        foreach(Oasis oasis in currentOases)
        {
            oases.Add(oasis);
        }
    }

    public void NewOasis(Vector2 position, float radius)
    {
        oases.Add(Instantiate(oasisPrefab, position, Quaternion.identity));
        oases[oases.Count - 1].setRadius(radius);
    }

}
