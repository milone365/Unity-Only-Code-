using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public GameObject checkpointOn, checkPointOff;
   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instace.setCheckPoint(transform.position);

            Checkpoint[] allCp = FindObjectsOfType<Checkpoint>();
            for(int i = 0; i < allCp.Length; i++)
            {
                allCp[i].checkPointOff.SetActive(true);
                allCp[i].checkpointOn.SetActive(false);
            }
            checkpointOn.SetActive(true);
            checkPointOff.SetActive(false);

            
        }
    }
   
}
