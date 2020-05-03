using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scriptable object
[CreateAssetMenu(menuName = "cardInfo/Spell", fileName = "Spell")]
public class SpellCard : Card
{
    //ダメージとタワーダメージ
    [SerializeField]
    public float areaDamage = 500, crownTowerDamage = 200;
    //攻撃の範囲
    [SerializeField]
    public float radius = 2.5f;
   
}
