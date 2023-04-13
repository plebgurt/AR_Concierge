using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRState : BaseState
{
    internal MenuPairBase MenuPairBase;
    public override void EnterState(StateManager stateManager)
    { 
        Debug.LogWarning(MenuPairBase.name + " has spawned");
        MenuPairBase.SetMenuPairActive(true);
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent += HandleEvent;
    }

    public override void HandleEvent(string eventName)
    {
        Debug.Log(eventName);
    }

    public override void ExitState(StateManager stateManager)
    {
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent -= HandleEvent;
        MenuPairBase.SetMenuPairActive(false);
    }
}
