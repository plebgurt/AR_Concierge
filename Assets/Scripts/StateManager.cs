using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    internal static StateManager CurrentStateManager;
    internal BaseState currentState;
    internal MainMenuState mainMenuState = new MainMenuState();
    internal QRState qrState = new QRState();
    
    
    // Start is called before the first frame update
    void Awake()
    {
        CurrentStateManager ??= this;
    }

    internal void StartStateManager()
    {
        currentState = qrState;
        qrState.EnterState(CurrentStateManager);
    }
    
    internal void ChangeMenuState(BaseState baseState)
    {
        currentState.ExitState(CurrentStateManager);
        currentState = baseState;
        baseState.EnterState(CurrentStateManager);
    }
}
