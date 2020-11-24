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
    
    public MenuSelection script;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
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
                EventManager.TriggerOnInventorySwap();
                if (result.gameObject.name == "NewButton")
                {
                    script.load = false;
                    SceneManager.LoadScene("Story");
                }
                else if (result.gameObject.name == "LoadButton")
                {
                    if (File.Exists(Application.persistentDataPath + "overworld.sav")){
                        script.load = true;
                        SceneManager.LoadScene("Overworld_Scot");
                    }
                }
            }
        }
    }
}
