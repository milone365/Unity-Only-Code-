using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class forge : interactable
{
    
    public GameObject goldGear, gear;
    public override void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            base.interact(message);
            if (Bag.instance.iron >= 20)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (QuestManager.instance.quest[7].isActiveAndEnabled)
                    {
                        Instantiate(goldGear, Bag.instance.armSpawner.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Bag.instance.iron -= 10;
                        Instantiate(gear, Bag.instance.armSpawner.transform.position, Quaternion.identity);
                        
                    }
                    
                }
                
               
            }
            else
            {
                base.interact("you don't have iron");
            }
        }
    }
    public override void OnTriggerExit(Collider other)
    {
        base.deactiveDialog();
        
    }
}
