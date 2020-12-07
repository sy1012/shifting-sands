using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures that at runtime that we create one and only one set of objects that persist throughtought the game
// These objects include the Canvas and equipment
public class SoundChecker : MonoBehaviour
{
    public GameObject soundPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find("SoundManager") == null)
        {
            GameObject temp = Instantiate(soundPrefab);
            temp.name = "SoundManager";
        }
        if (GameObject.Find("Canvas") != null)
        {
            GameObject.Destroy(GameObject.Find("Canvas"));
        }
        if (GameObject.Find("Equipment") != null)
        {
            GameObject.Destroy(GameObject.Find("Equipment"));
        }
    }

}