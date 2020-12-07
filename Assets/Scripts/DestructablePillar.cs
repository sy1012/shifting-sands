using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePillar : MonoBehaviour
{
    public Sprite rubbleSprite;
    public void TurnToRubble()
    {
        if (rubbleSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = rubbleSprite;
        }
    }
}
