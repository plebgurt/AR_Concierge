using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonSpawnPoints;
    [SerializeField] private List<ScriptableObject> buttonSO;

    private void OnEnable()
    {
        for (int i = 0; i < buttonSpawnPoints.Count; i++)
        {
            Instantiate(buttonSO[i]);
        }
    }
}
