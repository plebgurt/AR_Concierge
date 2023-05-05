using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckedInState : BaseState
{
    internal MenuPairBase MenuPairBase;
    public override void EnterState(StateManager stateManager)
    {
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent += HandleEvent;
        MenuPairBase.SpawnElements(stateManager.tabletCanvas.transform, stateManager.monitorCanvas.transform);
        MenuPairBase.SetMenuPairActive(true);
        MenuPairBase.EnableFirstChild();
    }

    public override void HandleEvent(string eventName)
    {
        var trans = MenuPairBase.SpawnedMonitor.transform;
        var childC = trans.childCount;
        var found = false;
        for (int i = 0; i < childC; i++)
        {
            trans.GetChild(i).gameObject.SetActive(false);
            if (trans.GetChild(i).name.Equals(eventName))
            {
                trans.GetChild(i).gameObject.SetActive(true);
                found = true;
            }
        }
        if(found) return;
        if(eventName.Equals("Tillbaka")) StateManager.CurrentStateManager.ChangeMenuState(StateManager.CurrentStateManager.mainMenuState);
        
    }

    public override void ExitState(StateManager stateManager)
    {
        EventHandler.EventHandlerSingleton.OnButtonPressedEvent -= HandleEvent;
        MenuPairBase.SetMenuPairActive(false);
        
    }

    public override string GetNameMenuPair()
    {
        return MenuPairBase.GetName();
    }
}
