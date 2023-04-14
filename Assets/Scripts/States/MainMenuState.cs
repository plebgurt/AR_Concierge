using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState
{
    internal MenuPairBase MenuPairBase;
    public override void EnterState(StateManager stateManager)
    {
        Debug.LogWarning(MenuPairBase + " has spawned");
        MenuPairBase.SpawnElements(stateManager.tabletCanvas.transform, stateManager.monitorCanvas.transform);
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
