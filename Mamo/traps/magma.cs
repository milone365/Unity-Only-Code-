using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magma : MonoBehaviour
{
    public float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            HealthManager.instace.burn(1);



        }
    }
         private void OnTriggerStay(Collider other)
         {
             if (other.tag == "Player")
             {
                 timer -= Time.deltaTime;
                 if (timer <= 0)
                 {
                     HealthManager.instace.burn(1);
                     timer = 2f;

                 }


             }

         }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                timer = 2f;
            }
        }
    } 
