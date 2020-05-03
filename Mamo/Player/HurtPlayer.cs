using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int DamageToGive;
    public bool haveRandomicFactor = false;
    public int randomicFactor = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            
        {
            if (haveRandomicFactor)
            {
                int newRand = Random.Range(0, randomicFactor);
                randomicFactor = newRand;
            }
            HealthManager.instace.TakeDamage(DamageToGive+randomicFactor);
            
        }else if (other.tag == "minion")
        {
            if (haveRandomicFactor)
            {
                int newRand = Random.Range(0, randomicFactor);
                randomicFactor = newRand;
            }
            other.GetComponent<IMinion>().takeDamage(DamageToGive + randomicFactor);
        }
    }
}
