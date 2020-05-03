using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fishController : MonoBehaviour
{
    
    public float distanceRun = 4f;
    public float speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float squareDistance = (transform.position - PlayerController.instance.transform.position).magnitude;
        float run = distanceRun * distanceRun;
        if (squareDistance < run)
        {
            Vector3 dirToPlayer = transform.position - PlayerController.instance.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(newPos.x,transform.position.y,newPos.z), speed);
        }
    }
}
