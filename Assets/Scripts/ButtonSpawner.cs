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
        var tempS = "";
        for (int i = 0; i < buttonSpawnPoints.Count; i++)
        {
            if(buttonSpawnPoints[i].transform.childCount > 0) continue;
            buttonSO[i].GenerateButton(buttonSpawnPoints[i]);
            tempS += $"{buttonSO[i].buttonName}, pos {buttonSpawnPoints[i].position} \n";
        }
        Debug.Log(tempS);
    }
}
