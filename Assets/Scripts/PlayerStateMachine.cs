using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] public LayerMask dashLayerMask;
    public Rigidbody2D rb;
    public Animator animator;
    GameObject text;
    GameObject background;
    public float playerRootOffset = -0.5f;
    public Weapon weaponEquiped;
    KeyCode interactKey = KeyCode.E;
    KeyCode rollKey = KeyCode.Space;
    

    public State state;
    public StateEnum currentState;
    public float speed;

    public void SetState(State newstate)
    {
        currentState = newstate.stateEnum;
        state = newstate;
        StartCoroutine(newstate.Enter());
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(new NormalState(this));
    }

    public Vector2 GetArrowKeysDirectionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }

    public void MoveCharacter(Vector2 heading, float speed)
    {
        rb.MovePosition(rb.position + heading * speed * Time.fixedDeltaTime);
    }

    public void NormalMovement()
    {
        Vector2 input = GetArrowKeysDirectionalInput();
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
        MoveCharacter(GetArrowKeysDirectionalInput(), speed);
    }
    public void NormalMovementFraction(float fraction)
    {
        Vector2 input = GetArrowKeysDirectionalInput();
        animator.SetFloat("Horizontal", input.x*fraction);
        animator.SetFloat("Vertical", input.y*fraction);
        animator.SetFloat("Speed", input.sqrMagnitude*fraction);
        MoveCharacter(GetArrowKeysDirectionalInput(), speed*fraction);
    }

    // Update is called once per frame
    void Update()
    {
        //State Specific Behaviour
        if (Input.GetKeyDown(interactKey))
        {
            StartCoroutine(state.Interact());
        }

        if (Input.GetKeyDown(rollKey))
        {
            StartCoroutine(state.Roll());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Change State to Attack");
            StartCoroutine(state.Attack());
        }

        state.Execute();

        //Global Player State Behaviour
        if (DungeonMaster.getLootInRange(this.transform.position, 1).Count != 0)
        {
            (text, background) = DungeonMaster.getLootInRange(this.transform.position, 1)[0].GetComponent<Weapon>().Info();
            text.transform.position = this.transform.position + new Vector3(0, 2, 0);
            background.transform.position = this.transform.position + new Vector3(0, 2, 0);
        }
        if (DungeonMaster.getLootOuttaRange(this.transform.position, 1).Count != 0)
        {
            List<GameObject> loot = DungeonMaster.getLootOuttaRange(this.transform.position, 1);
            foreach (GameObject item in loot)
            {
                item.GetComponent<Weapon>().DestroyInfo();
            }
        }

    }
    public Vector2 GetRoot()
    {
        return new Vector2(transform.position.x, transform.position.y + playerRootOffset);
    }
}
