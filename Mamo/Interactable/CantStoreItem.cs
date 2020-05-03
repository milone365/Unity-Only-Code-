using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantStoreItem : Item
{
    public bool isMoney, isHealth;
    public int valor;


    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isMoney)
            {
                GameManager.instace.addCoint(valor);
                Instantiate(particle, transform.position, transform.rotation);
            }
            if (isHealth)
            {
                HealthManager.instace.HealPlayer(valor);
                Instantiate(particle, transform.position, transform.rotation);
            }
            AudioManager.instance.playSFX(8);
            Destroy(gameObject);

        }
    }
}
