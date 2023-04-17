using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "New Button", fileName = "New Button")]
public class ButtonBase : ScriptableObject
{
    public string buttonName;
    public string imagePath;
    public GameObject buttonGO;
    private Button button;
    private TextMeshProUGUI textMeshProUGUI;
    private GameObject SpawnedButton;
    
    public GameObject GenerateButton(Transform parent)
    {
        SpawnedButton = Instantiate(buttonGO, parent);
        button = SpawnedButton.GetComponent<Button>();
        ColorBlock cb = button.colors;
        cb.normalColor = Color.magenta;
        button.colors = cb;
        textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.text = buttonName;
        button.onClick.AddListener(SendPressedNameEvent);
        return SpawnedButton;
    }
    
    public void SendPressedNameEvent()
    {
        Debug.Log("Sending: " + buttonName);
        EventHandler.EventHandlerSingleton.OnButtonPressed(buttonName);
    }
    
}
