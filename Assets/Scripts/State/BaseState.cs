using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{

    public abstract void EnterState(StateManager stateManager);

    public abstract void HandleEvent(string eventName);

    public abstract void ExitState(StateManager stateManager);
    
    public abstract string GetNameMenuPair();

    public abstract void removeMenuPair();
}
