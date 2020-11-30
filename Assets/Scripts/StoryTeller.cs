using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryTeller : MonoBehaviour
{
    public GameObject Text;
    public escClick button;
    public float scrollSpeed;  // this is as a percent of screen size
    private float endPosition;
    private float screenScrollSpeed;
    private blackfade fade;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 70;

        // how fast should the text really scroll
        screenScrollSpeed = (Camera.main.ViewportToWorldPoint(new Vector2(scrollSpeed, scrollSpeed)) -
            Camera.main.ViewportToWorldPoint(new Vector2(0, 0))).y;

        // when is the text off the screen
        endPosition = (Text.GetComponent<RectTransform>().rect.height + Camera.main.transform.localScale.y);

        fade = FindObjectOfType<blackfade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fade.fadein)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Text.GetComponent<RectTransform>().localPosition.y >= endPosition || button.clicked)
            {
                fade.fadeout = true;
            }
            else
            {
                Text.transform.position = new Vector2(Text.transform.position.x, Text.transform.position.y + screenScrollSpeed);
            }
        }
        
        if (fade.finished)
        {
            EnterOverworld();
        }

    }
       

    void EnterOverworld()
    {
        SceneManager.LoadScene("Overworld_Scot");
    }
}
