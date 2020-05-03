using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissiles : MonoBehaviour,IMissile<Vector3>
{

    [SerializeField] GameObject explosion;
    [SerializeField] float velocity = 4f, rotateSpeed = 1;
    [SerializeField] int damage = 10;
    [SerializeField]
    float veloctyMultipler = 50f;
    Transform player;
    Vector3 newTarget;
    bool followPlayer;
    private void Start()
    {
        
        if (PlayerController.instance != null)
            player = PlayerController.instance.transform;
        followPlayer = true;
        Destroy(gameObject, 5);

    }
    // Update is called once per frame
    void Update()
    {
       
            if (PlayerController.instance != null)
            {
            if (followPlayer)
            {
                //プレイヤーへ動く
                
                Vector3 lookRot =new Vector3 (player.position.x,player.position.y,player.position.z);
                Vector3 dir = lookRot - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
                if(dir.magnitude>0.1f)
                transform.Translate(dir.normalized * Time.deltaTime * velocity*veloctyMultipler, Space.World); 
               
            }
            else
            {

                Vector3 lookRot = new Vector3(newTarget.x,newTarget.y,newTarget.z);
                Vector3 dir = lookRot - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
                if (dir.magnitude > 0.1f)
                    transform.Translate(dir.normalized * Time.deltaTime * velocity * veloctyMultipler, Space.World);

            }
        }
        else
        {
            transform.Translate(0, 0, 0.4f);
        }
           
      
    }

 
    //削除
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Player" || other.name == "KillZone" || other.tag == "Bullet")
        {
               if (other.tag == "Player")
            {
                other.GetComponent<HealthManager>().Takedamage(damage);
                
            }
            Destroy(gameObject);
        }


    }

    //新しいtargetを作る
    public void giveATarget(Vector3 pos)
    {
        followPlayer = false;
        newTarget = pos;
     }
    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
