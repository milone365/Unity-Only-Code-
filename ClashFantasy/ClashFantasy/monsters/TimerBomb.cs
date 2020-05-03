﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBomb : MonoBehaviour
{
    float damage = 1;
    team team;
    float timerCount = 3;
    //flag
    bool Boom = false;
    //画像
    MeshRenderer[] renderers;
    bool start = false;
    public void INIT(float dmg,team tm)
    {
        team = tm;
        damage = dmg;
        renderers = GetComponentsInChildren<MeshRenderer>();
        start = true;
    }
    //タイマーと点滅
    private void Update()
    {
        if (!start) return;
        timerCount -= Time.deltaTime;
        foreach(var r in renderers)
        {
            if ((int)timerCount * 10 % 2==0)
            {
                r.enabled = true;
            }
            else
            {
                r.enabled = false;
            }
        }
        if (timerCount <= 0)
        {
            if (!Boom)
            {
                Boom = true;
                explode();
            }
        }
    }
    //爆弾の爆発
    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);
        foreach(var c in colliders)
        {
            Health h = c.GetComponent<Health>();
            if (h == null) continue;
            Iteam t = c.GetComponent<Iteam>();
            if (t.getTeam() == team) continue;
            h.takeDamage(damage);
        }
        EffectManager.instance.playInPlace(transform.position, "TowerExplosion");
        Destroy(gameObject);
    }

}
