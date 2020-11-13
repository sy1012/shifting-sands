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

    public Sprite inventoryButtonSprite;
    public Sprite armourMerchantSprite;
    public Sprite weaponMerchantSprite;
    public Sprite runeMerchantSprite;
    public Sprite craftingSprite;

    private GameObject armourMerchantButton;
    private GameObject weaponMerchantButton;
    private GameObject runeMerchantButton;
    private GameObject craftingButton;
    private GameObject inventoryButton;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 70;

        // set up the various buttons needed for the overworld
        inventoryButton = Formatter.ScaleSpriteToPercentOfScreen(inventoryButtonSprite, new Vector2(0.1f, 0.1f), 16);
        inventoryButton.transform.SetParent(this.transform);
        inventoryButton.gameObject.name = "Inventory Button";
        inventoryButton.AddComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.7f, .95f));
        inventoryButton.transform.position += new Vector3(0, 0, -8);
        inventoryButton.AddComponent<InventoryButton>();
        inventoryButton.AddComponent<BoxCollider2D>().isTrigger = true;
        inventoryButton.AddComponent<Rigidbody2D>().gravityScale = 0;

        weaponMerchantButton = Formatter.ScaleSpriteToPercentOfScreen(weaponMerchantSprite, new Vector2(0.1f, 0.1f), 16);
        weaponMerchantButton.transform.SetParent(this.transform);
        weaponMerchantButton.gameObject.name = "Weapon Merchant Button";
        weaponMerchantButton.AddComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.6f, .95f));
        weaponMerchantButton.transform.position += new Vector3(0, 0, -8);
        weaponMerchantButton.AddComponent<WeaponMerchantButton>();
        weaponMerchantButton.AddComponent<BoxCollider2D>().isTrigger = true;
        weaponMerchantButton.AddComponent<Rigidbody2D>().gravityScale = 0;

        armourMerchantButton = Formatter.ScaleSpriteToPercentOfScreen(armourMerchantSprite, new Vector2(0.1f, 0.1f), 16);
        armourMerchantButton.transform.SetParent(this.transform);
        armourMerchantButton.gameObject.name = "Armour Merchant Button";
        armourMerchantButton.AddComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.5f, .95f));
        armourMerchantButton.transform.position += new Vector3(0, 0, -8);
        armourMerchantButton.AddComponent<ArmourMerchantButton>();
        armourMerchantButton.AddComponent<BoxCollider2D>().isTrigger = true;
        armourMerchantButton.AddComponent<Rigidbody2D>().gravityScale = 0;

        runeMerchantButton = Formatter.ScaleSpriteToPercentOfScreen(runeMerchantSprite, new Vector2(0.1f, 0.1f), 16);
        runeMerchantButton.gameObject.name = "Rune Merchant Button";
        runeMerchantButton.transform.SetParent(this.transform);
        runeMerchantButton.AddComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.4f, .95f));
        runeMerchantButton.transform.position += new Vector3(0, 0, -8);
        runeMerchantButton.AddComponent<RuneMerchantButton>();
        runeMerchantButton.AddComponent<BoxCollider2D>().isTrigger = true;
        runeMerchantButton.AddComponent<Rigidbody2D>().gravityScale = 0;

        craftingButton = Formatter.ScaleSpriteToPercentOfScreen(craftingSprite, new Vector2(0.1f, 0.1f), 16);
        craftingButton.transform.SetParent(this.transform);
        craftingButton.gameObject.name = "Crafting Button";
        craftingButton.AddComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(new Vector2(.3f, .95f));
        craftingButton.transform.position += new Vector3(0, 0, -8);
        craftingButton.AddComponent<CraftingButton>();
        craftingButton.AddComponent<BoxCollider2D>().isTrigger = true;
        craftingButton.AddComponent<Rigidbody2D>().gravityScale = 0;
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