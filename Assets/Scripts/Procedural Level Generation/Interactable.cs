using UnityEngine;

/// <summary>
/// Abstact monobehaviour with the Interaction prototypes. You can Interact with objects that have a component that inherites this.
/// TODO. Turn into interface so that object can "have a interactable" instead of "is a interactable". Maybe we want players to be interactable?
/// </summary>
public abstract class Interactable:MonoBehaviour
{
    // Not used yet
    protected int priority;
    // Not used yet
    protected KeyCode key;
    /// <summary>
    /// Interface in which a interactor interacts with the interactable.
    /// </summary>
    /// <param name="interactor"></param>
    public virtual void Interact(GameObject interactor)
    {
        return;
    }

    /// <summary>
    /// Interface in which a interactor ends and interaction with the interactable
    /// Optional
    /// </summary>
    /// <param name="interactor"></param>
    public virtual void EndInteraction(GameObject interactor)
    {
        return;
    }
}
