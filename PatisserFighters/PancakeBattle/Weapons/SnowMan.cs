using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMan : MonoBehaviour
{
    [SerializeField]
    int maxMovement = 6;
    [SerializeField]
    ParticleSystem snow=null;
    int movement = 0;
    GameObject objectToFreez = null;
    bool go = false;
    bool ai=false;
    float frozeTimer = 1.5f;
    float scrollingTime = 1.5f;
    void Update()
    {
        if (ai)
        {
            automaticScroll();
        }
        else
        {
            if (Input.GetButtonDown(StaticStrings.X_key))
            {
                addMovement();
            }
        }
    }
    //インデクスに1を足す、上限に着いたら雪だるまを削除して、キャラクターを活性する
    void addMovement()
    {
        movement++;
        GetComponent<Animator>().SetTrigger(StaticStrings.Scroll);
        snow.Play();
        if (movement > maxMovement)
        {
            if (!go)
            {
                go = true;
                ripristing();
            }
            
        }
    }
    //reference
    public void objectToFreeze(GameObject obj,bool isAI)
    {
        objectToFreez = obj;
        objectToFreez.SetActive(false);
        ai = isAI;
    }
    //無効されたオブジェクトを活性する
    private void ripristing()
    {
        if(objectToFreez!=null)
        objectToFreez.SetActive(true);
        FiniStateMachine sm = objectToFreez.GetComponent<FiniStateMachine>();
        if (sm != null) { sm.changeState(stateType.cake); }
        Destroy(this.gameObject);
    }
    //時間が終ったら自動的に関数を呼ぶ
    public void automaticScroll()
    {
        frozeTimer -= Time.deltaTime;
        if (frozeTimer <= 0)
        {
            frozeTimer = scrollingTime;
            addMovement();
            
        }
    }
}
