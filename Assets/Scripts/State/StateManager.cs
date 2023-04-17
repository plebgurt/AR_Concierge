using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] internal List<MenuPairBase> menuPairBases = new List<MenuPairBase>();
    
    internal static StateManager CurrentStateManager;
    
    internal BaseState currentState;
    internal MainMenuState mainMenuState = new MainMenuState();
    internal QRState qrState = new QRState();
    internal SchemaState schemaState = new SchemaState();
    
    internal List<BaseState> enterOrder = new List<BaseState>();
    internal List<BaseState> allBaseStates = new List<BaseState>();
    
    [SerializeField] internal Canvas tabletCanvas;
    [SerializeField] internal Canvas monitorCanvas;
    
    // Start is called before the first frame update
    void Awake()
    {
        CurrentStateManager ??= this;
        
        mainMenuState.MenuPairBase = Instantiate(menuPairBases[0]);
        qrState.MenuPairBase = Instantiate(menuPairBases[1]);
        schemaState.MenuPairBase = Instantiate(menuPairBases[2]);
        
        allBaseStates.Add(mainMenuState);
        allBaseStates.Add(qrState);
        allBaseStates.Add(schemaState);
        
        currentState = mainMenuState;
        currentState.EnterState(CurrentStateManager);
    }


    internal void ChangeMenuState(BaseState newBaseState)
    {
        Debug.Log("Changed event");
        currentState.ExitState(CurrentStateManager);
        currentState = newBaseState;
        newBaseState.EnterState(CurrentStateManager);
        
    }

    internal void HandleEventFromMenu(string eventName)
    {
        Debug.Log("Received: " + eventName);
        
        foreach (var state in allBaseStates)
        {
            Debug.Log($"Checking {eventName} with {state.GetNameMenuPair()}");

            if (state.GetNameMenuPair().Equals(eventName))
            {
                Debug.Log("Changed event");   
                ChangeMenuState(state);
                return;
            }
        }
    }
}