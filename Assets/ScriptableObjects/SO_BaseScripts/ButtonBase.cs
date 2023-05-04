using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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
    private GameObject textImageChild;
    private GameObject SpawnedButton;


    public void GenerateButton(Transform parent)
    {
        SpawnedButton = Instantiate(buttonGO, parent, false);
        button = SpawnedButton.GetComponent<Button>();
        textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = button.transform.Find("ButtonImage").GetComponent<Image>();
        textImageChild = button.transform.Find("ButtonTextImage").gameObject;
        
        textMeshProUGUI.enabled = false;
        buttonImage.enabled = false;
        textImageChild.SetActive(false);
        var someOtherSprite = Resources.Load<Sprite>(imagePath);
        
        switch (ProgramController.instance.currentUser.knappInfo)
        {
            case PersonBase.KnappInfo.Bild:
                buttonImage.enabled = true;
                buttonImage.sprite = someOtherSprite;
                break;
            
            case PersonBase.KnappInfo.Text:
                textMeshProUGUI.enabled = true;
                textMeshProUGUI.text = buttonName;
                break;
            
            case PersonBase.KnappInfo.TextOchBild:
                textImageChild.SetActive(true);
                textImageChild.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
                textImageChild.GetComponentInChildren<Image>().sprite = someOtherSprite;
                break;
        }
        
        Debug.Log($"Button name: {button.name}, button pos: {SpawnedButton.transform.TransformPoint(SpawnedButton.transform.position)}, ");
        button.onClick.AddListener(SendPressedNameEvent); 
        
    }
    
    public void SendPressedNameEvent()
    {
        Debug.LogWarning($"Sending: {buttonName} event.");
        EventHandler.EventHandlerSingleton.OnButtonPressed(buttonName);
    }
    
}
