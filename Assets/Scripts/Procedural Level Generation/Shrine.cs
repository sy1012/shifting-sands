
using UnityEngine;
//using UnityEditor;
public class Shrine : Interactable
{
    /// <summary>
    /// Try to heal the interactor
    /// </summary>
    /// <param name="interactor"></param>
    public override void Interact(GameObject interactor)
    {
        IHealable target = interactor.GetComponent<IHealable>();
        if(target == null) { return; }
        target.Heal(10);
        Debug.Log("Heal " + interactor.name);
    }
}

