using UnityEngine;

// The most complicated of scripts
public class ArmourMerchantButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        EventManager.TriggerOnArmournMerchant();
    }
}