using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DestroyerCannonAttack : MonoBehaviour,ITakeDamage
{
    [SerializeField] float Health=100;
    [SerializeField] int power = 10;
    [SerializeField] GameObject explosion;
    [SerializeField]  EnemyBullet bullet; 
   
    [SerializeField] Transform[] firepoints;
 
    public void fire()
    {
        for(int i = 0; i < firepoints.Length; i++)
        {
            EnemyBullet newBullet= Instantiate(bullet, firepoints[i].position, transform.rotation) as EnemyBullet;
            newBullet.damage = power;
        }
    }

    
    public void takeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            DestroyerHead.instance.takeDamage(50);
            Instantiate(explosion, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        

    }
}
