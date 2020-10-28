using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidManager : MonoBehaviour
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

    //Creates pyramids around any new oases
    public void generatePyramids()
    {

        //check that the new pyramid is far enough away from the oasis and other pyramids, if not, try new position
        //while (Physics2D.OverlapCircleAll(activePyramids[activePyramids.Count - 1].transform.position, 3f).Length > 0)

        //Physics2D.SyncTransforms(); //needed for the pyramid to recognize other pyramids made in this frame
    }


    public void newOasis(Vector2 position, float radius)
    {
        oases.Add(Instantiate(oasisPrefab, position, Quaternion.identity));
        oases[oases.Count - 1].setRadius(radius);
    }

}
