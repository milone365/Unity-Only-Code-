using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrone : MonoBehaviour
{
    BossMove boss;
    float x=0, y=0,z=0;
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
    float changemovemente=5;
    GameObject orbit;
    [SerializeField]
    float BossDist=5;
    [SerializeField]
    float angle = 30;
    int tipe = 0;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindObjectOfType<BossMove>();
        orbit = GameObject.Find("DroneOrbit");
       
        fireCounter = fireDelay;
        x = Random.Range(-BossDist, BossDist);
        y = Random.Range(-BossDist, BossDist);

       // tipe = Random.Range(0, 2);

        while (x > -2 && x < 2)
        {
            x = Random.Range(-BossDist, BossDist);
        }
        while (y > -2 && y< 2)
        {
            y = Random.Range(-BossDist, BossDist);
        }

            start = true;
        player = PlayerController.instance.transform;
        attackDistance = (boss.gameObject.transform.position.z - player.position.z)/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer -= Time.deltaTime;
            dir = new Vector3(x, y, z);
            transform.Translate(dir.normalized * Time.deltaTime * speed);
            if (timer <= 0)
            {   
                start = false;
            }
        }
        else
        {
            Vector3 angleDir = player.position - transform.position;
           float  visAngle = Vector3.Angle(angleDir, transform.forward);

            if (angle < visAngle)
            {
                transform.position = Vector3.MoveTowards(transform.position, orbit.transform.position,speed*Time.deltaTime);
                if (transform.position == orbit.transform.position)
                {
                    start = true;
                }

            }
            //player 
            Vector3 loocktarget = player.position;
            Vector3 direction = loocktarget - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 1.2f);
            if (direction.magnitude > attackDistance)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else
            {
                  changemovemente -= Time.deltaTime;
                    if (changemovemente <= 0)
                    {
                        changemovemente = 5;
                        changeMove();
                    }
                    dir = new Vector3(x, y, 0);
                    Vector3 newDirec = Vector3.Slerp(transform.localPosition, dir, speed);
                  
                    transform.Translate(newDirec.normalized  * Time.deltaTime, Space.Self);
                    
            
                

            }
               
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireDelay;
                Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
            }
        }
       
            

        
    }


    public void changeMove()
    {
        x = Random.Range(-BossDist, BossDist);
        y = Random.Range(-BossDist, BossDist);
        z = Random.Range(-0.1f, 0.1f);

    }
}
