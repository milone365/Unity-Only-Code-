using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItem : Item
{
    public bool isMagic,isLifeRestore,egg;
    public bool isLifeBonus, isManaBonus, isResistanceRing,isLegendaryWeapon;
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            if (isMagic)
            {
                GameManager.instace.addIntellect();
            }
            if (isLifeBonus)
            {
                HealthManager.instace.upGradeMaxHp();
            }
            if (isManaBonus)
            {
                GameManager.instace.upGradeManaPoint();
            }
            if (isLifeRestore)
            {
                HealthManager.instace.activeRegeration();
            }
            if (isResistanceRing)
            {
                HealthManager.instace.ActiveResistance();
            }
            if (isLegendaryWeapon)
            {
                GameManager.instace.haveLegendaryWeapons();
            }
            if (egg)
            {
                GameManager.instace.haveEgg = true;
            }
            UIManager.instance.UpdateUI();
        }
    }


}
