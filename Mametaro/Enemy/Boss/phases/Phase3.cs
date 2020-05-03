using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Phase3 : Phase2
{
    public Phase3(NavMeshAgent ag, Animator an, EnemyStatus sta, Weapon w, Player p, Transform t,Boss b) : base(ag, an, sta, w, p,t,b)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("is phase 3");
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void Updating()
    {
        base.Updating();
    }
}
