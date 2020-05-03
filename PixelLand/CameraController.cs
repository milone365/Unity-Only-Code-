using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    private Vector3 targetPosition;
    public float followAHead;
    public float smoothing;
    public bool followTarget;
    // Use this for initialization
    void Start()
    {
        followTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

            if (target.transform.localScale.x > 0f)
            {
                targetPosition = new Vector3(targetPosition.x + followAHead, targetPosition.y, targetPosition.z);
            }
            else { targetPosition = new Vector3(targetPosition.x - followAHead, targetPosition.y, targetPosition.z); }

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
    

