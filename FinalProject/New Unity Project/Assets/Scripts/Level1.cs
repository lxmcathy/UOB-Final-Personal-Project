using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level1 : MonoBehaviour
{
    public GameObject win;
    public GameObject lose;

    PlayerState player;

    public float star3 = 60;

    public float star2 = 90;


    Text timeLab;

    float time = 0;

    void Start()
    {
        
        win.SetActive(false);
        lose.SetActive(false);

        player = GameObject.FindObjectOfType<PlayerState>();
        int level = PlayerPrefs.GetInt("level",1);
        if( level < 1 )
        {
            PlayerPrefs.SetInt("level",1);
            PlayerPrefs.Save();
        }

        timeLab = this.transform.Find("timeLab").GetComponent<Text>();
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextGame()
    {
        SceneManager.LoadScene("level2");
    }

    // Update is called once per frame
    void Update()
    {
        time+= Time.deltaTime;
        timeLab.text = "Time："+(int)time;
        if( player.hp <= 0 )
        {
            GameObject.FindObjectOfType<FPSPlayer>().gameOver();
            lose.SetActive(true);
        }
        else if( player.monsterNum <=0  && win.activeSelf == false )
        {
            GameObject.FindObjectOfType<FPSPlayer>().gameOver();
            win.SetActive(true);
            GameObject xinxin = win.transform.Find("xinxin").gameObject;
            int num = 0;
            if( time <= star3 )
            {
                num = 2;
            }
            else if( time <= star2 )
            {
                num = 1;
            }

            for (int i = 0; i < 3; i++)
            {
                if( i <= num )
                {
                    xinxin.transform.GetChild(i).GetChild(i).gameObject.SetActive(false);
                }
                
            }

            int n = PlayerPrefs.GetInt("xinxin",1);
            n++;
            PlayerPrefs.SetInt("xinxin",n);
            PlayerPrefs.Save();
            
        }

    }
}
