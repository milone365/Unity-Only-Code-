using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : Item
{
    
    public int questNumber;
   
    

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
                
                QuestManager.instance.itemCollected = itemName;
                Destroy(gameObject);
            }
        }
    }

