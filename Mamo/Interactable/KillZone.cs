using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
  
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            HealthManager.instace.instantKill();
        }

        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
        }
    }
}

    