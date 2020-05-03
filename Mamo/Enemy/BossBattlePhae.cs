using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattlePhae : MonoBehaviour
{
    public GameObject gameEntrace;
    public EnemyAbilityes ability;
    public GameObject bossActivator;
    public BossHealth bh;
    public GameObject objectToDrop;
    public GameObject deathEffect;
    public enum bossPhases { intro,phase1,phase2,phase3,end}
    [SerializeField]
    private bossPhases currentPhase = bossPhases.intro;
    public float vulnerableTime = 5f;
    public float attackRange=15f;
    public float moveSpeed=5f;
    float vulnerableCounter;
    public enemyAnimationManager anim;
    public bool inerme = false, coindrop;
    private bool startBattle;
    private bool isDead;
    public GameObject lifeBar;
    public string bossName;
    public Text nameText;
    public bool isMovingBoss;
    
    
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = bossName;
        
        startBattle = false;
        isDead = false;
        vulnerableCounter = vulnerableTime;
        ability = GetComponent<EnemyAbilityes>();
        bh = GetComponent<BossHealth>();
        anim = GetComponent<enemyAnimationManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        if (isMovingBoss)
        {
            if (!startBattle)
            {
                var distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
                if (distanceToPlayer <= attackRange)
                {
                    startBattle = true;
                    currentPhase = bossPhases.phase1;
                    checkState();
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
                }
            }
        }
       
        if (GameManager.instace.respawning)
        {
            lifeBar.SetActive(false);
            currentPhase = bossPhases.intro;
            anim.animationPhase(0); //intro
            gameEntrace.SetActive(true);
            bossActivator.SetActive(true);
            GameManager.instace.respawning = false;
            startBattle = false;
            gameObject.SetActive(false);
        }
        if (inerme)
        {

            anim.animationPhase(0); //idle
            vulnerableCounter -= Time.deltaTime;
            if (vulnerableCounter <= 0)
            {
               
                bh.invincible = true;
                inerme = false;
                vulnerableCounter = vulnerableTime;
                checkState();
            }
        }
       
      
    }
    public void goToNextPhase()
    {
        if (currentPhase != bossPhases.end)
        {
            
            anim.TriggerAnim("hurt");
            currentPhase++;
           

            switch (currentPhase)
            {
                case bossPhases.phase1: ability.activePhase1(); break;
                case bossPhases.phase2: ability.activePhase2();break;
                case bossPhases.phase3: ability.activePhase3(); break;
                case bossPhases.end:
                    StartCoroutine(endBattle()); break;
            }
       }
  }
    
    public void checkState()
    {
        switch (currentPhase)
        {
            case bossPhases.phase1:
                startBattle = true;
                anim.animationPhase(1); break;
            case bossPhases.phase2:

                anim.animationPhase(2); break;
            case bossPhases.phase3:

                anim.animationPhase(3); break;
            case bossPhases.end:
                
                
                StartCoroutine(endBattle()); break;
        }
    }
    private IEnumerator endBattle()
    {
        anim.animationPhase(4);
        yield return new WaitForSeconds(2f);
        gameEntrace.SetActive(true);
       Instantiate(deathEffect, transform.position, transform.rotation);
        isDead = true;
        lifeBar.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (isDead)
        {
            if (coindrop)
            {
                for (int i = 0; i <= 4; i++)
                {

                    Instantiate(objectToDrop, new Vector3(transform.position.x + i, transform.position.y, transform.position.z + i), transform.rotation);
                    Instantiate(objectToDrop, new Vector3(transform.position.x + i, transform.position.y, transform.position.z - i), transform.rotation);
                    Instantiate(objectToDrop, new Vector3(transform.position.x - i, transform.position.y, transform.position.z + i), transform.rotation);
                    Instantiate(objectToDrop, new Vector3(transform.position.x - i, transform.position.y, transform.position.z - i), transform.rotation);
                }
            }
            else
            {
                Instantiate(objectToDrop, Bag.instance.armSpawner.transform.position, Quaternion.identity);


            }
        }
        startBattle = false;
    }
    private void OnEnable()
    {
        lifeBar.SetActive(true);
    }
}
