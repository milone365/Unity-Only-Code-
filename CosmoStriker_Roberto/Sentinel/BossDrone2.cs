using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrone2 : MonoBehaviour
{

    BossMove boss;
    float x = 0, y = 0, z = 0;
    bool start;
    float timer = 1;
    Vector3 dir = new Vector3();
    [SerializeField]
    float speed = 1f;
    Transform player;
    [SerializeField]
    float attackDistance = 5;
    [SerializeField]
    GameObject bullet;
    [SerializeField] float fireDelay = 2f;
    float fireCounter;
    [SerializeField] Transform spawnpoint;
    GameObject orbit;
    [SerializeField]
    float BossDist = 5;
    DroneManager dronemanager;
    bool turning;
    Vector3 center;
    int rand=0;
    // Start is called before the first frame update
    void Start()
    {
        fireCounter = fireDelay;
        x = Random.Range(-BossDist, BossDist);
        y = Random.Range(-BossDist, BossDist);


        while (x > -2 && x < 2)
        {
            x = Random.Range(-BossDist, BossDist);
        }
        while (y > -2 && y < 2)
        {
            y = Random.Range(-BossDist, BossDist);
        }

        dronemanager = FindObjectOfType<DroneManager>();
        start = true;
        player = PlayerController.instance.transform;
        center = dronemanager.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            initialMove();
        }
        else
        {
            center = dronemanager.transform.position;
            Bounds b = new Bounds(center, dronemanager.Limits);
            if (!b.Contains(transform.position))
            {

                turning = true;
                dir = center - transform.position;
                
            }
            if (turning)
            {
                goToCenter();
            }
            else
            {
                if (timer > 0)
                {
                    Vector3 lookRotation = player.position;
                    dir = lookRotation - transform.position;
                    
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 1.2f * Time.deltaTime);
                    if (rand > 30)
                    {
                        if(dir.magnitude > attackDistance )
                          transform.Translate(0, 0, speed * Time.deltaTime);
                    }
                    else
                    {
                        dir = new Vector3(x, y, z);
                        transform.Translate(dir.normalized * Time.deltaTime * speed);
                    }
                        
                }
                else
                {
                    timer = 5;
                    changeMove();
                }
                

            }
                
           }

        fire();
   }


 
    public void initialMove()
    {
        timer -= Time.deltaTime;
        dir = new Vector3(x, y, z);
        transform.Translate(dir.normalized * Time.deltaTime * speed);
        if (timer <= 0)
        {
            start = false;
            timer = 5;
        }
    }
    public void fire()
    {
        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0)
        {
            fireCounter = fireDelay;
            Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
        }

    }
    public void goToCenter()
    {

        transform.position = Vector3.MoveTowards(transform.position, center, speed * Time.deltaTime);
        if (transform.position == center)
            turning = false;
    }

    public void changeMove()
    {
        x = Random.Range(-BossDist, BossDist);
        y = Random.Range(-BossDist, BossDist);
        z = Random.Range(-0.1f, 0.1f);
        rand = Random.Range(0, 100);

    }
}
