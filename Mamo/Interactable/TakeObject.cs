using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour
{
    
    private Transform launchPoint;
    public Rigidbody rb;
    public float speed=2f;
    public bool attaked=false;
    //public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (!attaked)
        {

        }*/
        if (attaked&&Input.GetButtonDown("Fire1"))
        {
           
            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.velocity = PlayerController.instance.transform.forward * speed*Time.deltaTime*19f;
            attaked = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&!attaked)
        {
            attaked = true;
            rb.isKinematic = true;
            rb.useGravity = false;
            launchPoint = PlayerController.instance.plyerHand;
            transform.position = launchPoint.position;

            this.transform.SetParent(launchPoint);
            transform.rotation = PlayerController.instance.transform.rotation;
            
        }
        
    }
}
