using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 1;
    public float removeTime = 3;
    public float speed = 100;
    Rigidbody rig;
    void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        rig.velocity = Vector3.Normalize(GameObject.FindObjectOfType<FPSPlayer>().transform.position - this.transform.position) *speed;
        Destroy(this.gameObject,removeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.CompareTag("Player") )
        {
            GameObject.FindObjectOfType<PlayerState>().hp -= damage;
        }
    }
}
