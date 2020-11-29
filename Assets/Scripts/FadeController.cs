using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{

    private static FadeController instance = null;
    [SerializeField]
    Sprite curseTextSprite;
    [SerializeField]
    Sprite blessingTextSprite;
    [SerializeField]
    Image fadeTextImage;

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
        curseTextSprite = Resources.Load<Sprite>("Sprites/CurseDampenedText");
        blessingTextSprite = Resources.Load<Sprite>("Sprites/BlessingReceivedText");
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.name.Equals("FadeText"))
            {
                fadeTextImage = child.GetComponent<Image>();
            }
        }
        if (fadeTextImage == null)
        {
            Debug.LogError("Children does not have a transform named FadeText for the needed image source");
        }
    }

    private void EventManager_DoorEntered(System.EventArgs e)
    {
        animator.SetTrigger("Fade_Out");
    }

    public static void PlayFadeOut()
    {
        instance.animator.SetTrigger("Fade_Out_Hold");
    }
    public static void PlayFadeOutText(string textOption)
    {
        // Select the correct text sprite
        if (textOption.Equals("blessing"))
        {
            instance.fadeTextImage.sprite = instance.blessingTextSprite;
        }
        else if(textOption.Equals("curse"))
        {
            instance.fadeTextImage.sprite = instance.curseTextSprite;
        }

        instance.animator.SetTrigger("Fade_Out_Text");
    }
}
    
