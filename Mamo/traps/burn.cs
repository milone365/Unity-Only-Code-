using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burn : MonoBehaviour
{
    public GameObject fire;
    private bool initiate,fireisActive;
    public float burningTime=3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initiate)
        {
            if (!fireisActive)
            {
                Instantiate(fire, transform.position, transform.rotation);
                fireisActive = true;
            }
            
            burningTime -= Time.deltaTime;
            if (burningTime <= 0)
            {
                Destroy(gameObject);
            }


        }
    }
    public void startToBurn()
    {
        initiate = true;
    }
}
