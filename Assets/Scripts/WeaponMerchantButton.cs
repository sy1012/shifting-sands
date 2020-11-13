using UnityEngine;

// The most complicated of scripts
public class WeaponMerchantButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        EventManager.TriggerOnWeaponMerchant();
    }
}
