using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObject : MonoBehaviour
{
    public ConsumableData data;
    protected SpriteRenderer sr;           // Quick Reference to the Sprite Renderer attached to this object  

    // when a consumable is picked up you imediantly apply its effects
    public void PickedUp()
    {
        DungeonMaster.loot.Remove(this.gameObject);
        if (GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").GetComponent<PlayerStateMachine>().Heal(data.healAmount);
        }
        Destroy(this.gameObject);
    }

    public void Dropped()
    {
        Debug.Log(data.name);
        this.sr = this.gameObject.AddComponent<SpriteRenderer>();
        this.sr.sprite = data.sprite;
        this.transform.localScale = this.data.spriteScaling;
        this.sr.sortingLayerName = "Player";
        this.gameObject.AddComponent<BoxCollider2D>();
        if (this.GetComponent<Rigidbody2D>() == null) this.gameObject.AddComponent<Rigidbody2D>();
        Vector2 force = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
        this.GetComponent<Rigidbody2D>().AddForce(force);
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.GetComponent<Rigidbody2D>().drag = data.relativeWeight;
    }
}
