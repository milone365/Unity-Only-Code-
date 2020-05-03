using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    private bool battle,allert=false;
    enemyAnimationManager anim;
    public bool canAttack = true;
    float distanceToPlayer;
    public float attackrange = 2f;
    public float followRange = 7f;
    public static bool attacked = false;
    public Rigidbody rb;
    public float speed;

    public float rotationgrade;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<enemyAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allert)
        {


            if (!battle)
            {
                battle = true;
                anim.TriggerAnim("intro");

            }

            distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            if (distanceToPlayer <= followRange)
            {
                if (distanceToPlayer > attackrange)
                {

                    speed = Random.Range(0.05f, 0.15f);

                    anim.animationMove(true); 
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
        
    }
    public void battleActive()
    {
        allert = true;
    }
    public void dontMove()
    {

        anim.animationMove(false);
        speed = 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position, speed);

    }
    public virtual IEnumerator inflictdamage()
    {
        dontMove();
        canAttack = false;
        anim.animationAttak();
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
}
