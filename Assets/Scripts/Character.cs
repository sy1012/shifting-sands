using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isMousedOver = false;
    public int maxHealth = 100;
    public int health;
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
    private void OnMouseOver()
    {
        isMousedOver = true;
    }

    private void OnMouseExit()
    {
        isMousedOver = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
    }
}
