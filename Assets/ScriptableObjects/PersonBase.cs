using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "New Person", fileName = "New Person")]
public class PersonBase : ScriptableObject
{
    public string user;
    public int ID;
    public bool textOn;

    public PersonBase(string user, int ID, bool textOn)
    {
        this.user = user;
        this.textOn = textOn;
        this.ID = ID;
    }
}
