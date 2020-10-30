using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour {

    public GameObject trail;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject instance = (GameObject)Instantiate(trail, transform.position, Quaternion.identity);
            Destroy(instance, 8f);
        }
    }
}
