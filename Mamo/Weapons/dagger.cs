using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dagger : MonoBehaviour
{
    public GameObject hit;
    public Rigidbody rb;
    public float speed = 500f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = PlayerController.instance.transform.rotation;
        if (Bag.instance.crossHairIsActive)
        {
            RaycastHit hit;
            Ray newray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(newray, out hit))
            {
                Vector3 direction = hit.point - transform.position;
                if (hit.distance < 50)
                {
                    rb.velocity = (direction * speed * 3);
                }
                else { rb.velocity = (transform.forward * speed * 3); }

            }
            else
            {
                rb.velocity = (transform.forward * speed * 3);
            }
        }
        else
        {
            rb.velocity = (transform.forward * speed * 3);
        }



    }
    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hit, transform.position, transform.rotation);
        Destroy(gameObject);
    }

   

}
