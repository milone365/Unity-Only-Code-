using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCamera : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance != null)
        {
            target = PlayerController.instance.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.rotation = Quaternion.Euler(90, target.eulerAngles.y, 0);
        }
    }
}
