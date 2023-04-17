using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowUsernameMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void OnEnable()
    {
        textMeshProUGUI.text = $"Hej! {ProgramController.instance.currentUser.userName}";
    }
}
