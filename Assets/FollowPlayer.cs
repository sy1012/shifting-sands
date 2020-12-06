using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    PlayerStateMachine psm;

    // Start is called before the first frame update
    void Start()
    {
        psm = FindObjectOfType<PlayerStateMachine>();
    }

    void LateUpdate()
    {
        if(psm == null)
        {
            psm = FindObjectOfType<PlayerStateMachine>();
        }
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = psm.transform.position + (mouse - psm.transform.position).normalized * 3;
    }
}
