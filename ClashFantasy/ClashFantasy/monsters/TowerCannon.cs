using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannon : Build
{
    //overrideで中身を変更する
    //アニメションはありませんからです
    public override void defenceBeaviour()
    {

        if (target == null)
        {
            Collider[] visibileObjects = Physics.OverlapSphere(transform.position, viewField);
            foreach (var c in visibileObjects)
            {
                //ターゲットを見付ける
                Character ch = c.transform.GetComponent<Character>();
                if (ch != null && ch.getTeam() != tm)
                {
                    target = c.transform;
                    break;
                }
            }
            return;
        }
        else
        {
            //攻撃する
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0)
            {
                attackCounter = attackDelay;
                castBullet();
            }
        }
    }
}