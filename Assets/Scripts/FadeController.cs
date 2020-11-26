using UnityEngine;

public class FadeController : MonoBehaviour
{

    private static FadeController instance = null;

    public static FadeController GetInstance()
    {
        return instance;
    }

    // The Room Changer Animator
    public Animator animator;

    private void Awake()
    {
        // One to rule them all
        if (instance == null)
        {
            instance = this;
        }
        //  "There can be only one!"
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        EventManager.DoorEntered += EventManager_DoorEntered;
    }

    private void EventManager_DoorEntered(System.EventArgs e)
    {
        animator.SetTrigger("Fade_Out");
    }

    public static void PlayFadeOut()
    {
        instance.animator.SetTrigger("Fade_Out_Hold");
    }
}
    
