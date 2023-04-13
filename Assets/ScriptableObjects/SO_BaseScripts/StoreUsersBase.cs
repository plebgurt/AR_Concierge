using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New User list", fileName = "New User list")]
public class StoreUsersBase : ScriptableObject
{
    public List<PersonBase> registeredUsers;
}
