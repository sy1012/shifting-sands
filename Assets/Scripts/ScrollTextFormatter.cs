using TMPro;
using UnityEngine;

// purpose of this class is to contain the logic needed to format a message and a background so that it looks good
public static class ScrollTextFormatter
{
    const int MINWRAP = 10;   // how small should the number of characters be before we start wrapping
    const float PADDING = 1.2f;  // how much larger do we want the scroll to be than the text

    // Avoid using this where possible if the message is likely to be repeated often as it has the overhead of creating
    // Brand new objects
    // returns (textObj, scrollObj)
    public static (GameObject, GameObject) CreateAssetsFromScratch(string text, Sprite scrollSprite, Transform parent=null, string textName="text", string scrollName="textScroll")
    {
        // Innitialize everything
        TextMeshPro textComponent = new GameObject(textName).AddComponent<TextMeshPro>();
        GameObject scroll = new GameObject(scrollName);
        textComponent.sortingOrder = 12;
        SpriteRenderer sr = scroll.AddComponent<SpriteRenderer>();
        sr.sprite = scrollSprite;
        sr.sortingOrder = 11;
        
        // if they set a parent for this object
        if (parent != null)
        {
            scroll.transform.SetParent(parent);
            textComponent.gameObject.transform.SetParent(parent);
        }

        // get some initial measurents of our data
        float xSize = sr.sprite.bounds.size.x;
        float ySize = sr.sprite.bounds.size.y;
        float targetRatio = ySize / xSize;
        float area = (xSize/(xSize+ySize) * (ySize / (xSize + ySize)));
        Debug.Log(area);
        Debug.Log(sr.sprite.bounds.size.y);

        textComponent.fontSize = 4;
        textComponent.text = text;

        scroll.transform.localScale = new Vector2((1 / xSize) * Mathf.Sqrt(text.Length*1.9f) * area, targetRatio * (1/ySize) * Mathf.Sqrt(text.Length*1.9f) * area);
        textComponent.rectTransform.sizeDelta = new Vector2(sr.bounds.size.x, sr.bounds.size.y);
        scroll.transform.localScale *= PADDING;
        textComponent.alignment = TextAlignmentOptions.Midline;

        return (textComponent.gameObject, scroll);
    }
}
