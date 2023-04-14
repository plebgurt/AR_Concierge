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

    public void SpawnElements(Transform tabletCanvas, Transform monitorCanvas)
    {
        SpawnedTablet = Instantiate(tabletObject, tabletCanvas);
        SpawnedMonitor = Instantiate(monitorObject, monitorCanvas);
    }
    
    public void SetMenuPairActive(bool state)
    {
        SpawnedTablet.SetActive(state);
        SpawnedMonitor.SetActive(state);
    }

    public string GetName()
    {
        return menuName;
    }
    
}
