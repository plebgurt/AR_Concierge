using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "New Menu", fileName = "New Menu")]
public class MenuPairBase : ScriptableObject
{
    public string menuName;
    public GameObject tabletObject;
    public GameObject monitorObject;

    internal GameObject SpawnedTablet;
    internal GameObject SpawnedMonitor;
    
    public void SetMenuPairActive(bool state)
    {
        SpawnedTablet = Instantiate(tabletObject);
        SpawnedMonitor = Instantiate(monitorObject);
        
        SpawnedTablet.SetActive(state);
        SpawnedMonitor.SetActive(state);
    }

    public string GetName()
    {
        return menuName;
    }
    
}
