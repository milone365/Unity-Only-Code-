using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMinion : MonoBehaviour,IMinion 
{
    
    
    public float attackTimer, attackCounter=3f;
    private bool canAttack = false;
    private Transform target;
     public Animator anim;
    public GameObject fireball;
    public Transform spawnPoint;
    public GameObject goldEgg;


    public int health = 10;

    private void Start()
    {
        canAttack = false;
        transform.rotation = PlayerController.instance.transform.rotation;
        transform.position= new Vector3(PlayerController.instance.transform.position.x + 0.5f, PlayerController.instance.transform.position.y + 0.75f, PlayerController.instance.transform.position.z - 0.5f);
        
    }
    private void Update()
    {
        Move();
   }
    

    public void Move()
    {
        if (!canAttack)
        {
            var distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            if (distanceToPlayer > 2f)
            {
                Vector3 destination = new Vector3(PlayerController.instance.transform.position.x + 0.5f, PlayerController.instance.transform.position.y + 0.75f, PlayerController.instance.transform.position.z - 0.5f);
                transform.position = Vector3.MoveTowards(transform.position, destination, 0.1f);

                setRotation(PlayerController.instance.transform);
            }
            else
            {
                transform.position = transform.position;
                setRotation(PlayerController.instance.transform);
            }
        }
        else
        {
            transform.position = transform.position;
            attack();
            setRotation(target);
        }

        if (target == null)
        {
            anim.SetBool("stay", true);
            canAttack = false;
            return;
        }
        if (Input.GetButtonDown("Fire2") || Input.GetButtonDown("Jump"))
        {
            canAttack = false;
        }
    }

    public void attack()
    {
        anim.SetBool("stay", true);
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            anim.SetBool("stay", false);
            attackTimer = attackCounter;
            anim.SetTrigger("atk");

        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(goldEgg, transform.position, transform.rotation);
            Destroy(gameObject);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            target = other.transform;
            canAttack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
            canAttack = false;
        }
    }
    public void Fire()
    {
        Instantiate(fireball, spawnPoint.position, spawnPoint.rotation) ;
        
    }

    public void setRotation(Transform rotationTarget)
    {
        transform.LookAt(rotationTarget, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
}
