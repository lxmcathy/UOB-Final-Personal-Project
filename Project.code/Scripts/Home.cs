using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Home : MonoBehaviour
{
    public Setting setting;
    
    void Start()
    {
        setting.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSetting()
    {
        setting.gameObject.SetActive(true);
    }

    public void startGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void continueGame()
    {
        int level = PlayerPrefs.GetInt("level",1);
        SceneManager.LoadScene("level"+level);
    }

    public void outGme()
    {
        Application.Quit();
    }
}
