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
    internal LunchState lunchState = new LunchState();
    internal CheckedInState checkedInState = new CheckedInState();
    internal AktivitetState aktivitetState = new AktivitetState();

    internal List<BaseState> allBaseStates = new List<BaseState>();
    
    [SerializeField] internal Canvas tabletCanvas;
    [SerializeField] internal Canvas monitorCanvas;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        
        
    }

    internal void SetupSM()
    {
        CurrentStateManager ??= this;
        
        mainMenuState.MenuPairBase = Instantiate(menuPairBases[0]);
        qrState.MenuPairBase = Instantiate(menuPairBases[1]);
        schemaState.MenuPairBase = Instantiate(menuPairBases[2]);
        lunchState.MenuPairBase = Instantiate(menuPairBases[3]);
        checkedInState.MenuPairBase = Instantiate(menuPairBases[4]);
        aktivitetState.MenuPairBase = Instantiate(menuPairBases[5]);
        
        
        allBaseStates.Add(mainMenuState);
        allBaseStates.Add(qrState);
        allBaseStates.Add(schemaState);
        allBaseStates.Add(lunchState);
        allBaseStates.Add(checkedInState);
        allBaseStates.Add(aktivitetState);

        tabletCanvas.targetDisplay = 0;
        monitorCanvas.targetDisplay = 1;
        
        tabletCanvas.worldCamera = ProgramController.instance.main;
        monitorCanvas.worldCamera = ProgramController.instance.monitor;
        
        currentState = ProgramController.instance.DebugUnity ? mainMenuState : qrState;
        currentState.EnterState(CurrentStateManager);
    }


    internal void ChangeMenuState(BaseState newBaseState)
    {
        currentState.ExitState(CurrentStateManager);
        currentState = newBaseState;
        newBaseState.EnterState(CurrentStateManager);
        
        
    }

    internal void HandleEventFromMenu(string eventName)
    {
        foreach (var state in allBaseStates)
        {
            if (state.GetNameMenuPair().Equals(eventName))
            {
                ChangeMenuState(state);
                return;
            }
        }

        if (eventName.Equals("Quit"))
        {
            ChangeMenuState(qrState);
        }
    }
}