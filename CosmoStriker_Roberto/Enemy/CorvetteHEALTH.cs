using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvetteHEALTH : MonoBehaviour, ITakeDamage { 

    public bool corvette1;
    [SerializeField]
    CorvetteLaser laser;
    [SerializeField]
    GameObject explosion, Corvette6;
   [SerializeField]
    int health = 1200;
    public int currentHealth;
    bool death;
    private void Start()
    {
        currentHealth = health;
    }
    public void takeDamage(int damage)
    {
        
        currentHealth -= damage;
        if (currentHealth == 750)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            //レーサー攻撃を使う
            laserShot();

        }
        if (currentHealth == 350)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            laserShot();


        }
        if (currentHealth <= 0)
        {
            if (!death)
            {
                death = true;
                if (corvette1)
                {

                    Corvette6.SetActive(true);
                }

                GetComponent<Animator>().SetTrigger("end");

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            ushort _dmg = other.gameObject.GetComponent<_tmpBullet>().Damage;

            takeDamage(_dmg);
        }
        if (other.tag == "Player")
        {
            takeDamage(10);
            other.GetComponent<HealthManager>().Takedamage(10);
        }
    }

    public void laserShot()
    {
        laser.LaserActive();
    }
}
