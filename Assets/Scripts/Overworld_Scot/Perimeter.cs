using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perimeter : MonoBehaviour
{
    public Oasis oasis;
    public SpriteMask lineMaskPrefab;
    private SpriteMask lineMask;
    private bool grown = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
        lineMask = Instantiate(lineMaskPrefab, oasis.transform);
        lineMask.transform.localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
		if (!grown)
		{
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(oasis.radius * 2 / 5, oasis.radius * 2 / 5, 1), 10f * Time.deltaTime);
            lineMask.transform.localScale = transform.localScale;
            if(transform.localScale.x == oasis.radius * 2 / 5)
			{
                grown = true;
			}
        }

        transform.Rotate(0, 0, 25 * Time.deltaTime);
        lineMask.transform.Rotate(0, 0, 25 * Time.deltaTime);
    }
}
