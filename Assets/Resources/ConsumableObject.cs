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
            StartCoroutine(PickedUpRoutine());
        }
    }

    IEnumerator PickedUpRoutine()
    {
        bool done = true;
        //TODO ADD EFFEECT IN THIS LOOP
        while (!done)
        {
            Vector3 distToCorner = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(100f, 50f, Camera.main.nearClipPlane));
            transform.position = Vector3.Lerp(transform.position, transform.position + distToCorner,0.1f);
            if (distToCorner.magnitude < 0.2f)
            {
                done = true;
            }
            yield return null;
        }
        GameObject.Find("Player").GetComponent<PlayerStateMachine>().Heal(data.healAmount);
        Destroy(this.gameObject);
    }

    public void Dropped()
    {
        Debug.Log(data.name);
        Instantiate(data.prefab,this.transform);
        this.sr = this.gameObject.AddComponent<SpriteRenderer>();
        this.sr.sprite = data.sprite;
        this.transform.localScale = this.data.spriteScaling;
        this.sr.sortingLayerName = "Player";
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        /*if (this.GetComponent<Rigidbody2D>() == null) this.gameObject.AddComponent<Rigidbody2D>();
        Vector2 force = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
        this.GetComponent<Rigidbody2D>().freezeRotation = true;
        this.GetComponent<Rigidbody2D>().AddForce(force);
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.GetComponent<Rigidbody2D>().drag = data.relativeWeight;*/
    }
}
