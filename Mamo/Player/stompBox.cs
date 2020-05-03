using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stompBox : MonoBehaviour
{
  public int bounceFloorForce = 50;
    
  
    public void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Bouncing")
        {
            PlayerController.instance.Bounce(bounceFloorForce);
        }


    }
}
