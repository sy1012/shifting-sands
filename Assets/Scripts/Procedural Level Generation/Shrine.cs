
using UnityEngine;
//using UnityEditor;
public class Shrine : Interactable
{
    public override void Interact(GameObject interactor)
    {
        IHealable target = interactor.GetComponent<IHealable>();
        if(target == null) { return; }
        target.Heal(10);
        Debug.Log("Heal " + interactor.name);
    }
}

