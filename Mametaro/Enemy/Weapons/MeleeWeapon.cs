using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    Collider col = null;
    [SerializeField]
    float attackPower = 1;
    private void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }
    //collider active/deactive
    public override void attack(int i)
    {
        if (i != 0)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
       
        
    }
    //ダメージを付ける
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticStrings.player)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
             playerHealth.takeDamage(attackPower);
            
        }
    }
}
