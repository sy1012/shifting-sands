using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidManager : MonoBehaviour
{
    public Pyramid pyramidPrefab;
    public Oasis oasisPrefab;

    private List<Pyramid> activePyramids = new List<Pyramid>();
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

        generatePyramids();
    }

    //Creates pyramids around any new oases
    public void generatePyramids()
    {
        foreach(Oasis oasis in oases)
        {
            //only generate for oases that dont have pyramids yet
            if (!oasis.generated)
            {
                // random amount based on min and max values from the oasis
                int numPyramids = Random.Range(oasis.getMinPyramids(), oasis.getMaxPyramids());
                for (int i = 1; i <= numPyramids; i++)
                {
                    //create pyramid within a radius from the oasis
                    activePyramids.Add(Instantiate(pyramidPrefab, oasis.transform.position + (Vector3)Random.insideUnitCircle * oasis.getRadius(), oasis.transform.rotation));
                    int counter = 0;

                    //check that the new pyramid is far enough away from the oasis and other pyramids, if not, try new position
                    while (Physics2D.OverlapCircleAll(activePyramids[activePyramids.Count - 1].transform.position, 3f).Length > 0)
                    {
                        Physics2D.SyncTransforms(); //needed for the pyramid to recognize other pyramids made in this frame
                        counter++;
                        activePyramids[activePyramids.Count - 1].transform.position = oasis.transform.position + (Vector3)Random.insideUnitCircle * oasis.getRadius();
                        if (counter >= 1000) //infinite loop protection
                        {
                            break;
                        }
                    }
                }
            }
            oasis.generated = true;
        }
    }

    public void newOasis(Vector2 position, float radius, int maxPyramids, int minPyramids)
    {
        oases.Add(Instantiate(oasisPrefab, position, Quaternion.identity));
        oases[oases.Count - 1].setRadius(radius);
        oases[oases.Count - 1].setMinPyramids(minPyramids);
        oases[oases.Count - 1].setMaxPyramids(maxPyramids);
    }

}
