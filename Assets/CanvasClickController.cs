using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CanvasClickController : MonoBehaviour
{
    public GameObject inventoryButton;
    public GameObject runeMerchantButton;
    public GameObject weaponMerchantButton;
    public GameObject armourMerchantButton;
    public GameObject craftingButton;

    public GameObject exclamationMark;

    private GameObject _inventoryButton;
    private GameObject _runeMerchantButton;
    private GameObject _weaponMerchantButton;
    private GameObject _armourMerchantButton;
    private GameObject _craftingButton;

    private GameObject inventoryMark;
    private GameObject runeMark;
    private GameObject weaponMark;
    private GameObject armourMark;
    private GameObject craftingMark;



    private bool clicked;

    // Normal raycasts do not work on UI elements, they require a special kind
    GraphicRaycaster raycaster;

    void Awake()
    {

        Inventory inventory = FindObjectOfType<Inventory>();
        MenuSelection load = FindObjectOfType<MenuSelection>();

        _inventoryButton = Instantiate(inventoryButton);
        _inventoryButton.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
        _inventoryButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        _inventoryButton.transform.SetParent(this.transform);
        _inventoryButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.7f, .95f));

        if (!load.loadInventory)
        {
            inventoryMark = Instantiate(exclamationMark);
            inventoryMark.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
            inventoryMark.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            inventoryMark.transform.SetParent(this.transform);
            inventoryMark.GetComponent<RectTransform>().position = _inventoryButton.GetComponent<RectTransform>().position + new Vector3(30, -30);
        }
        

        _craftingButton = Instantiate(craftingButton);
        _craftingButton.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
        _craftingButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        _craftingButton.transform.SetParent(this.transform);
        _craftingButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.6f, .95f));

        if (!load.loadInventory)
        {
            craftingMark = Instantiate(exclamationMark);
            craftingMark.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
            craftingMark.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            craftingMark.transform.SetParent(this.transform);
            craftingMark.GetComponent<RectTransform>().position = _craftingButton.GetComponent<RectTransform>().position + new Vector3(30, -30);
        }

        _runeMerchantButton = Instantiate(runeMerchantButton);
        _runeMerchantButton.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
        _runeMerchantButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        _runeMerchantButton.transform.SetParent(this.transform);
        _runeMerchantButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .95f));

        if (!load.loadInventory)
        {
            runeMark = Instantiate(exclamationMark);
            runeMark.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
            runeMark.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            runeMark.transform.SetParent(this.transform);
            runeMark.GetComponent<RectTransform>().position = _runeMerchantButton.GetComponent<RectTransform>().position + new Vector3(30, -30);
        }

        _weaponMerchantButton = Instantiate(weaponMerchantButton);
        _weaponMerchantButton.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
        _weaponMerchantButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        _weaponMerchantButton.transform.SetParent(this.transform);
        _weaponMerchantButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.4f, .95f));

        if (!load.loadInventory)
        {
            weaponMark = Instantiate(exclamationMark);
            weaponMark.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
            weaponMark.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            weaponMark.transform.SetParent(this.transform);
            weaponMark.GetComponent<RectTransform>().position = _weaponMerchantButton.GetComponent<RectTransform>().position + new Vector3(30, -30);
        }

        _armourMerchantButton = Instantiate(armourMerchantButton);
        _armourMerchantButton.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
        _armourMerchantButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        _armourMerchantButton.transform.SetParent(this.transform);
        _armourMerchantButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.3f, .95f));

        if (!load.loadInventory)
        {
            armourMark = Instantiate(exclamationMark);
            armourMark.transform.localScale = (Camera.main.ViewportToScreenPoint(new Vector2(.05f, .1f)));
            armourMark.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            armourMark.transform.SetParent(this.transform);
            armourMark.GetComponent<RectTransform>().position = _armourMerchantButton.GetComponent<RectTransform>().position + new Vector3(30, -30);
        }

        EventManager.onResubscribeOverworld += OverworldSubscribe;
        EventManager.onResubscribeDungeon += DungeonSubscribe;
        EventManager.onInventoryTrigger += hideInvExclamation;
        EventManager.onWeaponMerchant += hideWeapExclamation;
        EventManager.onArmourMerchant += hideArmExclamation;
        EventManager.onRuneMerchant += hideRuneExclamation;
        EventManager.onCrafting += hideCraftExclamation;

        // Get components we need to do this
        this.raycaster = GetComponent<GraphicRaycaster>();
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void DungeonSubscribe(EventArgs e)
    {
        _armourMerchantButton.SetActive(false);
        _weaponMerchantButton.SetActive(false);
        _craftingButton.SetActive(false);
        _runeMerchantButton.SetActive(false);
        _inventoryButton.SetActive(false);
    }

    private void OverworldSubscribe(EventArgs e)
    {
        _armourMerchantButton.SetActive(true);
        _weaponMerchantButton.SetActive(true);
        _craftingButton.SetActive(true);
        _runeMerchantButton.SetActive(true);
        _inventoryButton.SetActive(true);
    }

    private void hideInvExclamation(object sender, System.EventArgs e)
    {
        inventoryMark.SetActive(false);
    }

    private void hideWeapExclamation(object sender, System.EventArgs e)
    {
        weaponMark.SetActive(false);
    }

    private void hideArmExclamation(object sender, System.EventArgs e)
    {
        armourMark.SetActive(false);
    }

    private void hideRuneExclamation(object sender, System.EventArgs e)
    {
        runeMark.SetActive(false);
    }

    private void hideCraftExclamation(object sender, System.EventArgs e)
    {
        craftingMark.SetActive(false);
    }

    void Update()
    {
        //Set up the new Pointer Event
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        pointerData.position = Input.mousePosition;
        this.raycaster.Raycast(pointerData, results);

        bool foundSomething = false;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Slot>() != null)
            {
                foundSomething = true;
                GameObject.Find("Inventory").GetComponent<Inventory>().slotHovered = result.gameObject.GetComponent<Slot>();
            }
            else if (!foundSomething)
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().slotHovered = null;
            }
        }


        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clicked = false;

            //Set up the new Pointer Event
            pointerData = new PointerEventData(EventSystem.current);
            results = new List<RaycastResult>();
            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

            foundSomething = false;
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name == "Inventory Button(Clone)")
                {
                    EventManager.TriggerOnInventoryTrigger();
                }
                else if (result.gameObject.name == "Crafting Button(Clone)")
                {
                    EventManager.TriggerOnCrafting();
                }
                else if (result.gameObject.name == "Armour Merchant Button(Clone)")
                {
                    EventManager.TriggerOnArmourMerchant();
                }
                else if (result.gameObject.name == "Weapon Merchant Button(Clone)")
                {
                    EventManager.TriggerOnWeaponMerchant();
                }
                else if (result.gameObject.name == "Rune Merchant Button(Clone)")
                {
                    EventManager.TriggerOnRuneMerchant();
                }
                else if (result.gameObject.name == "Interact Button")
                {
                    foundSomething = true;
                    GameObject.Find("Inventory").GetComponent<Inventory>().Interact();
                }
                else if (result.gameObject.GetComponent<Slot>() != null)
                {
                    foundSomething = true;
                    GameObject.Find("Inventory").GetComponent<Inventory>().SetHeld(result.gameObject.GetComponent<Slot>(), result.gameObject.GetComponent<Slot>().RetrieveItem());
                }
                else if (!foundSomething)
                {
                    GameObject.Find("Inventory").GetComponent<Inventory>().slotHovered = null;
                }
            }
        }
    }
}
