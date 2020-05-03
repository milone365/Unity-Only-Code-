using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    float damage = 1;
    public Rigidbody rb;

    private void Start()
    {
        if(rb==null)
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticStrings.player)
        {
           PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
             playerHealth.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
