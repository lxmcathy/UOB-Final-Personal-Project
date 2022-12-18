using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using MySql.Data;


public class LoginService
{
    public UserInfo Service(string username, string password)
    {
        UserInfo user = null;

        string sql = "select * from userinfo where username='{0}'and password='{1}';";//userInfo
        sql = string.Format(sql, username, password);//还是要赋值
        try
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand comd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                user = new UserInfo();
                user.id = reader.GetInt32(0);
                user.Name = reader.GetString(1);
                user.Psw = reader.GetString(2);
                //user.Address = reader.GetString(3);
                //user.Email = reader.GetString(4);
            }
            reader.Close();
            return user;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public MySqlConnection GetConnection()
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;UserId=root;Password=root;Database=userinfo");
        conn.Open();
        return conn;
    }


}
