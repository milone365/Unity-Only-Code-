using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corvette : MonoBehaviour 
{

    [SerializeField] CannonMovement laserCannon;
    
    //attackポジション
    [SerializeField] Transform[] missileSpawnpoints;
    [SerializeField] Transform[] bulletsSpawnPoints;
    //時間
    [SerializeField] float attackdelay = 2f;
     float attackTimer;
    [SerializeField]
    float damageMultipler = 0.5f;
    [SerializeField] EnemyMissile missiles;
    public EnemyBullet bullet;
    [SerializeField]
    private int debritsNumberToSpawn;//ゴミ数
    public GameObject[] debridsToSpawn;//ゴミprefab
    
   Transform player;

    [SerializeField] float offset;
    
   
   public enum attackType { missile, bullet}
    public attackType attack;
    // Start is called before the first frame update
    void Start()
    {
       laserCannon = GameObject.Find("mobileCannoX").GetComponent<CannonMovement>();
        
       if(PlayerController.instance!=null)
        player = PlayerController.instance.transform;
  
    }

    // Update is called once per frame
    void Update()
    {
          attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                randomChange(); 
                attackTimer = attackdelay;

            switch (attack)
            {   case attackType.missile: missileFire(); laserCannon.canMove = false; break;
                case attackType.bullet: StartCoroutine(bulletFire()); laserCannon.canMove = false; break;
            }
       }
    
   }

    //バレット攻撃
 public IEnumerator bulletFire() {
        if(player.transform.position.y>transform.position.y-offset)
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < bulletsSpawnPoints.Length; j++)
            {
                Instantiate(bullet, bulletsSpawnPoints[j].position, bulletsSpawnPoints[j].rotation);
            }
            yield return new WaitForSeconds(0.5f);
        }


    }
    //ミサイル攻撃
    void missileFire() {
        for (int i = 0; i < missileSpawnpoints.Length; i++)
        {
            Instantiate(missiles, missileSpawnpoints[i].position, missileSpawnpoints[i].rotation);
        }
    }


    //攻撃タイプを変更する
    public void changeFase(attackType type)
    {
        attack = type;
 }

    
    public void spawnDebrids(int num)
    {
        for (int i = 0; i < num; i++)
        {
            ///オブジェクト配列の中でinstantiateするゴミを選択する
            int rand_Sel = Random.Range(0, debridsToSpawn.Length);
            //ランダムポジションを作るため
            float randomPos = Random.Range(-2, 2);
            Instantiate(debridsToSpawn[rand_Sel], new Vector3(transform.position.x + randomPos, transform.position.y + randomPos, transform.position.z + randomPos), transform.rotation);
        }
    }


    
    public void randomChange()
    {
        int num = Random.Range(0, 10);
      
        if (num >= 5)
        {
            attack = attackType.bullet;
        }
        else
        {
            attack = attackType.missile;
        }
       
       
    }
}
