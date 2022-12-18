using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterAction : MonoBehaviour
{
    public InputField nameInput;
    public InputField passwordInput;
    public Button register;
    public Button login;
    public Text info;

    RegisterService service = new RegisterService();
    // Use this for initialization
    void Start()
    {
        login.onClick.AddListener(Login);
        register.onClick.AddListener(Register);
    }


    public void Login()
    {
        SceneManager.LoadScene("Login");
    }
    public void Register()
    {
        info.text = service.Service(nameInput.text, passwordInput.text);
    }

}
