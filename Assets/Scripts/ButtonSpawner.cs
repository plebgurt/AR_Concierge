using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> buttonSpawnPoints;
    [SerializeField] private List<ButtonBase> buttonSO;

    private void OnEnable()
    {
        for (int i = 0; i < buttonSpawnPoints.Count; i++)
        {
            var temp = buttonSO[i].GenerateButton();
            temp.transform.SetParent(buttonSpawnPoints[i]);
            Debug.LogWarning("Spawned: " + buttonSO[i].buttonName + " is active: " + temp.activeInHierarchy);
            
        }
    }
}
