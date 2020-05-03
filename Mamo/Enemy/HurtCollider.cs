using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCollider : MonoBehaviour
{
    public bool isiron=false,haveARandomicFactor;
    
    public int randomicfactor=0;
  public GameObject blood;
  public int damage = 1;
    public int force = 0;

    private void Start()
    {
        force = GameManager.instace.force;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Enemy")
        {
            if (force != GameManager.instace.force)
            {
                force = GameManager.instace.force;
            }
            if (haveARandomicFactor)
            {
                int newRand = Random.Range(1, randomicfactor);
                randomicfactor = newRand;
            }
            
           if (isiron)
            {
                Instantiate(blood, transform.position, transform.rotation);
            }
            other.GetComponent<IEnemyHealth>().hurtEnemy(damage + randomicfactor+force);
        }
        if (other.tag == "robot")
        {
            other.GetComponent<RobotDamageButton>().getDamage();
        }
        if (other.tag == "destroy")
        {

            if (haveARandomicFactor)
            {
                int newRand = Random.Range(1, randomicfactor);
                randomicfactor = newRand;
            }
            
            if (isiron)
            {
                Instantiate(blood, transform.position, transform.rotation);
            }
            other.GetComponent<Endurance>().takedamage(damage + randomicfactor);
        }
  }
}
    

