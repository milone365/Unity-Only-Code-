using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomberMissile : EnemyBullet
{
    [SerializeField]
    GameObject explosion;

    private void Update()
    {
        move();
    }

    public override void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<HealthManager>().Takedamage(damage);
             
            Destroy(gameObject);
        }
        if (other.tag == "KillZone"||other.tag=="Bullet")
        {
            
            Destroy(gameObject);
        }
        
       
    }

    public override void move()
    {
        // 登録した口座へ移動
        if (PlayerController.instance != null)
        {
            transform.position += target * Time.deltaTime * bulletSpeed * speedMultipler;
            

        }
        else
        {
            transform.Translate(0, 0, -bulletSpeed, Space.World);
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
