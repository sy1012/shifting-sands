using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perimeter : MonoBehaviour
{
    public Oasis oasis;
    public SpriteMask lineMaskPrefab;
    private SpriteMask lineMask;
    private bool grown = false;
    private bool grow = false;

    // Start is called before the first frame update
    void Start()
    {
        //make it small when first created
        transform.localScale = new Vector3(0.1f, 0.1f, 1);

        //spawn a sprite mask as well to allow circles to overlap and combine
        lineMask = Instantiate(lineMaskPrefab, oasis.transform);
        lineMask.transform.localScale = transform.localScale;

        if (!oasis.old)
        {
            StartCoroutine(WaitToGrow(1.5f));
        }
        else
        {
            grow = true;
        }
        
    }

    private IEnumerator WaitToGrow(float secs)
    {
        yield return new WaitForSeconds(secs);
        grow = true;
    }

    // Update is called once per frame
    void Update()
    {
        //lerp size of the circle until it reaches the intended size
		if ((!grown && grow) || FindObjectsOfType<Oasis>().Length == 1)
		{
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(oasis.radius * 2 / 5, oasis.radius * 2 / 5, 1), 10f * Time.deltaTime);
            lineMask.transform.localScale = transform.localScale;
            if(transform.localScale.x == oasis.radius * 2 / 5)
			{
                grown = true;
			}
        }

        //rotate the circle at a constant rate
        transform.Rotate(0, 0, 25 * Time.deltaTime);
        lineMask.transform.Rotate(0, 0, 25 * Time.deltaTime);
    }
}
