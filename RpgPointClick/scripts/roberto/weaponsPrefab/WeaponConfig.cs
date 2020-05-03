using rpg.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon",menuName ="Rpg/Weapon",order =0)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] AnimatorOverrideController weaponOverride = null;
    [SerializeField] Weapon weaponPrefab = null;
    [SerializeField] float weaponDamage = 10, weaponRange = 2f;
    [SerializeField] bool RightHandWp=true;
    [SerializeField] Projectile projectile = null;
    const string weaponName = "weapon";

    public void Spawn(Transform rightHand,Transform leftHand,Animator anim)
    {
        destroyOldWeapon(rightHand, leftHand);
        if (weaponPrefab != null)
        {
            Weapon weapon= Instantiate(weaponPrefab, selectHand(rightHand,leftHand));
            weapon.gameObject.name = weaponName;
        }
        var overrideController = anim.runtimeAnimatorController as AnimatorOverrideController;
        if (weaponOverride != null)
        {
            anim.runtimeAnimatorController = weaponOverride;
        }
        else if (overrideController != null)
            {
                anim.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
           
    }

    private void destroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        Transform oldWeapon = rightHand.Find(weaponName);
        if (oldWeapon==null)
        {
            oldWeapon = leftHand.Find(weaponName);
        }
        if (oldWeapon == null) return;
        oldWeapon.name = "Destroyng";
        Destroy(oldWeapon.gameObject);
    }

    public float getDamage()
    {
        return weaponDamage;
    }
    public float getWeaponRange()
    {
        return weaponRange;
    }
    public bool haveProjectile()
    {
        return projectile != null;
    }
    public void launchProjectile(Transform rHand, Transform lHand, Health target,GameObject instigator,float calculatedamage)
    {
        Projectile projectileInstance = Instantiate(projectile, selectHand(rHand, lHand).position,Quaternion.identity);
        projectileInstance.setTarget(target,weaponDamage,instigator);
    }

    Transform selectHand(Transform rightHand, Transform leftHand)
    {
        Transform targetHand;
        if (RightHandWp) { targetHand = rightHand; }
        else
        {
            targetHand = leftHand;
        }
        return targetHand;
    }
    
}
