using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitEff;
    public float removeTime = 3;
    public float speed = 100;
    Rigidbody rig;
    void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        rig.velocity = this.transform.forward * speed;
        Destroy(this.gameObject,removeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        GameObject eff = Instantiate(hitEff,col.GetContact(0).point,this.transform.rotation);
        eff.transform.LookAt(col.GetContact(0).point -col.GetContact(0).normal);
        Destroy(this.gameObject);

        if( col.gameObject.CompareTag("Monster") )
        {
            Monster monster = col.transform.parent.gameObject.GetComponent<Monster>();
            if( monster != null )
            {
                monster.damage(damage);
            }
            else
            {
                col.transform.parent.gameObject.GetComponent<Boss>().damage(damage);
            }
        }
    }
}
