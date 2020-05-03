using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeactivetor : MonoBehaviour
{
    public GameObject bossActivetor;
    public GameObject boss;
    public bool isDead;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            
                if (boss.activeInHierarchy&&!isDead)
                {
                    boss.SetActive(false);
                    UIManager.instance.deactiveEnemyBar();
                bossActivetor.SetActive(true);

            }
    
        }
    }
}
