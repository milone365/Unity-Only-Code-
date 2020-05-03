using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationSetter : MonoBehaviour
{
    Quaternion startRot = new Quaternion();
    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation != startRot)
        {
            transform.rotation = startRot;
        }
    }
}
