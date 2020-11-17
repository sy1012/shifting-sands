using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures that at runtime that we create one and only one set of objects that persist throughtought the game
// These objects include the Canvas and equipment
public class InventoryChecker : MonoBehaviour
{
    public GameObject equipmentPrefab;
    public GameObject CanvasPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find("Canvas") == null)
        {
            GameObject temp = Instantiate(CanvasPrefab);
            temp.name = "Canvas";
            temp = new GameObject("Inventory");
            temp.transform.SetParent(GameObject.Find("Canvas").transform);
            temp.AddComponent<Inventory>();
            temp = Instantiate(equipmentPrefab);
            temp.name = "Equipment";
        }
    }
}
