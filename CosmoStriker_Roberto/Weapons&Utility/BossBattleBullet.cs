using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBullet : EnemyBullet
{
    // Start is called before the first frame update
    void Start()
    {
        Initializing();
    }

    private void Update()
    {
        base.move();
    }
    public override void Initializing()
    {

        if (PlayerController.instance == null)
        {
            return;
        }

        player = PlayerController.instance.transform;
       
        target = player.position - transform.position;
        Vector3 dir = player.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
    }
}

