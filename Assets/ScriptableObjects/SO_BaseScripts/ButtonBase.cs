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
    private Image buttonImage;
    private GameObject SpawnedButton;
    
    public void GenerateButton(Transform parent)
    {
        SpawnedButton = Instantiate(buttonGO, parent);
        button = SpawnedButton.GetComponent<Button>();
        textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = button.transform.Find("ButtonImage").GetComponent<Image>();
        if (ProgramController.instance.currentUser.textOn)
        {
            buttonImage.enabled = false;
            textMeshProUGUI.text = buttonName;
        }
        else
        {
            textMeshProUGUI.enabled = false;
            
            var someOtherSprite = Resources.Load<Sprite>(imagePath);
            Debug.Log(someOtherSprite == null);
            buttonImage.sprite = someOtherSprite;

        }
        
        button.onClick.AddListener(SendPressedNameEvent); 
    }
    
    public void SendPressedNameEvent()
    {
        EventHandler.EventHandlerSingleton.OnButtonPressed(buttonName);
    }
    
}
