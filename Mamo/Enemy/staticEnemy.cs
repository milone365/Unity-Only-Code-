using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticEnemy : MonoBehaviour
{
    public GameObject bee;
    
    public enemyAnimationManager enAnimMan;
    [SerializeField]
    private float attackCounter;
    private float attackTime=1.5f;
    public float distanceToPlayer;
    public float attackrange=20f;
    // Start is called before the first frame update
    void Start()
    {
        enAnimMan = GetComponent<enemyAnimationManager>();
        attackCounter = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        if (activeBattle)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                enAnimMan.animationAttak();
                attackCounter = attackTime;
            }
        }
    }
    public bool activeBattle
    {
        get{ return distanceToPlayer <= attackrange; }
    }
    public void SpaWnBee()
    {
        Instantiate(bee, transform.position, transform.rotation);
    }
}
