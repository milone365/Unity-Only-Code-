using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUpPlayer : MonoBehaviour
{
    
    public float airForce=6f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            PlayerController.instance.movedirection.y = airForce;
        }
    }
   /* private void OnTriggerExit(Collider other)
    {
        PlayerController.instance.movedirection.y = ;
    }*/



}
