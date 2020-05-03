using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvetteMovement : MonoBehaviour
{
    [SerializeField] float Range = 0.03f;
    [SerializeField] float moveX;
    [SerializeField] float moveY;
    [SerializeField] float moveZ=-0.002f;
    [SerializeField]
    float distanceToPlayer;
    //50以上必ず
    
    [SerializeField] float moveSpeed=0.05f;
    [SerializeField] float changeMovementTimer = 3f;
    [SerializeField]
    float changeMovementCounter;
    [SerializeField]
    int changePhaseLimiter = 10;
    Vector3 startPosition;
    int phaseLimit;
    bool reAsset;
   
    //動きの管理ため
    public enum moveBattlePhases { phase1,phase2,phase3}
    //現在
    public moveBattlePhases currentMovePhase;

    // Start is called before the first frame update
    void Start()
    {

        Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
       
         changeMovementCounter -= Time.deltaTime;
        if (changeMovementCounter <= 0)
        {
            //時間がたったら、反対側へ動く、決めた回に繰り返す。カウンターゼロになったらphase変更
            ChangeMovementManagement();
            
        }
        if (changePhaseLimiter <= 0)
        {

            changePhase();

        }
        
          if (currentMovePhase == moveBattlePhases.phase1)
            {

  
                transform.Translate(moveX, moveY, moveZ, Space.World);


               
            }
            if (currentMovePhase == moveBattlePhases.phase2)
            {
               transform.Translate(moveX, moveY, moveZ, Space.World);
   
            }
            //三回目後ろへ動く、最高の距離までそのあと最初のphaseに戻る
            if (currentMovePhase == moveBattlePhases.phase3)
            {

                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed);
                if (transform.position == startPosition)
                {
                    changePhase();
                }

            }

            if (transform.position.z < PlayerController.instance.transform.position.z)
            {
                moveZ *= -1;
            }

        }
        
        
        /* transform.LookAt(PlayerController.instance.transform);
         transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y , 0);*/
    

    public void ChangeMovementManagement()
    {
        changeMovementCounter = changeMovementTimer;
        //+から-、-から+
        moveX *= -1;
        moveY *= -1;
        //カウンターを減らす
        changePhaseLimiter--;
    }
    public void changePhase()
    {
       
        currentMovePhase++;
       
        if (currentMovePhase > moveBattlePhases.phase3)
        {
            //リセット
            currentMovePhase = 0;
        }
        //カウンターリセット
        changePhaseLimiter = phaseLimit;
        
            //ランダムの座標のリセット
            moveX = Random.Range(-Range, Range);
            moveY = Random.Range(-Range, Range);
            moveZ *= -1f;
       
    }
    public void Initialize()
    {
        currentMovePhase = moveBattlePhases.phase1;
        //ランダム
        moveX = Random.Range(-Range, Range);
        moveY = Random.Range(-Range, Range);
        changeMovementCounter = changeMovementTimer;
        phaseLimit = changePhaseLimiter;
        startPosition = transform.position;
        transform.LookAt(PlayerController.instance.transform);
    }
}
