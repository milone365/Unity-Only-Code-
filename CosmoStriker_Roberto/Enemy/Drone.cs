using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealthManager))]
public class Drone : CorvetteMovement
{

    [SerializeField]
    GameObject bullet;
    [SerializeField] float fireDelay=2f;
    float fireCounter;
    [SerializeField] Transform spawnpoint;
   
    private void Start()
    {
        Initialize();
        fireCounter = fireDelay;
    }
    private void Update()
    {
        Move();
        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0)
        {
            fireCounter = fireDelay;
            if(transform.position.z>PlayerController.instance.transform.position.z)
            Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
        }
        if (PlayerController.instance != null)
        {
            transform.LookAt(PlayerController.instance.transform, Vector3.up);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
           
        }
        
    }

}
