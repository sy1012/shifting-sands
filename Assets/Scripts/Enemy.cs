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

    

    // Start is called before the first frame update
    void Awake()
    {
        healthCanvas = Instantiate(healthCanvasPrefab, transform.position, transform.rotation, gameObject.transform);
        health = maxHealth;
        healthbar = healthCanvas.GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
		{
            Destroy(gameObject);
		}

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
    }

    private void OnMouseDown()
    {
        /* For testing healthbar
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(10);
        }
        */
    }

    private void OnMouseOver()
	{
        isMousedOver = true;  
	}

	private void OnMouseExit()
	{
        isMousedOver = false;
	}
}
