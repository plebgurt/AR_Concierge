using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowUsernameMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Image profileImage;

    private void OnEnable()
    {
        textMeshProUGUI.text = $"Hej {ProgramController.instance.currentUser.userName}!";
        profileImage.sprite = Resources.Load<Sprite>(ProgramController.instance.currentUser.profileImagePath);
    }
}
