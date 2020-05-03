
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CorvetteLaser : MonoBehaviour
{
    public static Action afterLaser = delegate { };
    [SerializeField] GameObject charge, laser;
    public bool Shot;
    float chargeDealay=3;
    float shotTime = 5;
    float activingTime = 10;
    // Update is called once per frame
    void Update()
    {
        if (Shot)
        {
            if(chargeDealay>0)
            charge.SetActive(true);

            chargeDealay -= Time.deltaTime;
            if (chargeDealay <= 0)
            {
               
                if(charge.activeInHierarchy)
                charge.SetActive(false);

                if (!laser.activeInHierarchy)
                {
                    laser.SetActive(true);
                }
                shotTime -= Time.deltaTime;
                if (shotTime <= 0)
                {
                    laser.SetActive(false);
                    Shot = false;
                    chargeDealay = 3;
                    shotTime = 5;
                }

            }
        }
    }


   
    public void LaserActive()
    {
        Shot = true;
    }
}
