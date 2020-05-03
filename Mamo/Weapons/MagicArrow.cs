using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : HurtCollider
{
    private int intellect;
    // Start is called before the first frame update
    void Start()
    {
        intellect = GameManager.instace.intellect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (haveARandomicFactor)
            {
                int newRand = Random.Range(1, randomicfactor);
                randomicfactor = newRand;
            }
            if (intellect != GameManager.instace.intellect)
            {
                intellect = GameManager.instace.intellect;
            }
            other.GetComponent<EnemyHealthManager>().hurtEnemy(intellect + randomicfactor);
        }
    }
}
