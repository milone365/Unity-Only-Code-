﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour,Iteam
{
    Health health;
    team tm;
    GameManager gm;
    public team getTeam()
    {
        return tm;
    }
    public towerType TowerType = towerType.pricess;
    [SerializeField]
    Color blue = new Color();
    
    //hpバーの色を変わる
    void changeColorToHPbar()
    {
        if (health.getBar() == null)
        {
            Debug.Log(gameObject.name + "hpbar is null");
            return;
        }
        if (tm == team.Ateam)
            {
            //blue
            health.getBar().changeColor(blue);
            
            }
    }
    public void Ininialize(team t)
    {
        tm = t;
        health = GetComponent<Health>();
        float towerHp = 0;
        //hpを決める
        switch (TowerType)
        {
            case towerType.pricess:
                towerHp = 2534;
                health.onDeath += givePointToEnemy;
                break;
            case towerType.king:
                towerHp = 4008;
                health.onDeath += I_Lose;
                break;
        }
        health.initializeHP(towerHp);
        changeColorToHPbar();
        gm = FindObjectOfType<GameManager>();
    }
    //敵にポイントを追加する
    public void givePointToEnemy(Transform t)
    {
        gm.addPointToPlayer(tm);
    }
    //ゲームを負ける
    public void I_Lose(Transform t)
    {
        gm.LoseTeam(tm);
    }
    
}
public enum towerType
{
    pricess,
    king
}