using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{    //最小と最大の値を決める
    [SerializeField] private float minValue=-5;
    [SerializeField] private float MaxValue=5;
    //プレーヤーへ向かう
    [SerializeField] private float moveZ=-0.2f;
    float movex;
    float movey; 

    // Start is called before the first frame update
    void Start()
    {
        //ｘとｙ座標をランダムに決める
        movex = Random.Range(minValue, MaxValue);
        movey = Random.Range(minValue, MaxValue);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movex, movey, moveZ);
    }
}
