using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int userLevel = 1;
    public int exp = 0;
    public Dictionary<string,UserData> data = new Dictionary<string,UserData>();
}
