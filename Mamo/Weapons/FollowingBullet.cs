using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    public int damage = 1;
    public GameObject particle;
    public float moveSpeed = 0.1f;
  
  
    // Update is called once per frame
    void Update() 
    {
        
        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position , moveSpeed);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager.instace.TakeDamage(damage);

        }
        Instantiate(particle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
