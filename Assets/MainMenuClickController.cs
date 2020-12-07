using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuClickController : MonoBehaviour
{
    // Normal raycasts do not work on UI elements, they require a special kind
    GraphicRaycaster raycaster;
    PointerEventData pointerData;
    List<RaycastResult> results;
    blackfade fade;
    
    public MenuSelection script;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        fade = FindObjectOfType<blackfade>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            pointerData = new PointerEventData(EventSystem.current);
            results = new List<RaycastResult>();
            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                // trigger sound!
                //EventManager.TriggerOnInventorySwap();
                if (result.gameObject.name == "NewButton")
                {
                    EventManager.TriggerOnInventorySwap();
                    script.load = false;
                    script.loadInventory = false;
                    fade.fadeout = true;
                }
                else if (result.gameObject.name == "LoadButton")
                {
                    EventManager.TriggerOnInventorySwap();
                    if (File.Exists(Application.persistentDataPath + "/overworld.sav") || File.Exists(Application.persistentDataPath + "/inventory.sav")){
                        script.load = true;
                        script.loadInventory = true;
                        fade.fadeout = true;
                    }
                }
            }
        }
        if (fade.finished)
        {
            if (script.load)
            {
                SceneManager.LoadScene("Overworld_Scot");
            }
            else
            {
                SceneManager.LoadScene("Story");
            }
        }
    }
}
