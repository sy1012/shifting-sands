using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackfade : MonoBehaviour
{

    public bool fadein = true;
    public bool fadeout = false;
    public bool finished = false;
    private Image sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(fadein)
        {
            if (sr.color.a > 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.05f);
            }
            else
            {
                fadein = false;
            }
        }

        if (fadeout)
        {
            if (sr.color.a < 1)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + 0.05f);
            }
            else
            {
                fadeout = false;
                finished = true;
            }
        }
    }
}
