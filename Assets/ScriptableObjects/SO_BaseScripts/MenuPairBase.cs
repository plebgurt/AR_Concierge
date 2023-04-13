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

    public void SetActive(bool state)
    {
        tabletObject.SetActive(state);
        monitorObject.SetActive(state);
    }

    public string GetName()
    {
        return menuName;
    }
    
}
