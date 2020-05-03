using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPhase 
{
   protected Animator anim;
   protected NavMeshAgent agent;
   protected EnemyStatus status;
   protected Weapon weapon;
   protected Player player = null;
   protected bool canmove=false;
   protected Transform transform;
    protected BossActions currentAction;
    protected float attackCounter;
    protected float attackDelay;
    protected Boss boss;
    
    //conntructor値渡し
    public BossPhase(NavMeshAgent ag, Animator an,EnemyStatus sta,Weapon w,Player p,Transform t,Boss b)
    {
        anim = an;
        agent = ag;
        status = sta;
        weapon = w;
        player = p;
        transform = t;
        attackCounter = status.attackCounter;
        attackDelay = status.attackDelay;
        boss = b;
    }
    public virtual void Updating()
    {
        if (!canmove) return;
        if (player == null) return;
    }

    public virtual void EnterState()
    {
        canmove = true;
    }

    public virtual void ExitState()
    {
        canmove = false;
        agent.ResetPath();  
    }
    public void changeState(BossActions action)
    {
        currentAction = action;
    }
    //phase2、3の為
    public virtual void resetValues() { }
}
