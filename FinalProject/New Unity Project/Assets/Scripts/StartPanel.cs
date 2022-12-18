using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartPanel : MonoBehaviour
{
    Text lab2;

    public float updateTime = 0.5f;
    float m_updateTime;

    int index = 0;
    string text;
    void Start()
    {
        lab2 = this.GetComponent<Text>();
        text = lab2.text;
        lab2.text = "";
        m_updateTime = updateTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_updateTime -= Time.deltaTime;
        if( m_updateTime <= 0)
        {
            if( index >= text.Length )
            {
                 SceneManager.LoadScene("Login");
            }
            m_updateTime = updateTime;
            lab2.text = text.Substring(0,index+1);
            index++;
            if( index >= text.Length )
            {
                m_updateTime = 1;
            }
        }
    }
}
