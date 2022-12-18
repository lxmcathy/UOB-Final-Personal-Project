using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.CompareTag("Player") )
        {
            GameObject.FindObjectOfType<PlayerState>().Health();
            GameObject.Destroy(this.gameObject);

        }
    }
}
