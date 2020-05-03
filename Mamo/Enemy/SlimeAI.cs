using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : EnemyAI
{
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        rb.GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        Move();
    }

}
