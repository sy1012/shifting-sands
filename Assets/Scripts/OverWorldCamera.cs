using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPos;
    public Vector2 minPos;
    public Sprite inventoryButtonBackground;

    private GameObject inventoryButton;
    private GameObject inventoryButtonText;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 70;

        // set up the inventory button
        (inventoryButtonText, inventoryButton) = Formatter.CreateAssetsFromScratch("Inventory", inventoryButtonBackground, this.gameObject.transform, "Inventory Button Text", "Inventory Button Background");
        //inventoryButton = new GameObject("Inventory Button");
        //inventoryButton.transform.SetParent(this.gameObject.transform);
        inventoryButton.AddComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.9f, .9f));
        inventoryButton.transform.position += new Vector3(0, 0, -8);
        inventoryButtonText.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.9f, .9f));
        inventoryButton.GetComponent<RectTransform>().localScale *= new Vector2(4, 4);
        inventoryButtonText.GetComponent<RectTransform>().localScale *= new Vector2(4, 4);
        inventoryButtonText.GetComponent<TextMeshPro>().fontSize = 2;
        inventoryButton.AddComponent<InventoryButton>();
        inventoryButton.AddComponent<BoxCollider2D>().isTrigger = true;
        inventoryButton.AddComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != target.position)
        {
            Vector3 targetposition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetposition.x = Mathf.Clamp(targetposition.x, minPos.x, maxPos.x);
            targetposition.y = Mathf.Clamp(targetposition.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetposition, smoothing);
        }

    }
}