using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "cardInfo/Build", fileName = "build")]
public class BuildCard : Card
{
    public float hp = 100;
    //残る時間
    public float lifeTime = 1.5f;
    //spawn delay
    public float spawnTime = 5;
    public Bullet bullet = null;
    public float DamageOnDeath = 0;
}
