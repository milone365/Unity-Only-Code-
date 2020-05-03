using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    Boss boss;
    
    public override void INIT()
    {
        base.INIT();
        boss = GetComponent<Boss>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            takeDamage(10);
        }
    }
    public override void takeDamage(float damage)
    {
        if (isDeath) { return; }
        healt -= damage;
        
        if (healt <= 0)
        {
            isDeath = true;
            boss.goToNextPhase();
        }
        else
        {
            int rnd = Random.Range(0, 11);
            if (rnd > 8)
            {
                if (anim != null)
                    anim.SetTrigger(StaticStrings.hurt);
            }
        }
    }
}
