using UnityEngine;

// The most complicated of scripts
public class InventoryButton : MonoBehaviour
{   private void OnMouseDown()
    {
        EventManager.TriggerOnInventoryTrigger();
    }
}
