using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunderAreaPlayer : MonoBehaviour
{
    public GameObject thunderPrefab;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"|| other.tag == "minion"|| other.tag == "Player")
        {
            Instantiate(thunderPrefab, other.transform.position, other.transform.rotation);
        }
        
        
    }
}
