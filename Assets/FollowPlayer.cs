using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 atkHeading = (mouse - psm.transform.position);
            atkHeading = new Vector3(atkHeading.x, atkHeading.y, 0).normalized;
            transform.position = (psm.transform.position + new Vector3(psm.GetRoot().x, psm.GetRoot().y, 0)) / 2 + atkHeading * 0.7f;
        }
        
    }
}
