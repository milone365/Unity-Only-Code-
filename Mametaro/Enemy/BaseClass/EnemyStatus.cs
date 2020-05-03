using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyStatus/Stat",fileName ="EnemyInfo")]
public class EnemyStatus :ScriptableObject
{
    public float attackDelay = 1;
    public float attackCounter = 1;
    public float speed = 10;
    public float attackRange = 10;
    public float chasingRange = 25;
    public float waiting = 3;
    public float waitingCounter = 3;

}
