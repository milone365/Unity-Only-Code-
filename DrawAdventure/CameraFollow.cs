using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // プレーヤーのｙ座標に沿って動く
    void Update()
    {
        if (player == null) return;
        transform.position = new Vector3(transform.position.x, player.transform.position.y,transform.position.z);
    }
}
