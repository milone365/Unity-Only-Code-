using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public static PlayerPunch instance;
    public GameObject firePunch, windpunch;
    public Transform punchEffectSpawner;
    public int damage = 1;
    private int damageStorage;
    public static int hitCounter = 0;
    private float comboTimeCounter;
    private float comboTimeRange = 1f;
    private bool canMakeCombo=false;
    public bool comboActive;
    public int force = 0;

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        force = GameManager.instace.force;
        if (GameManager.instace.comboActive)
        {
            comboActive = true;
        }
        comboTimeCounter = 0;
        damageStorage = damage;
      
    }
    public void Update()
    {
        if (canMakeCombo)
        {
            comboTimeCounter -= Time.deltaTime;
            
            if (comboTimeCounter <= 0)
            {
                canMakeCombo = false;
            }
        }
    }

    public void conut()
    {
        hitCounter++;
        comboTimeCounter = comboTimeRange;
        if (hitCounter > 3)
        {
            hitCounter = 0;
        }
        if (!canMakeCombo)
        {
            canMakeCombo = true;
        }
        if (hitCounter == 2)
        {
            Instantiate(windpunch, transform.position, transform.rotation);
            damage = 2;
            
        }else if (hitCounter == 3)
        {
            Instantiate(firePunch, transform.position, transform.rotation);
            damage = 4;
        }
        
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (force != GameManager.instace.force)
            {
                force = GameManager.instace.force;
            }
            if (comboActive)
            {
                conut();
                other.GetComponent<IEnemyHealth>().hurtEnemy(damage+force);
                damage = 1;
            }
            else
            {
                
                other.GetComponent<IEnemyHealth>().hurtEnemy(damage+force);
                
            }
            
          }

        if (other.tag == "robot")
        {
            other.GetComponent<RobotDamageButton>().getDamage();
        }
        else if (other.tag == "destroy")
        {
            other.GetComponent<Endurance>().takedamage(damage+force);
        }

    }
   
    
}

