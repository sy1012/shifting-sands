using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryTeller : MonoBehaviour
{
    public GameObject Text;
    public float scrollSpeed;  // this is as a percent of screen size
    private float endPosition;
    private float screenScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 70;

        // how fast should the text really scroll
        screenScrollSpeed = (Camera.main.ViewportToWorldPoint(new Vector2(scrollSpeed, scrollSpeed)) -
            Camera.main.ViewportToWorldPoint(new Vector2(0, 0))).y;

        // when is the text off the screen
        endPosition = (-Text.GetComponent<RectTransform>().rect.y
            + (Camera.main.ViewportToScreenPoint(new Vector2(1, 1)) - Camera.main.ViewportToScreenPoint(new Vector2(.5f, .5f))).y);

        Debug.Log(endPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Text.GetComponent<RectTransform>().localPosition.y >= endPosition)
        {
            EnterOverworld();
        }
        Text.transform.position = new Vector2(Text.transform.position.x, Text.transform.position.y + screenScrollSpeed);
    }
       
    void EnterOverworld()
    {
        SceneManager.LoadScene("Overworld_Scot");
    }
}
