using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour
{
    float zoomSize = 10;
    Caravan follow;

	private void Start()
	{
        follow = FindObjectOfType<Caravan>();
	}

	// Update is called once per frame
	void Update()
    {
        // get values if WASD was pressed in any way
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // move the camera
        if (Camera.current != null){
            Camera.current.transform.Translate(follow.transform.position);
        }

        // adjusting zoom
        // zoom in
        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0){
            if (zoomSize > 6){
                zoomSize -= 1;
            }
        }
        // zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            if (zoomSize < 19){
                zoomSize += 1;
            }
        }*/
        GetComponent<Camera>().orthographicSize = zoomSize;
    }

    private void LateUpdate()
    {
        Vector3 clampMovement = transform.position;
        float CamSize = Camera.main.orthographicSize;
        float aspect = Camera.main.aspect;


        clampMovement.x = Mathf.Clamp(clampMovement.x, -33f + CamSize * aspect, 35f - CamSize * aspect);
        clampMovement.y = Mathf.Clamp(clampMovement.y, -18.9f + CamSize, 19.1f - CamSize);

        transform.position = clampMovement;
    }
}
