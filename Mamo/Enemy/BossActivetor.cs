using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivetor : MonoBehaviour
{
    
    public GameObject entrace;
    public GameObject boss;

   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boss.SetActive(true);
            entrace.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    public void activeEntrace()
    {
        entrace.SetActive(true);
    }
}
