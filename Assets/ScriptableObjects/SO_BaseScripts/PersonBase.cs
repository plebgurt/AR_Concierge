using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "New Person", fileName = "New Person")]
public class PersonBase : ScriptableObject
{
    public Image profileImage;
    public string userName;
    public int userid;
    public bool textOn;
    
    

}
