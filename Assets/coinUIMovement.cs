using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinUIMovement : MonoBehaviour
{

    CoinDisplay coinDisplay;

    // Start is called before the first frame update
    void Start()
    {
        coinDisplay = MonoBehaviour.FindObjectOfType<CoinDisplay>();
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, coinDisplay.transform.localPosition, 40f);

        if(Vector2.Distance(transform.localPosition, coinDisplay.transform.localPosition) <= 5f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
