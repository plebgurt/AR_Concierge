using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private List<MenuPairBase> menuPairBases = new List<MenuPairBase>();
    internal static StateManager CurrentStateManager;
    internal BaseState currentState;
    internal MainMenuState mainMenuState = new MainMenuState();
    internal QRState qrState = new QRState();

    [SerializeField] internal Canvas tabletCanvas;
    [SerializeField] internal Canvas monitorCanvas;
    
    // Start is called before the first frame update
    void Awake()
    {
        CurrentStateManager ??= this;
        mainMenuState.MenuPairBase = Instantiate(menuPairBases[0]);
        qrState.MenuPairBase = Instantiate(menuPairBases[1]);
        
        currentState = mainMenuState;
        currentState.EnterState(CurrentStateManager);
    }


    internal void ChangeMenuState(BaseState baseState)
    {
        currentState.ExitState(CurrentStateManager);
        currentState = baseState;
        baseState.EnterState(CurrentStateManager);
    }
}