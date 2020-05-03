using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startLocalScale;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        startRot = this.transform.rotation;
        startLocalScale = this.transform.localScale;
        
    }
  
    public void ResetObject()
    {
        transform.rotation = startRot;
        transform.position = startPos;
        transform.localScale = startLocalScale;
        
    }
}
