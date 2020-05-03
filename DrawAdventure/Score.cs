using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    int score = 100;
    bool isTrigged = false;
    
    //スコア範囲
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isTrigged) return;
            isTrigged = true;
            //スコアアップ
            GameManager.instance.AddScore(score);
        }
    }
}
