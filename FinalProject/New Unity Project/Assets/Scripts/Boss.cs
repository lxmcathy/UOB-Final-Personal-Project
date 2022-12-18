using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 2;
    public Animator an;

    public int hp = 3;

    public bool hitPlayer;
    void Start()
    {
        
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
