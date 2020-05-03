using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatroll : MonoBehaviour
{
    public bool haveMultipleAttack;
   public enum enemyState { idle,patroll,chasing,attack}
    public int currentPatrollPoint = 0;
    public enemyState currentState=enemyState.idle;
    public Transform [] patrollPoints;
    public NavMeshAgent agent;
    public enemyAnimationManager anim;
    public float chaseRange=10f;
    public float attackRange = 1f;
    public float timeBetweenAttacks=2f;
    private float attackCounter;
    public float waitAtPoint = 2f;
    private float waitCounter;
    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (currentState)
        {
            case enemyState.idle: anim.animationMove(false);
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else { currentState = enemyState.patroll;
                    agent.SetDestination(patrollPoints[currentPatrollPoint].transform.position);
                }
                if (distanceToPlayer <= chaseRange) { currentState = enemyState.chasing;
                    anim.animationMove(true);
                } break;
            case enemyState.patroll: if (agent.remainingDistance <= .2f) {
                    currentPatrollPoint++;
                    if (currentPatrollPoint >= patrollPoints.Length)
                    {
                        currentPatrollPoint = 0;
                    }
                    currentState = enemyState.idle;
                    waitCounter = waitAtPoint;
                }
                if (distanceToPlayer <= chaseRange) {
                    currentState = enemyState.chasing;
                }anim.animationMove(true); break;
            case enemyState.chasing: agent.SetDestination(PlayerController.instance.transform.position);
                if (distanceToPlayer <= attackRange) { currentState = enemyState.attack;
                    if (haveMultipleAttack)
                    {
                        anim.randomAttack();
                    }
                    else
                    {
                        anim.animationAttak();
                    }
                    
                    anim.animationMove(false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    attackCounter = timeBetweenAttacks;
                }
                if (distanceToPlayer > chaseRange)
                {
                    currentState = enemyState.idle;
                    waitCounter = waitAtPoint;
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
                break;
            case enemyState.attack:transform.LookAt(PlayerController.instance.transform.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer <= attackRange)
                    {
                        if (haveMultipleAttack)
                        {
                            anim.randomAttack();
                            attackCounter = timeBetweenAttacks;
                        }
                        else
                        {
                            anim.animationAttak();
                            attackCounter = timeBetweenAttacks;
                        }
                    }
                    else
                    {
                        currentState = enemyState.idle;
                        waitCounter = waitAtPoint;
                        agent.isStopped = false;
                    }
                }
                break;
        }
    }


}
