using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballController : MonoBehaviour
{
    public int damage = 1;
    public GameObject particle;
    public float force=10;
    private Transform target;
    public Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }
    // Update is called once per frame
    void Update()
    {


       
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().hurtEnemy(damage);
            Destroy(gameObject);

        }
        Instantiate(particle, transform.position, transform.rotation);
        
    }
   
}
