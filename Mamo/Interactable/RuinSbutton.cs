using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinSbutton :interactable
{
    public Animator anim;
    public Collider coll;
    public GameObject objecToActive;
    private bool objIsNotActive=false;
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
    }


    public override void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            anim.SetBool("move", true);
            coll.enabled = false;
            if (!objIsNotActive)
            {
                objecToActive.SetActive(true);
                objIsNotActive = true;
            }
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        
    }
}
