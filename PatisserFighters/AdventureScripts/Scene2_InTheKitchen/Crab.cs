using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour,IShotable
{
    [SerializeField]
    int hp = 3;
    bool death = false;
    [SerializeField]
    float force = 2000;
    
    //damage
    public void interact(Vector3 hitPos)
    {
        if (death) return;
        hp--;
        if (hp <= 0)
        {
            death = true;
            //push
            GetComponent<Rigidbody>().AddForce(transform.right * force);
            //effect
            EffectDirector.instance.EffectAndPopup(hitPos, "HELPERMELEE", 150);
        }
    }

}
