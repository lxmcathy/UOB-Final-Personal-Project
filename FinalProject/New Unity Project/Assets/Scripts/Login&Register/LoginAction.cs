using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoginAction : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginBtn;
    public Button register;
    public Text info;

    LoginService service = new LoginService();
    // Use this for initialization
    void Start()
    {
        register.onClick.AddListener(Register);
        loginBtn.onClick.AddListener(Login);
    }

    public void Login()
    {
        //Debug.Log("通知service");
        //通知service层 去处理
        UserInfo user = service.Service(usernameInput.text, passwordInput.text);
        print(user);
        //Debug.Log("通知完毕");
        if (user == null)
        {
            info.text = "Login Failed";
        }
        else
        {
            info.text = "Login Successfully!";
            SceneManager.LoadScene("home");
        }
    }
    public void Register()
    {
        SceneManager.LoadScene("Register");
    }
}
