using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemyAttackCoordinator : MonoBehaviour
{
    
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    fighter[] ships;
    private float LeaderfireRate=2f;
    [SerializeField]
    private float plotonFireRate=1f;
    [SerializeField]
    private float fireTime;
    private int currentShip;
    [SerializeField]
    Transform[] Leaderfirepoint;



    // Start is called before the first frame update
    void Start()
    {
        LeaderfireRate = fireTime;

    }

    // Update is called once per frame
    void Update()
    {
        //リーダーの攻撃
        LeaderfireRate -= Time.deltaTime;
        if (LeaderfireRate <= 0)
        {
            LeaderfireRate = fireTime;
            for(int i = 0; i < Leaderfirepoint.Length; i++)
            {
                Instantiate(bulletPrefab, Leaderfirepoint[i].position, Leaderfirepoint[i].rotation);
            }
           
        }
        //プロトンの攻撃
        plotonFireRate -= Time.deltaTime;
        if (plotonFireRate <= 0)
        {
            plotonFireRate = 1f;
            spawnBullet(currentShip);
            currentShip++;
            if (currentShip >= ships.Length)
            {
                currentShip = 0;
            }
        }


           

        
    }

    private void spawnBullet(int currentShip)
    {
        if (ships[currentShip] != null)
        {
            ships[currentShip].shot();
           
         }


    }
}
