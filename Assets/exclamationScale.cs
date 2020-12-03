using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exclamationScale : MonoBehaviour
{
    RectTransform rTransform;

    bool grow = true;
    bool shrink = false;

    private void Start()
    {
        rTransform = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if(grow)
        {
            if(rTransform.sizeDelta.x <= 0.7)
            {
                rTransform.sizeDelta += new Vector2(0.02f, 0.02f);
            }
            else
            {
                grow = false;
                shrink = true;
            }
        }
        else if (shrink)
        {
            if (rTransform.sizeDelta.x >= 0.5)
            {
                rTransform.sizeDelta -= new Vector2(0.02f, 0.02f);
            }
            else
            {
                grow = true;
                shrink = false;
            }
        }
    }
}
