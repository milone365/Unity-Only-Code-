using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthManager : MonoBehaviour,IEnemyHealth
{
    public bool haveAbility,dieAnimation;
    public int stunProbability = 35;
    public string abilityname;
    public Transform spawnPoint;
    public Image healthBar;
    public int hurtanimationNumber;
    public bool haveDrop,haveDropRate;
    public GameObject[] drop;
    public int dropRate;
    
    
    public int heath;
    public int startingHealth;
    public string enemyName;
    public int experience;
    
    
    
    //public bool _hurt;
    private float cursedTimer=3f;
    public float cursedCounter;
    
    public GameObject deatheffect;
    public GameObject[] particle;
    public enemyAnimationManager animManager;
    private bool isCursed;
    private int cuurseDamage;

    private void Start()
    {
        cursedCounter = cursedTimer;
        
        heath = startingHealth;
        
      
                healthBar.GetComponent<Image>();
                healthBar.fillAmount = heath;
 }

    private void Update()
    {
       
        if (isCursed)
        {
            cursedCounter -= Time.deltaTime;
            if (cursedCounter <= 0)
            {
                isCursed = false;
                cursedCounter = cursedTimer;
                hurtEnemy(cuurseDamage);
            }
        }
      
    }

    public void giveCurse(int damage)
    {
        isCursed = true;
        cuurseDamage = damage;
    }
    public void cure(int amount)
    {
        heath += amount;
        if (heath > startingHealth)
        {
            heath = startingHealth;

        }
       
     healthBar.fillAmount = heath / 100f;
        
    }

    public void restoreHealt()
    {
        heath = startingHealth; 
       healthBar.fillAmount = heath / 100f;
  }

    public void hurtEnemy(int damageToGive)
    {
        
            heath -= damageToGive;
            int randamnumber = Random.Range(4, 6);
            AudioManager.instance.playSFX(randamnumber);
            
                    healthBar.fillAmount = heath / 100f;
                
               int randValor = Random.Range(1, 100);
                if (randValor <= stunProbability&& heath > 0)
                {
                  StartCoroutine(hurtCo());
                 }
                    
                
                if (heath <= 0)
                {
             
                    QuestManager.instance.EnemyKilled = enemyName;

                    if (!CharStatus.instance.reachMaxLevel)
                    {
                        CharStatus.instance.addExperience(experience);
                    }
                    Instantiate(deatheffect, spawnPoint.position, spawnPoint.rotation);
                    if (haveDrop)
                    {
                        if (haveDropRate)
                        {
                            int randomnum =Random.Range (0, drop.Length);
                            int rand2 = Random.Range(0, 100);
                           if (rand2 >= dropRate)
                           {
                              Instantiate(drop[randomnum], spawnPoint.position, spawnPoint.rotation);
                            }
                           
                        }
                        else
                        {
                            Instantiate(drop[0], spawnPoint.position, spawnPoint.rotation);
                            
                        }
                    }

                    if (dieAnimation)
                    {
                        StartCoroutine(dieCo());
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }} }
        
    

    public IEnumerator dieCo()
    {
        animManager.TriggerAnim("die");
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    public  IEnumerator hurtCo()
    {

       // animManager.animationMove(false);
        
        animManager.animationHurt(1);
        int ran2 = Random.Range(0, particle.Length);
        Instantiate(particle[ran2], transform.position, transform.transform.rotation);
        yield return new WaitForSeconds(1f);
        animManager.animationHurt(0);
        yield return new WaitForSeconds(1f);
        if (haveAbility)
        {
            EnemyAbilityes ability = GetComponent<EnemyAbilityes>();
            ability.activeAbility(abilityname);
        }
        
    }

}

