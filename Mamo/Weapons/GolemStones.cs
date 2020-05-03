using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStones : MonoBehaviour
{
    public Rigidbody rb;
    private bool spawnRock;
    public GameObject stones;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
         Vector3 direction = PlayerController.instance.transform.position-transform.position;
         rb.velocity = Vector3.MoveTowards(transform.position, direction, Mathf.Infinity);

        
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!spawnRock)
        {
            spawnRock = true;
            Instantiate(stones, transform.position, transform.rotation);
        }
        

       
    }
}
