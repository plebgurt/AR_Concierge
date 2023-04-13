using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MenuPairBase
{
    public abstract void EnterState(StateManager stateManager);

    public abstract void HandleEvent(string eventName);

    public abstract void ExitState(StateManager stateManager);
}
