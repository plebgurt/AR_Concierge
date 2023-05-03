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
    public List<MenuPairBase> children = new List<MenuPairBase>();

    internal GameObject SpawnedTablet;
    internal GameObject SpawnedMonitor;

    public void SpawnElements(Transform tabletCanvas, Transform monitorCanvas)
    {
        
        SpawnedTablet ??= Instantiate(tabletObject, tabletCanvas,false);
        SpawnedMonitor ??= Instantiate(monitorObject, monitorCanvas, false);
        
    }
    
    public void SetMenuPairActive(bool state)
    {
        SpawnedTablet.SetActive(state);
        SpawnedMonitor.SetActive(state);
        
    }

    public void EnableFirstChild()
    {
        
            if(SpawnedMonitor.transform.childCount <= 0) return;
            SpawnedMonitor.transform.GetChild(0).gameObject.SetActive(true);
        
    }

    public string GetName()
    {
        return menuName;
    }
    
}
