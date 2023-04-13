using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState
{
    internal MenuPairBase MenuPairBase;
    public override void EnterState(StateManager stateManager)
    {
        Debug.LogWarning(MenuPairBase + " has spawned");
        MenuPairBase.SetMenuPairActive(true);
        MenuPairBase.SpawnedTablet.transform.SetParent(stateManager.tabletCanvas.transform);
        MenuPairBase.SpawnedMonitor.transform.SetParent(stateManager.monitorCanvas.transform);
        
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
