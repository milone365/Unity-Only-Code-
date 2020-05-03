using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    /// <summary>
    /// Attacka>bullet, Attackb>raygun,Attackc>cannon,Attack>missiles
    /// </summary>
    public enum State { Attacka = 1, Attackb, Attackc, Attackd }
   
    public State currentState;

    Animator anim;
    public static int changeCounter;
    State attackMemory;
    BossBulletsSpawner bulletSpawner;
    bool canAttack;
    float missileTime = 3,missileCounter;

    //phase1 bullet and raygunAttack, phase2 only cannon, phase3 all attacks, end death
    public enum phase {  phase1=1,  phase2 , phase3,end }
    public phase currentphase = phase.phase1;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bulletSpawner = GetComponent<BossBulletsSpawner>();
        changeCounter = 3;
        currentState = State.Attacka;

        //イベントに関数を追加する
        BossHealth.instance.healthIsLess += healtISLess;

    }

    // Update is called once per frame
    void Update()
    {
        //statusによってanimation又は攻撃が変わる
        if(currentphase!=phase.end)
        switch (currentState)
        {
            case State.Attacka: anim.SetInteger("atk", 1); break;
            case State.Attackb: anim.SetInteger("atk", 2); break;
            case State.Attackc: anim.SetInteger("atk", 3); break;
            case State.Attackd: anim.SetTrigger("matk");
                    changeCounter = 0; goToIdle(); break;

        }
        

    }

    private void missileSpawn()
    {
        missileCounter -= Time.deltaTime;
        if (missileCounter <= 0)
        {
            missileCounter = missileTime;
            bulletSpawner.MissileAttack();
            goToIdle();
        }

        
    }

    public void goToIdle()
    {
        changeCounter--;

        if (changeCounter <= 0)
        {
            changeCounter = 3;
            currentState++;
            //phaseによってstateはリミットがある
            switch (currentphase)
            {
                case phase.phase1:
                    if (currentState > State.Attackb)
                    {
                        currentState = State.Attacka;
                    }
                    break;

                case phase.phase2:
                    if (currentState > State.Attackc)
                    {
                        currentState = State.Attackc;
                    }
                    break;
                case phase.phase3:
                    if (currentState > State.Attackd)
                    {
                        currentState = State.Attacka;
                    }
                    break;
             }

        }
  }

    //イベントから呼ばれた機能、numによってphaseが変わる
    public void healtISLess(int num)
    {
        if (num == 2)
        {
            currentState = State.Attackc;
            currentphase = phase.phase2;
        }
        else if (num == 3)
        {
            currentState = State.Attackd;
            currentphase = phase.phase3;
        }else if (num == 4)
        {
            currentphase = phase.end;
            anim.SetTrigger("dead");
        }
    }
}

