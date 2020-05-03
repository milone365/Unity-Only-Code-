using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = (target.transform.position + new Vector3(0, 0, 0))-transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion currentPos = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(currentPos, rotation, Time.deltaTime);
        transform.Translate(0, 0, 3 * Time.deltaTime);
    }
}
