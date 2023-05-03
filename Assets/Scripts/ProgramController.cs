using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;
using Task = System.Threading.Tasks.Task;

[RequireComponent(typeof(StateManager))]
public class ProgramController : MonoBehaviour
{
    public static ProgramController instance;
    public StoreUsersBase usersBase;
    private StoreUsersBase spawnedUsers;
    private bool waitSecondMonitor;
    public Camera main;
    public Camera monitor;
    [SerializeField] internal PersonBase currentUser;
    public StateManager stateManager;
    [SerializeField] private AudioClip loggedInSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Text loggedInText;
    
    
    [Header("Error text")] 
    public TextMeshProUGUI monitorScreenText;
    public TextMeshProUGUI tabletScreenText;

    [Header("Debug")] 
    public bool DebugUnity;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        spawnedUsers = Instantiate(usersBase);
        instance ??= this;
        if (DebugUnity)
        {
            var random = new Random();
            var user = random.Next(4);
            currentUser = spawnedUsers.registeredUsers[user];
            return;
        }
     
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
            monitorScreenText.text = "Error: Monitor not found";
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
        foreach (var user in spawnedUsers.registeredUsers)
        {
            if (!user.userid.Equals(userID)) continue;
            currentUser = user;
            stateManager.ChangeMenuState(stateManager.mainMenuState);
            audioSource.PlayOneShot(loggedInSound);
            return true;
        }
        DisplayError("Error! User not found with ID: " + userID);
        return false;
    }
    
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            // Check for 3D objects
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Touched 3D Object: " + hit.collider.gameObject.name);
                return;
            }

            // Check for UI elements
            GraphicRaycaster graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            if (graphicRaycaster == null)
            {
                Debug.LogError("No GraphicRaycaster component found in the parent GameObject.");
                return;
            }

            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.GetTouch(0).position;

            // Raycast against all graphics in the canvas
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, results);

            // Find the first UI element that intersects with the ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject != null)
                {
                    Debug.Log("Touched UI Element: " + result.gameObject.name);
                    return;
                }
            }

            Debug.Log("Touch detected, but no object or UI element found.");
        }
    }

}