using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterAI : MonoBehaviour
{
    public GameObject path;

    public int attackRange = 1;
    int pathIndex;
    public Monster monster;
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

        monster = this.GetComponent<Monster>();
        aiTime = time;

        player = GameObject.FindObjectOfType<FPSPlayer>();

        nav.speed = monster.speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if( monster.hp <= 0 )
        {
           
            if(this.state != -1)
            {
                this.state = -1;
                monster.an.Play("Die");
                nav.destination = this.transform.position;
                this.GetComponent<AudioSource>().Play();
            }
            else if(monster.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9)
            {
                GameObject.Destroy(this.gameObject);
                GameObject.FindObjectOfType<PlayerState>().monsterNum -- ;
            }
            
            return;
        }

        if( monster.hitPlayer )
        {
            if( state < 2 )
            {
                lockPlayer = true;
                state = 2;
                nav.speed = monster.speed * 3;
            }
           
        }

        aiTime -= Time.deltaTime;
        if( state == 0 )
        {
            monster.an.CrossFade("Idle",0.75f);
            if( aiTime <= 0 )
            {
                aiTime = time;
                state = 1;
            }
        }

        if( state == 1 )
        {
            if(monster.an.GetCurrentAnimatorStateInfo(0).IsName("walk") == false )
            {
                monster.an.Play("walk");
            }
            if( targetPoint == null || nav.remainingDistance <= 0.01f )
            {
                targetPoint = path.transform.GetChild(pathIndex).gameObject;
                nav.destination = targetPoint.transform.position;
                pathIndex = Random.Range(0,path.transform.childCount);
                if( pathIndex >= path.transform.childCount )
                {
                    pathIndex = 0;
                    state = 0;
                }
            }
        }

        if( state == 2 )
        {
            if(monster.an.GetCurrentAnimatorStateInfo(0).IsName("walk") == false )
            {
                monster.an.Play("walk");
            }
            nav.destination = player.transform.position;
            if( Vector3.Distance(player.transform.position,this.transform.position) <= attackRange)
            {
                state = 3;
                monster.an.CrossFade("attack",0.75f);
                GameObject.FindObjectOfType<PlayerState>().hp -= 1;
            }
        }

        if( state == 3 )
        {
            if( monster.an.GetCurrentAnimatorStateInfo(0).IsName("Idle") && monster.an.IsInTransition(0)  == false )
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
