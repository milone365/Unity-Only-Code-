using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MidleBoss : MonoBehaviour
{
    [SerializeField]
    EnemyStatus status=null;
    NavMeshAgent agent;
    Animator anim;
    Player player;
    LittleBossAction bossAction;
    EnemyHealth health;
    float attackCounter;
    bool useSpell = false;
    float spellTime = 30;
    [SerializeField]
    float spelltimeDelay = 30;
    [SerializeField]
    Transform magicSpawner=null;
    [SerializeField]
    bullet magic = null;
    MeleeWeapon weapon;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        attackCounter = status.attackDelay;
        health = GetComponent<EnemyHealth>();
        spellTime = spelltimeDelay;
        weapon = GetComponentInChildren<MeleeWeapon>();
       
    }

    
    void Update()
    {
        updating();
    }
    public void updating()
    {
        MoveUpdating();
        updateAnimator();
        spellTimerCheck();
    }
    void spellTimerCheck()
    {
        spellTime -= Time.deltaTime;
        if (spellTime <= 0)
        {
            spellTime = spelltimeDelay;
            
                changeState(LittleBossAction.spell);
        }
    }
    private void updateAnimator()
    {
        if (bossAction == LittleBossAction.spell) return;
        anim.SetFloat(StaticStrings.move,agent.velocity.magnitude);
    }

    private void MoveUpdating()
    {
        switch (bossAction)
        {
            case LittleBossAction.chasing:
                chasing();
                break;
            case LittleBossAction.attack:
                attack();
                break;
            case LittleBossAction.spell:
                preparingSpell();
                break;
        }
    }
    void chasing()
    {
        //プレーヤーへ移動
        if (player == null) return;
        Vector3 playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        agent.SetDestination(playerpos);
        float d = Vector3.Distance(transform.position, player.transform.position);

        if (d <= status.attackRange)
        {
            //攻撃stateへ行く
            changeState(LittleBossAction.attack);
        }
    }
    //actionを変わる
    private void changeState(LittleBossAction action)
    {
        bossAction = action;
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
            attackCounter = status.attackDelay;
            //攻撃
            anim.SetTrigger(StaticStrings.attack);
        }
        //距離チェック
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //遠いの場合は、chase motionへ行く
        if (distance > status.attackRange)
        {
            changeState(LittleBossAction.chasing);
        }
    }
    
    void preparingSpell()
    {
        if (agent.destination!=transform.position)
        {
            agent.SetDestination(transform.position);
        }
        //プレーヤーがなければ、出る
        if (player == null) return;
        //プレーヤーに合わせて回る
        transform.LookAt(player.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        if (!useSpell)
        {
            useSpell = true;
            StartCoroutine(castSpell());
        }
    }
    //無敵になって、アニメションPlay、flag->false,actionを変わる
   IEnumerator castSpell()
    {
        health.becomeInvincible();
        anim.SetTrigger(StaticStrings.magic);
        yield return new WaitForSeconds(5);
        useSpell = false;
        changeState(LittleBossAction.chasing);
    }
    //魔法スポンする
    public void InstantiateSpell()
    {
        if (player == null) return;
        bullet newBullet = Instantiate(magic, magicSpawner.position, magicSpawner.rotation)as bullet;
        Vector3 direction = player.transform.position - transform.position;
        newBullet.rb.AddForce(direction * 30);
        
    }
    //武器のコライダーをactive/deactive
    public void weaponAttack(int v)
    {
        if (weapon == null) { return; }
        weapon.attack(v);
    }
}
//Actions
public enum LittleBossAction
{
    chasing,
    attack,
    spell
}
