using TMPro;
using UnityEngine;

// purpose of this class is to contain the logic needed to format a message and a background so that it looks good
public static class Formatter
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
        textComponent.sortingLayerID = SortingLayer.NameToID("Player");
        textComponent.sortingOrder = 19;
        SpriteRenderer sr = scroll.AddComponent<SpriteRenderer>();
        sr.sprite = scrollSprite;
        sr.sortingLayerName = "Player";
        sr.sortingOrder = 18;
        
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

        textComponent.fontSize = 4;
        textComponent.text = text;

        scroll.transform.localScale = new Vector2((1 / xSize) * Mathf.Sqrt(text.Length*1.9f) * area, targetRatio * (1/ySize) * Mathf.Sqrt(text.Length*1.9f) * area);
        textComponent.rectTransform.sizeDelta = new Vector2(sr.bounds.size.x, sr.bounds.size.y);
        scroll.transform.localScale *= PADDING;
        textComponent.alignment = TextAlignmentOptions.Midline;

        return (textComponent.gameObject, scroll);
    }

    // Returns an object with the text and scaled to the specified size on screen, will overflow the text area potentially
    public static GameObject ScaleTextToPercentOfScreen(string text, int textSize, Vector2 percentScreen)
    {
        GameObject textObj = new GameObject();
        Vector2 size =  Camera.main.ViewportToWorldPoint(percentScreen) - Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        TextMeshPro textComponent = textObj.AddComponent<TextMeshPro>();
        textComponent.sortingOrder = 16;
        textComponent.sortingLayerID = SortingLayer.NameToID("Player");
        textComponent.fontSize = textSize;
        textComponent.fontSizeMin = 4;
        textComponent.text = text;
        textComponent.rectTransform.sizeDelta = size;
        textComponent.alignment = TextAlignmentOptions.Midline;
        return textObj;
    }

    // Returns the Sprite given scaled to the desired percentage of the screen
    public static GameObject ScaleSpriteToPercentOfScreen(Sprite sprite, Vector2 percentScreen, int layerOrder)
    {
        GameObject obj = new GameObject();
        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        Vector2 size = Camera.main.ViewportToWorldPoint(percentScreen) - Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        float spriteScalerX = sprite.bounds.size.x;
        float spriteScalerY = sprite.bounds.size.y;
        sr.sortingOrder = layerOrder;
        sr.sortingLayerName = "Player";
        Debug.Log(size);
        obj.transform.localScale = new Vector2(1/spriteScalerX * size.x, 1/spriteScalerY * size.y);
        return obj;
    }
}
