using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    public bool haveARandomicFactor;

    public int randomicfactor = 0;
    
    public int damage = 1;
   

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            getRandomDamage();
           other.GetComponent<EnemyHealthManager>().hurtEnemy(damage + randomicfactor );
        }
       
        if (other.tag == "destroy")
        {

            getRandomDamage();
            other.GetComponent<Endurance>().takedamage(damage + randomicfactor);
        }
       
        if (other.tag == "minion")
        {
            getRandomDamage();
            other.GetComponent<IMinion>().takeDamage(damage + randomicfactor);
        }
    }

    public void getRandomDamage()
    {
        if (haveARandomicFactor)
        {
            int newRand = Random.Range(1, randomicfactor);
            randomicfactor = newRand;
        }

    }
}
