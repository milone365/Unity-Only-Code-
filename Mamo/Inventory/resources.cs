using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resources : Item
{

    public override void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Bag.instance.addResource(itemName, numberOfItems);
            Destroy(gameObject);
            Instantiate(particle, transform.position, transform.rotation);
        }
    }
}
