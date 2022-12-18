using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Psw { get; set; }

    public UserInfo()
    {

    }

    public UserInfo(string username, string password)
    {
        this.Name = username;
        this.Psw = password;
    }
   

}

