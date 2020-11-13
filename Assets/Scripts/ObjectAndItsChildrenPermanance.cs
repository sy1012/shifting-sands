using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAndItsChildrenPermanance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnsurePermanance(this.gameObject);
    }

    // Makes sure that this object and all its children will not be destroyed when changing scenes
    void EnsurePermanance(GameObject obj)
    {
        DontDestroyOnLoad(obj);
        int i = 0;
        while(i < obj.transform.childCount)
        {
            EnsurePermanance(obj.transform.GetChild(0).gameObject);
            i += 1;
        }
    }
}
