using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Bag.instance.armSpawner.rotation;
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward * speed * 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
