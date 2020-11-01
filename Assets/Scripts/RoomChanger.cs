using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{

    // The Room Changer Animator
    public Animator animator;

    // Update is called once per frame
    void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.DoorEntered += EventManager_DoorEntered;
       
    }

    private void EventManager_DoorEntered(System.EventArgs e)
    {
        animator.SetTrigger("Fade_Out");
        
    }

}
    
