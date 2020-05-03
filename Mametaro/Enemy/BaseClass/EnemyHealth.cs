using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    protected float maxHelath = 10;
    protected float healt = 0;
    protected bool isDeath;
    protected Animator anim;
    bool isInvincible = false;
    [SerializeField]
    float invinibleCounter = 5;
    float invincibleTime = 5;

    void Start()
    {
        INIT();
    }

    public virtual void INIT()
    {
        healt = maxHelath;
        anim = GetComponent<Animator>();
    }
    //無敵になる
    public void becomeInvincible()
    {
        isInvincible = true;
    }
    //無敵の時のタイマー
    private void Update()
    {
        if (isInvincible)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime <= 0)
            {
                isInvincible = false;
                invincibleTime = invinibleCounter;
            }
        }
    }
    //回復とflagのリセット
    public void respawn()
    {
        healt = maxHelath;
        isDeath = false;
    }
   
    //ダメージを受ける
  public virtual void takeDamage(float damage)
    {
        if (isDeath||isInvincible) { return; }
        healt -= damage;
        
        if (healt <= 0)
        {
            isDeath = true;
            //動かないようにする
            NavMeshAgent ag = GetComponent<NavMeshAgent>();
            if (ag != null)
                ag.SetDestination(transform.position);
            //死のアニメション
            GetComponent<Animator>().SetTrigger(StaticStrings.death);
            Destroy(gameObject,3);
        }
        else
        {
            //randomで決めて
            int rnd = Random.Range(0, 11);
            if (rnd > 8)
            {
                //痛みのアニメション
                if (anim != null)
                    anim.SetTrigger(StaticStrings.hurt);
            }
        }

    }
}
