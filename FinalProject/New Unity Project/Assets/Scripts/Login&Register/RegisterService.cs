using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;

public class RegisterService
{
    public string Service(string username, string password)
    {
        string sql = "insert into userinfo(username,password) values('{0}','{1}');";//userInfo
        sql = string.Format(sql, username, password);//还是要赋值
        try
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.ExecuteNonQuery();
            return "Registered Successfully";
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return "Register Failed";
        }
    }

    public MySqlConnection GetConnection()
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;UserId=root;Password=root;Database=userinfo");
        conn.Open();
        return conn;


    }
}