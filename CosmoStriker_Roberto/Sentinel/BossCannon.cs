using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannon : MonoBehaviour
{
     BossBulletsSpawner bulletSpawner;
   
    [SerializeField]
    float fireDelay = 0.5f;
    float fireCounter;
    public bool fire;
    int patan = 4;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpawner = GetComponent<BossBulletsSpawner>();
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
                patan--;
                bulletSpawner.cannonAttack();
                if (patan <= 0)
                {
                    fire = false;
                    GetComponent<Animator>().SetTrigger("return");
                }
            }
        }
    }

    public void cannonActive()
    {
        fire = true;
        fireCounter = fireDelay;
        patan = 4;
    }
}
