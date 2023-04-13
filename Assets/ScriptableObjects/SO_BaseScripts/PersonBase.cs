using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "New Person", fileName = "New Person")]
public class PersonBase : ScriptableObject
{
    public string userName;
    public int userid;
    public bool textOn;
    
}
