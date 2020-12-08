using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHealable, IDamagable
{
    public bool isMousedOver = false;
    public int maxHealth = 100;
    public int health;
    public int weight = 3;
    //below are used to instantiate and manage the healthbar
    public Canvas healthCanvasPrefab;
    protected Canvas healthCanvas;
    protected Healthbar healthbar;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        healthCanvas = Instantiate(healthCanvasPrefab, transform.position, transform.rotation, gameObject.transform);
        maxHealth = (int)(maxHealth * Mathf.Pow((1.1f), 1/(GameObject.Find("DungeonData").GetComponent<DungeonDataKeeper>().blessingValue)));
        Debug.Log(maxHealth);
        health = maxHealth;
        healthbar = healthCanvas.GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(maxHealth);
    }
    private void OnMouseOver()
    {
        isMousedOver = true;
    }

    private void Update()
    {

    }

    private void OnMouseExit()
    {
        isMousedOver = false;
    }

    public virtual void TakeDamage(int damage, Collision2D collision)
    {
        //Knockback
        Vector2 colliderPos = new Vector2(collision.transform.position.x, collision.transform.position.y);
        Vector2 chacterPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 pushBackDir = (chacterPos - colliderPos).normalized;
        Vector2 pushBackVector = pushBackDir / weight;
        transform.position += new Vector3(pushBackVector.x,pushBackVector.y,0);
        //Take damage
        TakeDamage(damage);
    }
    public virtual void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            if(gameObject.GetComponent<Enemy>() != null)
			{
                Debug.Log("Char 111");
                gameObject.GetComponent<Enemy>().generateLoot();

            }
            Destroy(this.gameObject);
        }
        healthbar.SetHealth(health);
    }
    public void Heal(int amount)
    {
        // trigger shrine usage sound effect
        // EventManager.TriggerOnUseShrine();
        health+=amount;
        if (health > maxHealth) { health = maxHealth; }
        healthbar.SetHealth(health);
    }
    
    public virtual void OnDestroy()
    {
        return;
    }
}
