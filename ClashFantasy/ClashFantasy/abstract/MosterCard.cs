using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//scriptable object
[CreateAssetMenu(menuName ="cardInfo/Monster", fileName = "monster")]
public class MosterCard : Card
{

    public float hp = 100;
    public float damage = 100;
    // attack delay
    public float hitSpeed=1.5f;
    public Speed speed;
    //攻撃の為の距離
    public float range = 5.5f;
    //カ－度種類土、飛、建物
    public troupType type;
    //範囲ダメージ
    public bool isAreaDamage = false;
    public float A_bulletRange = 2.5f;
    //死んだら、敵にダメージを付ける
    public float DamageOnDeath = 0;
}
//スピード
public enum Speed
{
    slow=30,
    medium=60,
    fast=90,
    veryFast=120
}
//団タイプ
public enum troupType
{
    build,
    ground,
    fly
}
//目標タイプ
public enum targetType
{
    onlyBuild,
    onlyGround,
    All
}