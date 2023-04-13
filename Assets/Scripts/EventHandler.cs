using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler EventHandlerSingleton;
    
    // Start is called before the first frame update
    void Awake()
    {
        EventHandlerSingleton ??= this;
    }

    public event Action<string> OnButtonPressedEvent;

    public void OnButtonPressed(string buttonName)
    {
        OnButtonPressedEvent?.Invoke(buttonName);
    }
}
