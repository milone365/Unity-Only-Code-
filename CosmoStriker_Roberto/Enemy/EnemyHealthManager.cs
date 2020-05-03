using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour,ITakeDamage
{
    [SerializeField]
    int health = 10;
    [SerializeField]
    GameObject effect;

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
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
}

