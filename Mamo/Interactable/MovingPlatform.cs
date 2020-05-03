using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform point1, point2;
        private Transform nextPose;
    public float velocity = 5f;
    // Start is called before the first frame update
    void Start()
    {
        nextPose = point2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPose.position, velocity * Time.deltaTime);
        if (transform.position == nextPose.position)
        {
            changePos();
        }
    
        
        
    }
    void changePos()
    {
        if (nextPose == point1)
        {
            nextPose = point2;
        }
        else
        {
            nextPose = point1;
        }
    }
}
