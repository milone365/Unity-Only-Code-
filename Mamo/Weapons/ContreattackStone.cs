using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContreattackStone : MonoBehaviour
{
    public Rigidbody rb;
    private bool isTaked, isAtPopint, launched;
    private Transform target;
    public float speed = 2000f;
    private Transform launchPoint;
    public int damage = 25;
    private bool giveDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

   
    // Update is called once per frame
    void Update()
    {
        if (isTaked)
        {


            if (Input.GetButtonDown("Fire1"))
            {
                
                transform.SetParent(null);
                rb.useGravity = true;
                rb.isKinematic = false;

                
                   if (Bag.instance.crossHairIsActive)
                    {
                        RaycastHit hit;
                        Ray newray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(newray, out hit))
                        {
                            Vector3 direction = hit.point - transform.position;

                            rb.velocity = (direction * speed * 3);


                    }
                    else {
                        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
                        rb.AddTorque(0, 400, 0);
                        isTaked = false;
                    }
                   
                }
                else
                {

                    rb.AddForce(transform.forward * speed, ForceMode.Impulse);
                    rb.AddTorque(0, 400, 0);
                    
                }
                isTaked = false;
            }
         }
        }
    


    private void OnTriggerEnter(Collider other)
    {
        var boss = GameObject.Find("BossGolem");
        if (boss != null)
        {
            if (other.tag == "Player" && !isTaked)
            {
                target = boss.GetComponent<Transform>();
                
                isTaked = true;
                rb.isKinematic = true;
                rb.useGravity = false;
                launchPoint = Bag.instance.armSpawner.transform;
                transform.position = launchPoint.position;

                this.transform.SetParent(launchPoint);
                transform.rotation = Bag.instance.armSpawner.rotation;
                
            }
        }
        if (other.tag == "Enemy")
        {
            if (!giveDamage)
            {
                giveDamage = true;
                other.GetComponent<IEnemyHealth>().hurtEnemy(damage);
            }

            Destroy(gameObject);
        }
    }
}
