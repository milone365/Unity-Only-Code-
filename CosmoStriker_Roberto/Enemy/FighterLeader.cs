using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterLeader : _tmpEnemys
{
    Renderer[] rends;

    fighter[] allEnemyes;
    //protected float hitTimer = 0.15f;
   // bool hit;
    public void Start()
    {
        rends = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        Movement();
    }

    public override void Movement()
    {
      /*  if (hit)
        {
            if (hitTimer > 0)
            {

                foreach (var r in rends)
                {
                    r.enabled = false;
                }
                hitTimer -= Time.deltaTime;
                if (hitTimer <= 0)
                {
                    hit = false;
                    foreach (var r in rends)
                    {
                        r.enabled = true;
                    }
                }
            }
        }*/
     
    }
    public override void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            //hit = true;
            ushort _dmg = collision.gameObject.GetComponent<_tmpBullet>().Damage;
            HP -= _dmg;
            //hitTimer = 0.15f;
            if (HP <= 0)
            {

                allEnemyes= FindObjectsOfType<fighter>();
                for(int i = 0; i < allEnemyes.Length; i++)
                {
                    allEnemyes[i].randomMove = true;
                }
                Instantiate(Explode, transform.position, transform.rotation); //現在位置にエフェクト生成
                                                                              //ゴミを作る
                spawnDebrids(3);
                Destroy(this.gameObject); //HP無くなったらDestroy

            }


        }
        if (collision.gameObject.name == "KillZone")
        {
            Destroy(this.gameObject); //KZ入ったらDestroy
        }
    }
}
