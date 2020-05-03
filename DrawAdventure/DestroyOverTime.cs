using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    //五秒経ったら削除
    public float lifetime = 5;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
