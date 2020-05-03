using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    //parameterｓ
    public string CardName = "";
    public int cost = 3;
    //画像
    public Sprite image = null;
    //有効なターゲット
    public targetType targets;
    //キャラクター
    [SerializeField]
    public GameObject[] prefab;
    //
    public SpellCard spellCard;
}

