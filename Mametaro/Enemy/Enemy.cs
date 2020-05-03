using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   
    NavMeshAgent agent;
    Animator anim;
    //Patrollの為の値
    [SerializeField]
    float x=10, z=10;
    //idleの待つ時間
    float waiting = 0;
    float waitingCounter = 0;
    //現在のmotion
    public EnemyMotion currentMotion = EnemyMotion.patrolling;
    //wayponts
    int currentPatrollPoint = 0;
    Vector3[] patrollPoints = new Vector3[3];
    //Gizmos
    Vector3 AllertArea= new Vector3();
    
    Player player = null;
    Weapon wp = null;
    //攻撃力、などなど値を持っているクラス
    [SerializeField]
    protected EnemyStatus status;
    //何秒の間攻撃する
    protected float attackCounter = 1;
    protected float attackDelay = 1;

    #region Init
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //waypointを決める
        patrollPoints[0] = transform.position;
        patrollPoints[1] = new Vector3(transform.position.x - x, transform.position.y, transform.position.z + z);
        patrollPoints[2] = new Vector3(transform.position.x + x, transform.position.y, transform.position.z - z);
        //値渡し
        attackCounter = status.attackCounter;
        attackDelay = status.attackDelay;
        waiting = status.waiting;
        waitingCounter = status.waitingCounter;
        AllertArea = new Vector3(status.chasingRange, status.chasingRange, status.chasingRange);
        //プレーヤーを探す
        player = FindObjectOfType<Player>();
        //武器にアクセスする
        wp = GetComponentInChildren<Weapon>();
        //最初のmotionを決めます
        changeState(EnemyMotion.idle);
        
    }

    #endregion
    void Update()
    {
        updateMotion();
        updateAnimator();
    }
    #region Motions
    void updateMotion()
    {
        switch (currentMotion)
        {
            case EnemyMotion.idle:
                idle();
                break;
            case EnemyMotion.patrolling:
                patroll();
                break;
            case EnemyMotion.chase:
                chase();
                break;
            case EnemyMotion.attack:
                attack();
                break;
        }
    }
    //時間が経ったら攻撃アニマシオンが動く
    private void attack()
    {
        if (agent.destination != transform.position)
        {
            agent.SetDestination(transform.position);
        }
       //プレーヤーがなければ、出る
        if (player == null) return;
        //プレーヤーに合わせて回る
        transform.LookAt(player.transform,Vector3.up);
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
            changeState(EnemyMotion.chase);
        }
    }
 
    void idle()
    {
        //止まって
        if (agent.destination != transform.position)
        {
            agent.SetDestination(transform.position);
        }
        waitingCounter -= Time.deltaTime;
        //時間が経ったら次のwaypointへ行く
        if (waitingCounter <= 0)
        {
            waitingCounter = waiting;
            currentPatrollPoint++;
            currentPatrollPoint %= patrollPoints.Length;
            agent.SetDestination(patrollPoints[currentPatrollPoint]);
            changeState(EnemyMotion.patrolling);
            
        }
        //距離とプレーヤーの存在チェック
        if (player == null) return;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= status.chasingRange)
        {
            changeState(EnemyMotion.chase);
        }
    }
    //waypointに着いたら止まる
    void patroll()
    {
        if (agent.remainingDistance <= 0.5f)
        {
            changeState(EnemyMotion.idle);
        }
        //プレーヤーの存在と距離チェック
        if (player == null) return;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= status.chasingRange)
        {
            changeState(EnemyMotion.chase);
        }
    }
    //プレーヤーへ向かう
    void chase()
    {
        if (player == null) return;
        //agentのPathが出ないようにｙ座標は変わりません

        Vector3 playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        agent.SetDestination(playerpos);
        float d = Vector3.Distance(transform.position, player.transform.position);
        if (d > status.chasingRange)
        {
            //waypointへ行く
            changeState(EnemyMotion.patrolling);
        }
        else if (d <= status.attackRange)
        {
            //攻撃へ行く
            changeState(EnemyMotion.attack);
        }
    }
    #endregion
    //enumクラスの中で値を決める
    public void changeState(EnemyMotion motion)
    {
        currentMotion = motion;  
    }
    void updateAnimator()
    {
        anim.SetFloat(StaticStrings.move, agent.velocity.magnitude);
    }
    //アニメーションに呼ばされている関数
    public void weaponAttack(int v)
    {
        if (wp == null) { return; }
        wp.attack(v);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,AllertArea);
  
    }

    public void InstantiateSpell()
    {
        Debug.Log("magic");
    }

}
public enum EnemyMotion
{
    idle,//待つ
    patrolling,//waypointへ行く
    chase,//プレーヤーへ動く
    attack,//攻撃
}
