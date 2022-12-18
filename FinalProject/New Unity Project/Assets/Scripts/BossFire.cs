using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    public int damage = 3;
    
    void Start()
    {
        
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
