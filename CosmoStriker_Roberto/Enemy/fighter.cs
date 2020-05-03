using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighter : _tmpEnemys
{
    public bool randomMove;
    
    public EnemyBullet bulletPrefab;
    [SerializeField]
    private　Transform[] spawnpoint;
    //最小と最大の値を決める
    [SerializeField] private float minValue=-0.1f;
    [SerializeField] private float MaxValue=0.1f;
    //プレーヤーへ向かう
    [SerializeField] private float moveZ = -0.2f;
    float movex;
    float movey;
    bool battlequit = false;
    private PlayerController player;
    bool battleAsset = false;
    float attackdelay = 1f;
    private float attackrange = 30;
    bool canAttack;
   
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        randomMove = false;
        //ｘとｙ座標をランダムに決める
        movex = Random.Range(minValue, MaxValue);
        movey = Random.Range(minValue, MaxValue);
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public override void Movement()
    {
        if (!randomMove)
        {
            base.Movement();
        }
        else
        {
            if (!battleAsset)
            {
                transform.Translate(movex, movey, moveZ);
                StartCoroutine(resetBattlePositionCo());
            }
            else
            {
                if (player != null&&this.gameObject!=null) {

               
                var disstancetoplayer = (transform.position - player.transform.position).sqrMagnitude;

               
                 
                if (!battlequit)
                {
                    if (disstancetoplayer <= 500 && disstancetoplayer > attackrange)
                    {

                        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 0.2f);
                     
                     }
                    if (disstancetoplayer <= attackrange)
                    {
                        battlequit = true;
                        canAttack = false;
                    }
                    }
                }
                else
                {
                    transform.Translate(0, 0, 0.2f);
                }




                if (canAttack&&PlayerController.instance!=null) { 
                attackdelay -= Time.deltaTime;
                if (attackdelay <= 0)
                {
                    attackdelay = 1f;
                        if(transform.position.z>player.transform.position.z)
                    shot();
                    

                }
                }
            }
            
        }
       
    }

    IEnumerator resetBattlePositionCo()
    {
        yield return new WaitForSeconds(1f);
        battleAsset = true;

    }

    public  override void shot()
    {
        
            for (int i = 0; i < spawnpoint.Length; i++)
            {
                Instantiate(bulletPrefab, spawnpoint[i].position, transform.rotation);
            }
        
        
    }
 

}
