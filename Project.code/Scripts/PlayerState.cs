using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerState : MonoBehaviour
{
    FPSPlayer player;
    public int hp = 10;
    int hpMax = 10;

    public Image hpbar;
    public Image wpbar;

    Image wpIcon;

    public int monsterNum;

    public int bossNum;
    void Start()
    {
        hpMax = hp;
        monsterNum = GameObject.FindObjectsOfType<Monster>().Length;
        player = GameObject.FindObjectOfType<FPSPlayer>();

        bossNum = GameObject.FindObjectsOfType<Boss>().Length;

        this.wpIcon = this.wpbar.transform.parent.Find("HealthIcon").GetComponent<Image>();
    }

    public void Health()
    {
        this.hp = this.hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if( hp < 0 ) 
        {
            hp = 0;
        }
        this.hpbar.fillAmount = this.hp/(float)this.hpMax;
        this.wpbar.fillAmount = player.cGun.ammu/(float)player.cGun.ammuMax;

        if( this.wpIcon.sprite != player.cGun.icon )
        {
            this.wpIcon.sprite = player.cGun.icon;
        }
    }
}
