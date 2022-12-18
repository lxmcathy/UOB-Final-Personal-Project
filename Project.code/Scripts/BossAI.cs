using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossAI : MonoBehaviour
{
    public GameObject ball;
    public GameObject fire;

    public float fireTime = 10;
    float m_fireTime = 0;
    float m_fireTime2 = 0;
    public GameObject path;

    public int attackRange = 1;
    int pathIndex;
    public Boss boss;
    public int state = 0;

    public float time = 3;

    float aiTime;

    NavMeshAgent nav;

    GameObject targetPoint;

    FPSPlayer player;

    bool lockPlayer;


    void Start()
    {
        path.gameObject.SetActive(false);
        
        nav = this.GetComponent<NavMeshAgent>();

        boss = this.GetComponent<Boss>();
        aiTime = time;

        player = GameObject.FindObjectOfType<FPSPlayer>();

        nav.speed = boss.speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        m_fireTime -= Time.deltaTime;
        if( boss.hp <= 0 )
        {
           
            if(this.state != -1)
            {
                this.state = -1;
                boss.an.Play("Die");
                nav.destination = this.transform.position;
                this.GetComponent<AudioSource>().Play();
            }
            else if(boss.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9)
            {
                GameObject.Destroy(this.gameObject);
                GameObject.FindObjectOfType<PlayerState>().bossNum -- ;
            }
            
            return;
        }

        if( boss.hitPlayer )
        {
            if( state < 2 )
            {
                lockPlayer = true;
                state = 2;
            }
           
        }

        aiTime -= Time.deltaTime;
        if( state == 0 )
        {
            boss.an.CrossFade("Idle",0.75f);
            if( aiTime <= 0 )
            {
                aiTime = time;
                state = 1;
            }
        }

        if( state == 1 )
        {
            if(boss.an.GetCurrentAnimatorStateInfo(0).IsName("walk") == false )
            {
                boss.an.Play("walk");
            }
            if( targetPoint == null || nav.remainingDistance <= 0.01f )
            {
                targetPoint = path.transform.GetChild(pathIndex).gameObject;
                nav.destination = targetPoint.transform.position;
                pathIndex ++;
                if( pathIndex >= path.transform.childCount )
                {
                    pathIndex = 0;
                    state = 0;
                }
            }
        }

        if( state == 2 )
        {
            if(boss.an.GetCurrentAnimatorStateInfo(0).IsName("walk") == false )
            {
                boss.an.Play("walk");
            }
            nav.destination = player.transform.position;
            if( Vector3.Distance(player.transform.position,this.transform.position) <= attackRange)
            {
                if( m_fireTime <= 0 )
                {
                    m_fireTime2 = 4;
                    state = 4;
                    boss.an.CrossFade("FireBreathAttack-Start",0.75f);
                    nav.destination = this.transform.position;
                }
                else
                {
                    state = 3;
                    boss.an.CrossFade("attack",0.75f);
                    Instantiate(ball,fire.transform.position,this.transform.rotation);
                }
            }
        }

        if( state == 3 )
        {
            if( boss.an.GetCurrentAnimatorStateInfo(0).IsName("Idle") && boss.an.IsInTransition(0)  == false )
            {
                if( lockPlayer )
                {
                    state = 2;
                }
                else
                {
                    state = 1;
                }
            }
        }

        if( state == 4 )
        {
            if( boss.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f && boss.an.GetCurrentAnimatorStateInfo(0).IsName("FireBreathAttack-Start") && fire.activeSelf == false )
            {
                m_fireTime = fireTime;
                fire.SetActive(true);
            }
            m_fireTime2 -= Time.deltaTime;
            if( m_fireTime2 <= 0 )
            {
                state = 2;
                fire.SetActive(false);
            }
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if( state == 1 && collider.CompareTag("Player") )
        {
            state = 2;
            lockPlayer = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if( collider.CompareTag("Player") )
        {
            if( state == 2 )
            {
                state = 1;
            }
            lockPlayer = false;
        }
    }
}
