using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public static Pivot instace;

    public Quaternion pivotStartRotation;
    private void Awake()
    {
        instace = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pivotStartRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
