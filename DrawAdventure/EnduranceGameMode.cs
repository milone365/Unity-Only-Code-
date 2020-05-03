using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceGameMode 
{
    Transform player;
    //移動カウンター
    float travelDistance = 0;
    public Vector3 oldPosition;
    float spawnHazardDistance = 4;
    int hazardlenght = 0;
    
    //コンストラクタ
    public EnduranceGameMode(Transform p,int lenght)
    {
        player = p;
        hazardlenght = lenght;
        oldPosition = player.transform.position;
    }
    //決めたら距離を乗り越えた場合、ランダムで新たなギミックが作られている
    public void GameUpdate()
    {
        if (player == null) return;
        travelDistance = oldPosition.y - player.position.y;
        if (travelDistance > spawnHazardDistance)
        {
            //元のポジションはプレーヤーの現在ポジションになる
            oldPosition = player.position;
            //移動カウンターをリセットする
            travelDistance = 0;
            Vector2 spawnPos = new Vector2(0, oldPosition.y - 10);
            int rnd = Random.Range(0, hazardlenght);
            float rng = Random.Range(4, 8);
            spawnHazardDistance = rng;
            GameManager.instance.spawnHazard(rnd, spawnPos);
        }
    }
}
