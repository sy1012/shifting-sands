using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Statemachine : MonoBehaviour
{
    protected State currentState;
    public void SetState(State state)
    {
        currentState.Exit();
        currentState = state;
    }

}

