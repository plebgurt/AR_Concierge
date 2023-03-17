using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "New Button", fileName = "New Button")]
public class ButtonBase : ScriptableObject
{
    public string buttonName;
    public string imagePath;
}
