using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Transform startPoint;
    public Transform endPoint;
    public GameObject thisPlatform;
    public float movespeed = 3f;
    private Vector3 currerntTarget;
	// Use this for initialization
	void Start () {
        currerntTarget = endPoint.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        thisPlatform.transform.position = Vector3.MoveTowards(thisPlatform.transform.position, currerntTarget, movespeed * Time.deltaTime);
        if (thisPlatform.transform.position == endPoint.position)
        {
            currerntTarget = startPoint.position;        }

        if (thisPlatform.transform.position == startPoint.position)
        {
            currerntTarget = endPoint.position;
        }
        
	}
}
