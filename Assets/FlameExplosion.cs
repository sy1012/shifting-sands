using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameExplosion : MonoBehaviour
{
    public void PlayExplosionForTime(float time)
    {
        StartCoroutine(Explosion(time));
    }

    private IEnumerator Explosion(float time)
    {
        GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
