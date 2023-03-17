using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class ProgramController : MonoBehaviour
{
    public static ProgramController instance;
    public TextMeshProUGUI menuText;
    public TextMeshProUGUI tabletScreen;
    public List<string> users = new List<string>() { "Test1", "Test2" };
    public GameObject QRMenu;
    public GameObject mainMenu;
    private bool waitSecondMonitor;

    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        instance = this;
        mainMenu.SetActive(false);
        QRMenu.SetActive(false);
        Display.onDisplaysUpdated += DisplayOnDisplaysUpdated;
        if(Display.displays.Length <= 1 && !waitSecondMonitor) StartCoroutine(AwaitSecondMonitor());
        QRMenu.SetActive(true);
    }

    IEnumerator AwaitSecondMonitor()
    {
        waitSecondMonitor = true;
        DisplayErrorNoSecondMonitor();
        
        while (Display.displays.Length <= 1)
        {
            Debug.LogWarning("Amount of screens: " + Display.displays.Length);
            yield return null;
        }
        waitSecondMonitor = false;
        tabletScreen.enabled = false;
    }

    private void DisplayOnDisplaysUpdated()
    {
        foreach (var t in Display.displays)
        {
            if(t.active) continue;
            t.Activate();
        }
        
        
        if(Display.displays.Length <= 1 && !waitSecondMonitor) StartCoroutine(AwaitSecondMonitor());
    }

    private void DisplayErrorNoSecondMonitor()
    {
        tabletScreen.enabled = true;
        tabletScreen.text = "Error: No second screen connected";
    }

    private void DisplayErrorNoUser()
    {
        menuText.text = "Error: User not found";
    }

    public void ShutDown()
    {
        Application.Quit();
    }

    public void LogOut()
    {
        SwitchFromQR(false);
    }

    public void SwitchFromQR(bool state)
    {
        QRMenu.SetActive(!state);
        mainMenu.SetActive(state);
    }

    public bool AttemptLogin(string scannedUser)
    {
        if (users.Contains(scannedUser)) return true;

        DisplayErrorNoUser();
        return false;
    }
}