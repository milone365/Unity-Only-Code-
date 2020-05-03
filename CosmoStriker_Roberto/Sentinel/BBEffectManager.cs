using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBEffectManager : MonoBehaviour
{
    [SerializeField] GameObject[] partToBrake;
    BossHealth bossHealth;
    // Start is called before the first frame update
    void Start()
    {
        //reference
        bossHealth = BossHealth.instance;
        
        //イベントに関数を追加する
       bossHealth.healthIsLess += destroyPart;
    }

    //イベントの数によって違う部分が爆発する
    public void destroyPart(int num)
    {
        if (num < partToBrake.Length)
        {
            //bossManagerのスクリプトからdebrisとexplosionを使う
            Instantiate(bossHealth.debris[Random.Range(0,bossHealth.debris.Length-1)], partToBrake[num].transform.position, partToBrake[num].transform.rotation);
            Instantiate(bossHealth.deathEffect, partToBrake[num].transform.position, partToBrake[num].transform.rotation);
            //部分を消す
            partToBrake[num].SetActive(false);
        }
    }
}
