using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI:MonoBehaviour
{
    public float speed;
    
    public float rotationgrade;
    public enemyAnimationManager animManager;
    public bool canAttack = true;
    float distanceToPlayer;
    public float attackrange = 2f;
    public float followRange = 7f;
    public static bool attacked = false;
    public Rigidbody rb;


    
    //private bool hurt = false;

    public virtual void Move()
    {
        distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        if (distanceToPlayer <= followRange)
        {
            if (distanceToPlayer > attackrange)
            {
                
                speed = Random.Range(0.05f, 0.15f);

                animManager.animationMove(true);
                transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.plyerHand.transform.position, speed);
                //isAttacking = false;
            }
            else
            {
                
                if (canAttack)
                {
                    StartCoroutine(inflictdamage());
                }
            }
                
        }
        else
        {
            dontMove();
            
            
        }

         transform.LookAt(PlayerController.instance.playerModelChild.transform.position, Vector3.up);
         transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + rotationgrade, 0f);
 }

    public void dontMove()
    {

        animManager.animationMove(false);
        speed = 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position, speed);
        
    }
    public virtual IEnumerator inflictdamage()
    {
        dontMove();
        canAttack = false;
        animManager.animationAttak();
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
   
   
    
}

