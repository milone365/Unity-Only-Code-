using rpg.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    
    [SerializeField]
    public override void Move()
    {
        if(!target.isdead())
        transform.LookAt(getAimLocation());

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
  
}
