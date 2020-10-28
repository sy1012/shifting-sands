﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour
{
    float zoomSize = 2;

    // Update is called once per frame
    void Update()
    {
        // get values if WASD was pressed in any way
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // move the camera
        if (Camera.current != null){
            Camera.current.transform.Translate(new Vector3(x * 0.03f, y * 0.03f, 0.0f));
        }

        // adjusting zoom
        // zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0){
            if (zoomSize > 2){
                zoomSize -= 1;
            }
        }
        // zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            if (zoomSize < 5){
                zoomSize += 1;
            }
        }
        GetComponent<Camera>().orthographicSize = zoomSize;
    }
}
