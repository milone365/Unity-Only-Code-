using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using rpg.Core;
using rpg.Stato;
using System;
using UnityEngine.Events;
namespace rpg.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float regenerationPercentage = 70;
        bool isDead;
       public bool isdead () { return isDead; }
        float currenthealth =-1f;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += restoreHealth;
            if (currenthealth < 0)
            {
                currenthealth = GetComponent<BaseStats>().GetStat(Stats.health);
            }
            
        }

        private void restoreHealth()
        {
            float regenHealthpoints = GetComponent<BaseStats>().GetStat(Stats.health)*
                (regenerationPercentage/100);
            currenthealth = Mathf.Max(currenthealth, regenHealthpoints);
        }

        public float GetHealthPoints()
        {
            return currenthealth;
        }
        public float maxHelth()
        {
          return  GetComponent<BaseStats>().GetStat(Stats.health);
        }
        public void takeDamage(GameObject instigator, float damage)
        {
            currenthealth = Mathf.Max(currenthealth - damage, 0);
            DamageText dmtext = GetComponentInChildren<DamageText>();
            if (dmtext != null)
            {
                dmtext.onDamageEnter(damage);
            }
            if (currenthealth <= 0)
            {
                if (isDead) return;
                die();
                awardExperience(instigator);
            }
        }

        private void awardExperience(GameObject instigator)
        {
            experience exp = instigator.GetComponent<experience>();
            if (exp == null) return;
            exp.GainExperience(GetComponent<BaseStats>().GetStat(Stats.experienceReward));
        }

        void die()
        {
            isDead = true;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().cancelCurrentAction();
        }
        public float GetPercentage()
        {
            return 100 * (currenthealth / GetComponent<BaseStats>().GetStat(Stats.health));
        }

        public void restoreState(object state)
        {
            currenthealth = (float)state;
            if (currenthealth <= 0)
            {
                die();
            }
        }
    }
   
}