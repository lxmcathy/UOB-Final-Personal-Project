using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int skin;
    public float speed = 2;
    public Animator an;

    public int hp = 3;

    public bool hitPlayer;
    void Start()
    {
        for (int i = 0; i < this.transform.childCount - 1; i++)
        {
            if( skin == i )
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
                an = this.transform.GetChild(i).gameObject.GetComponent<Animator>();
            }
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }

    public void damage(int damage)
    {
        this.hitPlayer = true;
        this.hp -= damage;
        if( this.hp <=0 )
        {
            this.hp = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
