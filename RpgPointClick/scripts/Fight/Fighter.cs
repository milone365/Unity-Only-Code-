using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rpg.Movement;
using rpg.Core;
using System;
using rpg.Stato;

namespace rpg.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        [SerializeField] float timeBetWheenAttacks = 1f;
        [SerializeField] float timeSinceLastAttack = Mathf.Infinity;
        // [SerializeField] float rotateSpeed =5;
        WeaponConfig currentWeapon = null;
        [SerializeField] Transform rightHand = null, leftHand = null;
        [SerializeField] string defaultWeaponName = "Unarmed";
        private void Start()
        {
            timeSinceLastAttack = 0;
            WeaponConfig wp = Resources.Load<WeaponConfig>(defaultWeaponName);
            EquipWeapon(wp);
        }

        public void EquipWeapon(WeaponConfig wp)
        {
            currentWeapon = wp;
            Animator anim = GetComponent<Animator>();
            wp.Spawn(rightHand, leftHand, anim);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.isdead()) { return; }
            if (!IsInrange())
            {
                GetComponent<Mover>().moveTo(target.transform.position, 1);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBeahviour();
            }
        }


        public bool canAttack(GameObject combat_target)
        {
            if (combat_target == null) { return false; }
            /*if (!GetComponent<Mover>().CanMoveTo(combat_target.transform.position))
            {
                return false;
            }*/
            Health targetToTest = combat_target.GetComponent<Health>();
            return targetToTest != null && !targetToTest.isdead();
        }
        private void AttackBeahviour()
        {

            if (timeSinceLastAttack > timeBetWheenAttacks)
            {
                triggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void triggerAttack()
        {
            transform.LookAt(target.transform);
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("atk");
        }

        private bool IsInrange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.getWeaponRange();
        }

        public void attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().startAction(this);
            target = combatTarget.GetComponent<Health>();

        }
        public void Cancel()
        {
            stopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void stopAttack()
        {
            GetComponent<Animator>().ResetTrigger("atk");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        void hit()
        {
            if (target == null) return;
            float damage = GetComponent<BaseStats>().GetStat(Stats.Damage);
            if (currentWeapon.haveProjectile())
            {
                currentWeapon.launchProjectile(rightHand, leftHand, target, gameObject, damage);
            }
            else
            {

                target.takeDamage(this.gameObject, damage);
            }

        }
        void Shoot()
        {
            hit();
        }
        public Health getTarget()
        {
            return target;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stats stat)
        {
            if (stat == Stats.Damage)
            {
                yield return currentWeapon.getDamage();
            }

        }


    }
}
