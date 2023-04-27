using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "New Person", fileName = "New Person")]
public class PersonBase : ScriptableObject
{
    public string profileImagePath;
    public string userName;
    public int userid;

    public KnappInfo knappInfo;
    public enum KnappInfo
    {
        Text,
        Bild,
        TextOchBild
    }

}
