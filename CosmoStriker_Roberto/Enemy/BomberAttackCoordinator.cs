using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAttackCoordinator : MonoBehaviour
{
    [SerializeField]
    private float attackDelay=1f;
    private float attackTimer;
    [SerializeField]Transform[] attackPoints;
    [SerializeField] Bomber[] enemyes;
    [SerializeField]
    EnemyMissile missile;
    int missilenumbers = 0;
    int currentship = 0;
    
    Corvette[] corvettes = null;

    // Start is called before the first frame update
    void Start()
    {
        
        missilenumbers = 0;
        attackTimer = attackDelay;
        //corvetteを探す
        corvettes = FindObjectsOfType<Corvette>();
        if (corvettes != null)
        {
            //corvetteの攻撃タイプを変更する
            for (int i = 0; i < corvettes.Length; i++)
            {
                corvettes[i].changeFase("bullet");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        //時間が過ぎたら攻撃
        if (attackTimer <= 0)
        {
            attackTimer = attackDelay;
            //プロトンの攻撃
            if (enemyes[currentship] != null)
            {
                if(transform.position.z>PlayerController.instance.transform.position.z)
                enemyes[currentship].shot();
            }
            
            currentship++;
            if (currentship >= enemyes.Length)
            {
                currentship = 0;
            }

            //リーダーの攻撃
            if (canAttack)
            {
                if (PlayerController.instance != null)
                {
                    var distanceToPlayer = Vector3.Distance( transform.position , PlayerController.instance.transform.position);
                    if (distanceToPlayer <= 100f)
                    {
                        missilenumbers = 1;
                    }

                    for (int i = 0; i < attackPoints.Length; i++)
                    {

                        var obj = Instantiate(missile, attackPoints[i].position, attackPoints[i].rotation);
                        //callbackでミサイルの数量をコントロールする
                        obj.GetComponent<EnemyMissile>().Init(DestroyMissile);
                        missilenumbers++;
                    }
                }
                
                
            }

        }
    }

    private void DestroyMissile()
    {
       missilenumbers--;
    }
    //ミサイルがゼロになる場合、また攻撃ができる。

    public bool canAttack
    {
       get { return missilenumbers == 0; }
    }

}
