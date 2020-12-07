using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisReflection : MonoBehaviour
{
    public Vector2 target;
    private float timer = 10;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("WEEEEE");
        this.transform.position = Vector2.Lerp(this.transform.position, target, .05f);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
