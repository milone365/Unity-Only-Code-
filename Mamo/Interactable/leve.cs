using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leve : interactable
{
    public GameObject handler;
    public Transform leveOn,doorOpen;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (Input.GetButtonDown("Fire1"))
        {
            handler.transform.rotation = leveOn.rotation;
            door.transform.rotation = doorOpen.rotation;
        }
    }
}
