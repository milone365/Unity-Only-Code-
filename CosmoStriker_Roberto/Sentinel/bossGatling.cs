using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossGatling : MonoBehaviour
{
    BossBulletsSpawner bulletSpawner;
    [SerializeField]
    float fireDelay = 0.25f;
    float fireCounter;
    public bool fire;
    int bulletSpawned;

    // Start is called before the first frame update
    void Start()
    {
        fire = false;
        bulletSpawner = GetComponent<BossBulletsSpawner>();
        bulletSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {

            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireDelay;
                bulletSpawner.bulletFire();
                bulletSpawned++;
                if (bulletSpawned > 6)
                {
                    fire = false;
                    bulletSpawned = 0;
                }
                 }
        }
    }

    public void Return(){
        GetComponent<Animator>().SetTrigger("return");
        }
    public void gatlingActive()
    {
        fire = true;
        fireCounter = fireDelay;
    }
}
