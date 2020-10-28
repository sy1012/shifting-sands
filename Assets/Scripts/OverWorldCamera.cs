using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour
{
    float zoomSize = 8;
    private Vector2 minPos = new Vector2 (-5, -5);
    private Vector2 maxPos = new Vector2 (5, 5);

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
            if (zoomSize > 6){
                zoomSize -= 1;
            }
        }
        // zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            if (zoomSize < 16){
                zoomSize += 1;
            }
        }
        GetComponent<Camera>().orthographicSize = zoomSize;
    }

    private void LateUpdate()
    {
        Vector3 clampMovement = transform.position;
        float CamSize = Camera.main.orthographicSize;
        float aspect = Camera.main.aspect;


        clampMovement.x = Mathf.Clamp(clampMovement.x, -30f + CamSize * aspect, 30 - CamSize * aspect);
        clampMovement.y = Mathf.Clamp(clampMovement.y, -18f + CamSize, 18 - CamSize);

        transform.position = clampMovement;
    }
}
