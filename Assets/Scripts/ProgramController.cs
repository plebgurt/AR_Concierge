using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(StateManager))]
public class ProgramController : MonoBehaviour
{
    public static ProgramController instance;


    public TextMeshProUGUI tabletScreenText;
    public List<PersonBase> registeredUsers = new List<PersonBase>();
    private bool waitSecondMonitor;
    public Camera main;
    public Camera monitor;

    public GameObject monitorCanvas;
    public GameObject tabletCanvas;

    internal PersonBase currentUser;


    [Header("Tablet UI")] public GameObject QRMenu;


    public GameObject mainMenu;
    [Header("Monitor UI")] public TextMeshProUGUI monitorScreenText;

    [Header("SO Menu items")] [SerializeField]
    private List<ScriptableObject> MenuItems = new List<ScriptableObject>();

    [FormerlySerializedAs("_stateManager")] [SerializeField]
    private StateManager stateManager;

    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        instance ??= this;
        Display.onDisplaysUpdated += DisplayOnDisplaysUpdated;
        if (Display.displays.Length <= 1 && !waitSecondMonitor) StartCoroutine(AwaitSecondMonitor());
        main.targetDisplay = 0;
        monitor.targetDisplay = 1;
        stateManager = StateManager.CurrentStateManager;
        stateManager.StartStateManager();
    }

    IEnumerator AwaitSecondMonitor()
    {
        waitSecondMonitor = true;
        DisplayError("Error: No second source found");
        while (Display.displays.Length <= 1)
        {
            Debug.LogWarning("Amount of screens: " + Display.displays.Length);
            yield return null;
        }
        waitSecondMonitor = false;
        tabletScreenText.enabled = false;
    }

    private void DisplayOnDisplaysUpdated()
    {
        foreach (var t in Display.displays)
        {
            if (t.active) continue;
            t.Activate();
        }
        if (Display.displays.Length <= 1 && !waitSecondMonitor) StartCoroutine(AwaitSecondMonitor());
    }
    
    private void DisplayError(string error)
    {
        if (Display.displays.Length <= 1)
        {
            tabletScreenText.enabled = true;
            tabletScreenText.text = error;
        }
        else
        {
            monitorScreenText.text = "Error: User not found";
        }
        
    }

    public void ShutDown()
    {
        Application.Quit();
    }

    public void LogOut()
    {
        stateManager.ChangeMenuState(stateManager.qrState);
    }

    public bool AttemptLogin(string scannedUser)
    {
        foreach (var user in registeredUsers)
        {
            if (user.userid.Equals(int.Parse(scannedUser))) continue;
            currentUser = user;
            stateManager.ChangeMenuState(stateManager.mainMenuState);
            return true;
        }
        DisplayError("Error: User not found");
        return false;
    }
}