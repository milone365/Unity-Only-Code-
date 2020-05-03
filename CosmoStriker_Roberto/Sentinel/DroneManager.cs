using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
   
    public Vector3 Limits = new Vector3(5, 5, 5);
    public Transform center { get { return this.gameObject.transform; } }
 
}
