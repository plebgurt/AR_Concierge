using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState
{
    internal MenuPairBase MenuPairBase;
    public override void EnterState(StateManager stateManager)
    {
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent += HandleEvent;
        MenuPairBase.SpawnElements(stateManager.tabletCanvas.transform, stateManager.monitorCanvas.transform);
        MenuPairBase.SetMenuPairActive(true);
    }

    public override void HandleEvent(string eventName)
    {
        StateManager.CurrentStateManager.HandleEventFromMenu(eventName);
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
