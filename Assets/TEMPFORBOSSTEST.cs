using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPFORBOSSTEST : MonoBehaviour
{
    public GameObject boss1;
    public GameObject boss2;

    // Start is called before the first frame update
    void Start()
    {
        if (boss1.transform.parent.gameObject.activeSelf && boss2.transform.parent.gameObject.activeSelf)
        {
            boss1.SetActive(false);
            boss2.SetActive(false);
            Debug.Log("That is a little cheeky lol one boss at a time");
        }
        if (!boss1.transform.parent.gameObject.activeSelf && !boss2.transform.parent.gameObject.activeSelf)
        {
            Debug.Log("BORING");
        }
        else
        {
            if (boss1.transform.parent.gameObject.activeSelf) { boss1.GetComponent<Boss1Rhoss>().startFight = true; }
            else { boss2.GetComponent<Boss2Anubis>().startFight = true; }
        }
    }
}
