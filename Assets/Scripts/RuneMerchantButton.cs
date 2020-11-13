using UnityEngine;

// The most complicated of scripts
public class RuneMerchantButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        EventManager.TriggerOnRuneMerchant();
    }
}