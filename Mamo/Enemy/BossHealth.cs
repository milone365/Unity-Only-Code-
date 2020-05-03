using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour,IEnemyHealth
{
    public bool goingTotheNextPhase;
    public GameObject shield;
    private float invincibleCounter;
    private float invincibleLenght = 15f;
    public int heath;
    public int startingHealth;
    public bool invincible = false;

    public void hurtEnemy(int damageToGive)
    {
        if (!invincible)
        {
            heath -= damageToGive;

            if (heath <= 0)
            {
              heath = startingHealth;

                if (!goingTotheNextPhase)
                {
                    goingTotheNextPhase = true;
                    GetComponent<BossBattlePhae>().goToNextPhase();

                }
            }

            UIManager.instance.updateEnemyStatus(heath);

        }
    }

        // Start is called before the first frame update
        void Start()
    {
        goingTotheNextPhase = false;
        invincibleCounter = invincibleLenght;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            shield.SetActive(true);
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                invincible = false;
                shield.SetActive(false);
                GetComponent<BossBattlePhae>().inerme = true;
                invincibleCounter = invincibleLenght;
            }
        }
    }
}
