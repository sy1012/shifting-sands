using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{

    public Transform target;
    public float smoothing;
    public Vector2 maxPos;
    public Vector2 minPos;

    // Update is called once per frame
    void Update()
    {
        // the player could die at which point the player scripts will handle the event of death
        try
        {
            if (transform.position != target.position)
            {
                Vector3 targetposition = new Vector3(target.position.x, target.position.y, transform.position.z);
                targetposition.x = Mathf.Clamp(targetposition.x, minPos.x, maxPos.x);
                targetposition.y = Mathf.Clamp(targetposition.y, minPos.y, maxPos.y);
                transform.position = Vector3.Lerp(transform.position, targetposition, smoothing);
            }
        }
        catch { } // ignore on purpose...see try
    }
}
