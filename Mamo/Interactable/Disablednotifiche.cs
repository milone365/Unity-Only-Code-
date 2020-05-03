using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablednotifiche : MonoBehaviour
{
    [SerializeField]
    SkeletonsActivetor skel;
    private void Start()
    {
       
    }
    private void OnDisable()
    {
        skel.killed();
    }

}
