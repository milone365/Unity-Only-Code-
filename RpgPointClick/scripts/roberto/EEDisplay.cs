using rpg.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EEDisplay : MonoBehaviour
{
    Fighter fighter;
    Health health=null;
    private void Awake()
    {
        fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        
    }
    private void Update()
    {
        health = fighter.getTarget();
        if (health == null)
        {
            GetComponent<Text>().text = "N/A";
        }
        else
        {
            GetComponent<Text>().text = string.Format("{0:0.0}%", health.GetPercentage());
        }
       
    }
}
