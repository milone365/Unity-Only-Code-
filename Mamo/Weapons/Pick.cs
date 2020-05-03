using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public MeshCollider thiscoll;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            thiscoll.enabled = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "destroy")
        {
            other.GetComponent<Endurance>().takedamage(10);
            Bag.instance.usePick();
            thiscoll.enabled = false;
            
        }
        if (other.tag == "enemy")
        {
            other.GetComponent<EnemyHealthManager>().hurtEnemy(3);
            Bag.instance.usePick();
            thiscoll.enabled = false;
        }
        
        
    }
}
