using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealthManager))]
public class Gatling : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnpoint;
    [SerializeField]
    Transform rotationPoint;
    [SerializeField]
    Transform Torret;
    [SerializeField]
    float velocity = 10;
   public bool fire;
    Transform player;
    [SerializeField]
    float offset=60;
    [SerializeField]
    float fireDelay=0.25f;
    float fireCounter;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player!=null){
            Torret.LookAt(player, Vector3.up);
            Torret.rotation = Quaternion.Euler(0, Torret.eulerAngles.y+offset, 0);
        }
        if (fire)
        {
            rotationPoint.Rotate(velocity, 0, 0);
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireDelay;
                Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
            }
           
        }
       
    }
}
