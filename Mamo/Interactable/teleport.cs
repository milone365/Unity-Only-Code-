using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : interactable
{
    public Transform otherPoint;
    private string tel = "press action to teleport";
    public bool teleportActive;
    private void Update()
    {
        if (teleportActive)
        {
            if(Input.GetButtonDown("Fire1")){
                PlayerController.instance.transform.position = otherPoint.position;
                teleportActive = false;
            }
        }
    }

    public override void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {

            
            teleportActive = true;

        }

    }
    public override void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {


            teleportActive = false;

        }
    }
}

