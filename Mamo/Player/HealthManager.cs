using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    private int resistance = 0;
    private bool regeneration = false;
    private float regenerationCounter;
    private float regenerationTime = 30;
    public static HealthManager instace;
    public GameObject ShieldGameObject;
    public int MaxHealth = 10;
    public int currentHealth;
    public GameObject sprite, particle;
    public int shieldEndurance = 0;
    public bool shieldActive = false, bubbleActive = false;
    private float invincibilityLenght = 2f;
    public float Invincibilitycounter;
    public GameObject shieldImpactEffect, explodeShield;
    [SerializeField]
    public float shieldTime = 120;
    public bool iceShield;
    [SerializeField]
    private int iceShieldEndurance;
    public GameObject iceShieldPrefab;
    public int sfx;

    private void Awake()
    {
        instace = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bubbleActive = false;
        regenerationCounter = regenerationTime;

        shieldActive = false;

        if (PlayerPrefs.HasKey("save") && PlayerPrefs.GetInt("save") == 1)
        {


            MaxHealth = PlayerPrefs.GetInt("hp");
            shieldEndurance = PlayerPrefs.GetInt("shield");


        }

        if (PlayerPrefs.HasKey("regenerationActive") && PlayerPrefs.GetInt("regenerationActive") == 1)
        {
            regeneration = true;

        }
        else
        {
            regeneration = false;
        }
        currentHealth = MaxHealth;
        UIManager.instance.UpdateUI();
    }
    public void ActiveResistance()
    {
        resistance++;
    }
    public void activeRegeration()
    {
        regeneration = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (regeneration)
        {
            if (regenerationCounter > 0)
            {
                regenerationCounter -= Time.deltaTime;
                if (regenerationCounter <= 0)
                {
                    currentHealth++;
                    if (currentHealth < MaxHealth)
                    {
                        regenerationCounter = regenerationTime;
                    }
                }
            }
        }

        if (iceShield)
        {
            shieldTime -= Time.deltaTime;
            if (shieldTime <= 0)
            {
                iceShield = false;
                iceShieldPrefab.SetActive(false);
                iceShieldEndurance = 0;
                shieldTime = 120;
            }
        }
    
        
        if (Invincibilitycounter > 0)
        {
            Invincibilitycounter -= Time.deltaTime;

            if (Mathf.Floor(Invincibilitycounter * 5f) % 2 == 0)
            {
                sprite.SetActive(true);

            }
            else
            {
                sprite.SetActive(false);
            }
            if (Invincibilitycounter <= 0)
            {
                sprite.SetActive(true);
            }
        }
        
    }
    public void activeShield()
    {
        if (shieldEndurance > 0)
        {
            shieldActive = true;
            ShieldGameObject.SetActive(true);
        }
        
    }
    public void ActiveBUBBLE()
    {
        bubbleActive = true;
    }
    public void deactiveBubble()
    {
        bubbleActive = false;
    }
    public void deactiveShield()
    {
        shieldActive = false;
        ShieldGameObject.SetActive(false);
    }
    public void burn(int dmg)
    {
        if (!bubbleActive)
        {
            TakeDamage(dmg);
        }
    }
       
    public void giveShield(int amount)
    {
        shieldEndurance += amount;
    }
    public void giveICEShield(int amount)
    {
        iceShieldEndurance += amount;
        iceShield = true;
        shieldTime = 120;
    }

    public void TakeDamage(int damageToTake)
    {
        if (Invincibilitycounter<=0)
        {
            if (shieldActive||iceShield)
            {
                if (shieldActive)
                {
                    sottractShield(damageToTake);
                }
                else
                {
                    sottractICEShield(damageToTake);
                }
                
            }
            else
            {
                damageToTake -= resistance;
                currentHealth -= damageToTake;
                AudioManager.instance.playSFX(sfx);
            }
                
           if (currentHealth <= 0)
            {
                currentHealth = 0;
                Instantiate(particle, transform.position, transform.rotation);
                GameManager.instace.Respawn();
            }
            else
            {
                if (!shieldActive)
                {
                    PlayerController.instance.KnockBack();
                    Invincibilitycounter = invincibilityLenght;
                }
                
            }
            if (regeneration)
            {
                regenerationCounter = regenerationTime;
            }
            UIManager.instance.UpdateUI();

        }
        
    }
    public void SaveGame()
    {
        PlayerPrefs.SetInt("hp", MaxHealth);
        PlayerPrefs.SetInt("shield", shieldEndurance);
        if (regeneration)
        {
            PlayerPrefs.SetInt("regenerationActive", 1);
        }
    }
    public void instantKill()
    {
        currentHealth = 0;
        UIManager.instance.UpdateUI();
        Instantiate(particle, transform.position, transform.rotation);
        GameManager.instace.Respawn();
    }
    public void upGradeMaxHp()
    {
        MaxHealth++;
    }
    public void HealPlayer(int healing)
    {
        currentHealth += healing;
        if (currentHealth >= MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        UIManager.instance.UpdateUI();
    }
    public void restoreHealth()
    {
        currentHealth = MaxHealth;
        UIManager.instance.UpdateUI();
    }


    void sottractICEShield(int damagetoTake)
    {
        if (damagetoTake > iceShieldEndurance)
        {

            int rest = damagetoTake - iceShieldEndurance;
            shieldEndurance = 0;
            iceShield = false;
            Instantiate(explodeShield, transform.position, transform.rotation);
            iceShieldPrefab.SetActive(false);
            currentHealth -= rest;


        }
        else
        {
            iceShieldEndurance -= damagetoTake;
            Instantiate(shieldImpactEffect, transform.position, transform.rotation);
            if (iceShieldEndurance <= 0)
            {
                iceShield = false;
                Instantiate(explodeShield, transform.position, transform.rotation);
                iceShieldPrefab.SetActive(false);
            }
        }
    }


    void sottractShield(int damagetoTake)
    {
        if (damagetoTake > shieldEndurance)
        {

            int rest = damagetoTake - shieldEndurance;
            shieldEndurance = 0;
            Bag.instance.ammo = 0;
            shieldActive = false;
            Instantiate(explodeShield, transform.position, transform.rotation);
            ShieldGameObject.SetActive(false);
            currentHealth -= rest;


        }
        else
        {
            shieldEndurance -= damagetoTake;
            Bag.instance.ammo = shieldEndurance;
            Instantiate(shieldImpactEffect, transform.position, transform.rotation);
            if (shieldEndurance <= 0)
            {
                shieldActive = false;
                Instantiate(explodeShield, transform.position, transform.rotation);
                ShieldGameObject.SetActive(false);
            }
        }
    }
}
