using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTakeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoDamage();
        }
    }
    private void DoDamage()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray,out hit);
        Debug.Log(hit.collider.name);
        if (hit.collider != null && hit.collider.GetComponent<EnviromentBreakable>() != null)
        {
            Debug.Log("Take damage");
            hit.collider.GetComponent<EnviromentBreakable>().TakeDamage(100);
        }
    }
}
