using UnityEngine;

// The most complicated of scripts
public class CraftingButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        EventManager.TriggerOnCrafting();
    }
}
