using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Task = System.Threading.Tasks.Task;

[RequireComponent(typeof(StateManager))]
public class ProgramController : MonoBehaviour
{
    public static ProgramController instance;
    public StoreUsersBase usersBase;
    private bool waitSecondMonitor;
    public Camera main;
    public Camera monitor;
    [SerializeField] internal PersonBase currentUser;
    public StateManager stateManager;
    
    
    [Header("Error text")] 
    public TextMeshProUGUI monitorScreenText;
    public TextMeshProUGUI tabletScreenText;

    [Header("Debug")] 
    public bool DebugUnity;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        instance ??= this;
        if (DebugUnity) return;
        Display.onDisplaysUpdated += DisplayOnDisplaysUpdated;
        if (Display.displays.Length <= 1 && !waitSecondMonitor) StartCoroutine(AwaitSecondMonitor());
        main.targetDisplay = 0;
        monitor.targetDisplay = 1;
        
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
        var userID = int.Parse(scannedUser);
        foreach (var user in usersBase.registeredUsers)
        {
            if (!user.userid.Equals(userID)) continue;
            currentUser = user;
            stateManager.ChangeMenuState(stateManager.mainMenuState);
            return true;
        }
        DisplayError("Error! User not found with ID: " + userID);
        return false;
    }
}