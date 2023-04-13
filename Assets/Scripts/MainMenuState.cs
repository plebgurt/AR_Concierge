using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState
{

    public override void EnterState(StateManager stateManager)
    {
        SetActive(true);
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent += HandleEvent;
    }

    public override void HandleEvent(string eventName)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(StateManager stateManager)
    {
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent -= HandleEvent;
        SetActive(false);
    }
}
