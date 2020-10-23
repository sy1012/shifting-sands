using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

//base enemy class
public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public bool isMousedOver = false;
    
    //below are used to instantiate and manage the healthbar
    public Canvas healthCanvasPrefab;
    protected Canvas healthCanvas;
    private Healthbar healthbar;
    Camera cam;
    RaycastHit2D[] hits;


    // Start is called before the first frame update
    void Awake()
    {
        healthCanvas = Instantiate(healthCanvasPrefab, transform.position, transform.rotation, gameObject.transform);
        healthCanvas.sortingLayerName = "UI";
        health = maxHealth;
        healthbar = healthCanvas.GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(maxHealth);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
		{
            Destroy(gameObject);
		}

        //raycast from mouse to see if it is over the enemy - must use raycast to be able to detect when under a different collider
        isMousedOver = false;
        hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        foreach(RaycastHit2D hit in hits)
		{
            if (hit.collider.name == name)
            {
                isMousedOver = true;
                break;
            }
            else
            {
                isMousedOver = false;
            }
        }
		

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
    }

}
