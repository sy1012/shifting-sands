using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnOpenInventory : MonoBehaviour
{
    public MonoBehaviour script;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.onOpenInventory += InventoryOpened;
        EventManager.onCloseInventory += InventoryClosed;
    }

    private void InventoryClosed(object sender, System.EventArgs e)
    {
        if (script != null)
        {
            script.enabled = true;
        }
    }

    private void InventoryOpened(object sender, System.EventArgs e)
    {
        if (script != null)
        {
            script.enabled = false;
        }
    }
}
