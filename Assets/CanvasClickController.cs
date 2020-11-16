﻿using System.Collections;
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

    private GameObject _inventoryButton;
    private GameObject _runeMerchantButton;
    private GameObject _weaponMerchantButton;
    private GameObject _armourMerchantButton;
    private GameObject _craftingButton;

    private bool clicked;

    // Normal raycasts do not work on UI elements, they require a special kind
    GraphicRaycaster raycaster;

    void Start()
    {
        _inventoryButton = Instantiate(inventoryButton);
        _inventoryButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        _inventoryButton.transform.SetParent(this.transform);
        _inventoryButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.7f, .95f));

        _craftingButton = Instantiate(craftingButton);
        _craftingButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        _craftingButton.transform.SetParent(this.transform);
        _craftingButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.6f, .95f));

        _runeMerchantButton = Instantiate(runeMerchantButton);
        _runeMerchantButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        _runeMerchantButton.transform.SetParent(this.transform);
        _runeMerchantButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .95f));

        _weaponMerchantButton = Instantiate(weaponMerchantButton);
        _weaponMerchantButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        _weaponMerchantButton.transform.SetParent(this.transform);
        _weaponMerchantButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.4f, .95f));

        _armourMerchantButton = Instantiate(armourMerchantButton);
        _armourMerchantButton.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        _armourMerchantButton.transform.SetParent(this.transform);
        _armourMerchantButton.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(new Vector2(.3f, .95f));

        EventManager.onDungeonGenerated += EnteringDungeon;
        EventManager.OnExitDungeon += ExitDungeon;

        ExitDungeon(new EventArgs());

        // Get both of the components we need to do this
        this.raycaster = GetComponent<GraphicRaycaster>();
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void ExitDungeon(System.EventArgs e)
    {
        _armourMerchantButton.SetActive(true);
        _weaponMerchantButton.SetActive(true);
        _craftingButton.SetActive(true);
        _runeMerchantButton.SetActive(true);
        _inventoryButton.SetActive(true);
    }

    private void EnteringDungeon(System.EventArgs e)
    {
        _armourMerchantButton.SetActive(false);
        _weaponMerchantButton.SetActive(false);
        _craftingButton.SetActive(false);
        _runeMerchantButton.SetActive(false);
        _inventoryButton.SetActive(false);
    }

    void Update()
    {
        //Set up the new Pointer Event
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        pointerData.position = Input.mousePosition;
        this.raycaster.Raycast(pointerData, results);
        
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Slot>() != null)
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().slotHovered = result.gameObject.GetComponent<Slot>();
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

            bool foundSomething = false;
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
                else if (result.gameObject.name == "Trigger")
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
                else
                {
                    Debug.Log("Clicked On: " + result.gameObject.name);
                }
            }
        }
    }
}