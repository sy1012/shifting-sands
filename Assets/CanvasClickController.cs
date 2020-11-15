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

    // Normal raycasts do not work on UI elements, they require a special kind
    GraphicRaycaster raycaster;

    void Awake()
    {
        GameObject temp = Instantiate(inventoryButton);
        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        temp.transform.SetParent(this.transform);
        temp.GetComponent<RectTransform>().localPosition = new Vector2(100, 130);

        temp = Instantiate(craftingButton);
        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        temp.transform.SetParent(this.transform);
        temp.GetComponent<RectTransform>().localPosition = new Vector2(50, 130);

        temp = Instantiate(runeMerchantButton);
        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        temp.transform.SetParent(this.transform);
        temp.GetComponent<RectTransform>().localPosition = new Vector2(0, 130);

        temp = Instantiate(weaponMerchantButton);
        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        temp.transform.SetParent(this.transform);
        temp.GetComponent<RectTransform>().localPosition = new Vector2(-50, 130);

        temp = Instantiate(armourMerchantButton);
        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        temp.transform.SetParent(this.transform);
        temp.GetComponent<RectTransform>().localPosition = new Vector2(-100, 130);

        EventManager.onDungeonGenerated += EnteringDungeon;
        EventManager.OnExitDungeon += ExitDungeon;

        ExitDungeon(new EventArgs());

        // Get both of the components we need to do this
        this.raycaster = GetComponent<GraphicRaycaster>();
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void ExitDungeon(System.EventArgs e)
    {
        Debug.Log("Exiting");
        armourMerchantButton.SetActive(true);
        weaponMerchantButton.SetActive(true);
        craftingButton.SetActive(true);
        runeMerchantButton.SetActive(true);
        inventoryButton.SetActive(true);
    }

    private void EnteringDungeon(System.EventArgs e)
    {
        Debug.Log("Entering");
        armourMerchantButton.SetActive(false);
        weaponMerchantButton.SetActive(false);
        craftingButton.SetActive(false);
        runeMerchantButton.SetActive(false);
        inventoryButton.SetActive(false);
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

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
                else if (result.gameObject.name == "Armour Merchant Clone(Clone)")
                {
                    EventManager.TriggerOnArmourMerchant();
                }
                else if (result.gameObject.name == "Weapon Merchant Clone(Clone)")
                {
                    EventManager.TriggerOnWeaponMerchant();
                }
                else if (result.gameObject.name == "Rune Merchant Clone(Clone)")
                {
                    EventManager.TriggerOnRuneMerchant();
                }
                else if (result.gameObject.GetComponent<Slot>() != null)
                {
                    GameObject.Find("Inventory").GetComponent<Inventory>().slotHovered = result.gameObject.GetComponent<Slot>();

                }
            }
        }
    }
}
