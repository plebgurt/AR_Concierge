using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRState : BaseState
{
    internal MenuPairBase MenuPairBase;
    public override void EnterState(StateManager stateManager)
    { 
        MenuPairBase.SetMenuPairActive(true);
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent += HandleEvent;
    }

    public override void HandleEvent(string eventName)
    {
        
    }

    public override void ExitState(StateManager stateManager)
    {
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent -= HandleEvent;
        MenuPairBase.SetMenuPairActive(false);
    }

    public override string GetNameMenuPair()
    {
        return MenuPairBase.name;
    }
}
