using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMissile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] GameObject explosion;
    [SerializeField] 
    float movez;
    Transform target=null;

    void Update()
    {
        if (target != null)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            this.transform.position = this.transform.position.normalized;
        }
        else
        {
            transform.Translate(0, 0, movez);
        }
        
    }
    public void giveTarget(Transform enemy)
    {
        target = enemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == target)
        {
            if (other.tag == "Enemy"|| other.tag == "Corvette" || other.tag == "Boss")
            {
                ITakeDamage takedmg = other.GetComponent<ITakeDamage>();
                if (takedmg != null)
                {
                    takedmg.takeDamage(damage);
                }
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            
        }
        
    }
}
