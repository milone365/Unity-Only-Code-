using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public static BossMove instance;
    Transform player;
    [SerializeField] float rotatespeed = 1.2f;
    public bool lookPlayer;
    [SerializeField]
    float hight=10,speed=5,acuracy=0.1f;
    [SerializeField]
    GameObject boost;
   
    
    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        lookPlayer = true;
        player = PlayerController.instance.transform;
        //LastBossHealth.instance.healthIsLess += dontLoockPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookPlayer)
        {
            /*transform.LookAt(player);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);*/
            Vector3 lookDirection = new Vector3(player.position.x, transform.position.y, player.position.z);
            Vector3 direction = lookDirection - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotatespeed * Time.deltaTime);
            if (player.position.y > transform.position.y + hight|| player.position.y < transform.position.y - hight)
            {
                Vector3 newPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime );
                activeBoost(true);

            }
            else
            {
                activeBoost(false);
            }
            
        }
       
    }


    public void activeBoost(bool value)
    {
        boost.SetActive(value);
    }

  /*  public void dontLoockPlayer(int num)
    {
        if (num == 2)
        {
            lookPlayer = false;
        }
        
    }
    private void OnDisable()
    {
        LastBossHealth.instance.healthIsLess -= dontLoockPlayer;
    }*/
}
