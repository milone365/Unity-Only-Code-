using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrollPoints;
    public int currentPatrollPoint;
    public NavMeshAgent agent;
    public Animator anim;

    public enum AiState { idle, patroll, chasing, attack };
    public AiState currentState;
    public float waitAtPoint = 2f;
    public float waitCounter;
    public float chaseRange;
    public float attackRange = 1f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;
    public float attackRotationVarible;

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
            case AiState.idle:
                anim.SetBool("isMoving", false);
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AiState.patroll;
                    agent.SetDestination(patrollPoints[currentPatrollPoint].position);
                }
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AiState.chasing;
                    anim.SetBool("isMoving", true);
                }
                break;
            case AiState.patroll:
                if (agent.remainingDistance <= .2f)
                {
                    currentPatrollPoint++;
                    if (currentPatrollPoint >= patrollPoints.Length)
                    {
                        currentPatrollPoint = 0;
                    }
                    currentState = AiState.idle;
                    waitCounter = waitAtPoint;
                }
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AiState.chasing;
                }
                anim.SetBool("isMoving", true);
                break;

            case AiState.chasing:
                agent.SetDestination(PlayerController.instance.transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    currentState = AiState.attack;
                    anim.SetTrigger("Attack");
                    anim.SetBool("isMoving", false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    attackCounter = timeBetweenAttacks;
                }
                if (distanceToPlayer > chaseRange)
                {
                    currentState = AiState.idle;
                    waitCounter = waitAtPoint;
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
                break;
            case AiState.attack:
                transform.LookAt(PlayerController.instance.transform, Vector3.down);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y+attackRotationVarible, 0f);
                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer < attackRange)
                    {
                        anim.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;

                    }
                    else
                    {
                        currentState = AiState.idle;
                        waitCounter = waitAtPoint;
                        agent.isStopped = false;
                    }
                }
                break;
        }
    }
}
