using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    
    //攻撃力など、ボスの様々値が入っているクラス
    [SerializeField]
    EnemyStatus state=null;
    //ステップの管理
    List<BossPhase> phases = new List<BossPhase>();
    BossPhase currentPhase;
    int phaseIndex = 0;
    //magic
    
    Transform magicSpawner = null;
    [SerializeField]
    bullet magic = null;
    [SerializeField]
    bullet knife = null;
    Transform knifeSpawner = null;
    //
    NavMeshAgent ag;
    Animator anim;
    Player p;
    Weapon w;
    //minons
    [SerializeField]
    GameObject enemyToSpawn = null;
    [SerializeField]
    GameObject enemmyToSpawn2 =null;
    GameObject enemy=null;
    //teleport
    Vector3 startPos=Vector3.zero;
    ParticleSystem smoke = null;
    bool isDead = false;
    Weapon wp = null;
    EnemyHealth health;
   
    void Start()
    {
        anim = GetComponent<Animator>();
        ag = GetComponent<NavMeshAgent>();
        p = FindObjectOfType<Player>();
        w = GetComponentInChildren<Weapon>();
        health = GetComponent<EnemyHealth>();
        //バトルはステップに別れています、リストのなかでステップを作る
        BossPhase phase1 = new Phase1(ag, anim, state, w, p,transform,this);
        BossPhase phase2 = new Phase2(ag, anim, state, w, p,transform,this);
        BossPhase phase3 = new Phase3(ag, anim, state, w, p,transform,this);
        phases.Add(phase1);
        phases.Add(phase2);
        phases.Add(phase3);
        //スポーンポジションを登録する
        startPos = transform.position;
        smoke = GetComponentInChildren<ParticleSystem>();
        //現在phaseの設定
        currentPhase = phases[phaseIndex];
        //phaseエントリー
        currentPhase.EnterState();
        wp = GetComponentInChildren<Weapon>();
        //スポンサー設定
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach(var c in allChildren)
        {
            if(c.gameObject.name== "KnifeSpawner")
            {
                knifeSpawner = c.transform;
            }
            else if( c.gameObject.name== "MagicSpawner")
            {
                magicSpawner = c.transform;
            }
        }
        //スポーンprefab
        enemy = enemyToSpawn;
    }

   
    void Update()
    {
        if (isDead) return;
        //現在スペップのUpdate
        currentPhase.Updating();
        updateAnimator();
        if (Input.GetKeyDown(KeyCode.M))
        {
            goToNextPhase();
        }
       
    }
    void updateAnimator()
    {
        anim.SetFloat(StaticStrings.move, ag.velocity.magnitude);
    }
    //次のステップへ行く、もし最後のステップだったら死ぬ
    public void goToNextPhase()
    {
        
        //まずは現在のステップから出ます
        currentPhase.ExitState();
        phaseIndex++;
        if (phaseIndex >= phases.Count)
        {
            Death();
        }
        else
        {
            
            currentPhase = phases[phaseIndex];
            //ステップに入る
            currentPhase.EnterState();
            restoreHealth();
        }
    }
    #region spells
    //スタートポイントに戻ってエフェクトplay
    public void teleport()
    {
        if (smoke != null)
        {
            smoke.Play();
        }
        transform.position = startPos;
    }
    //敵のをスポンする
    public void SpawnMinion()
    {

        for(int i = 0; i < 4; i++)
        {
            Vector3 spawnPos;
            float x=0, z=0;
            switch (i)
            {
                case 0: x = 2;z = 2; break;
                case 1:x = -2;z = -2; break;
                case 2: x=4; z = 4; break;
                case 3: x = -4; z = -4; break;
                case 4: x = -6; z = -6; break;
 
            }
            spawnPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
            Instantiate(enemy, spawnPos, transform.rotation);
           
        }
        //敵の種類を変わる
        if (enemy == enemyToSpawn)
        {
            enemy = enemmyToSpawn2;
        }
        else
        {
            enemy = enemyToSpawn;
        }
    }
    #endregion
    //回復
     void restoreHealth()
    {
        health.respawn();
    }
    //武器のcolliderをactive/deactive
    public void weaponAttack(int v)
    {
        if (wp == null) { return; }
        wp.attack(v);
    }
    //死
    private void Death()
    {
        isDead = true;
        StartCoroutine(deathCo());
    }

    IEnumerator deathCo()
    {
        anim.SetTrigger(StaticStrings.death);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    //魔法スポンサー
        public void InstantiateSpell()
    {
        if (p == null) return;
        bullet newBullet = Instantiate(magic, magicSpawner.position, magicSpawner.rotation) as bullet;
        Vector3 direction = p.transform.position - transform.position;
        newBullet.rb.AddForce(direction * 30);

    }
    //バレットスポン
    public void ThrowKnife()
    {
        if (p == null) return;
        bullet newBullet = Instantiate(knife, knifeSpawner.position, knifeSpawner.rotation) as bullet;
        Vector3 direction = p.transform.position - transform.position;
        newBullet.rb.AddForce(direction * 30);
    }
    public void SpellCO()
    {
        StartCoroutine(castSpell());
    }
    
     IEnumerator castSpell()
    {
        //無敵になって
        health.becomeInvincible();
        if (currentPhase == phases[1])
        {
            //アニメション
            anim.SetTrigger(StaticStrings.magic);
        }
        else
        {
             //アニメション
            anim.SetTrigger(StaticStrings.special);
            yield return new WaitForSeconds(5);
            
        }
       
        yield return new WaitForSeconds(5);
        //phase2Classの値をリセットする
        currentPhase.resetValues();
    }
}
