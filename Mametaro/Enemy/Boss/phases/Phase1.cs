using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Phase1 : BossPhase
{
    
    public Phase1(NavMeshAgent ag, Animator an, EnemyStatus sta , Weapon w, Player p, Transform t,Boss b) : base(ag, an, sta, w, p,t,b)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        changeState(BossActions.chase);

    }
    public override void ExitState()
    {
        base.ExitState();      
    }
    public override void Updating()
    {
        base.Updating();
        switch (currentAction)
        {
            case BossActions.chase:
                chase();
                break;
            case BossActions.attack:
                attack();
                break;
        }
        
    }

    void chase()
    {
        if (player == null) return;
        Vector3 playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        agent.SetDestination(playerpos);
        float d = Vector3.Distance(transform.position, player.transform.position);

        if (d <= status.attackRange)
        {
            //攻撃へ行く
            changeState(BossActions.attack);
        }
    }
    void attack()
    {
        if (agent.destination != transform.position)
        {
            agent.SetDestination(transform.position);
        }
        //プレーヤーがなければ、出る
        if (player == null) return;
        //プレーヤーに合わせて回る
        transform.LookAt(player.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0)
        {
            attackCounter = attackDelay;
            //攻撃
            anim.SetTrigger(StaticStrings.attack);
        }
        //距離チェック
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //遠いの場合は、chase motionへ行く
        if (distance > status.attackRange)
        {
            changeState(BossActions.chase);
        }
    }
}
public enum BossActions
{
    chase,
    attack,
    magic
}
