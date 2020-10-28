using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    public Oasis oasis;
    private PyramidManager pyramidManager;

    // Start is called before the first frame update
    void Start()
    {
        pyramidManager = FindObjectOfType<PyramidManager>();
    }

    //for testing of transformation into a new oasis
    private void OnMouseDown()
    {
        pyramidManager.newOasis(transform.position, 6f, 2, 3);
        Destroy(gameObject);
        pyramidManager.generatePyramids();
    }
}
