using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMinion : MonoBehaviour,IMinion
{
    public float attackTimer, attackCounter = 3f;
    private bool canAttack = false;
    private Transform target;
    public Animator anim;
    
     public int health = 10;

// Start is called before the first frame update
    void Start()
    {
        canAttack = false;
        transform.rotation = PlayerController.instance.transform.rotation;
        transform.position = new Vector3(PlayerController.instance.transform.position.x + 0.5f, PlayerController.instance.transform.position.y , PlayerController.instance.transform.position.z - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
    public void setRotation(Transform rotationTarget)
    {
        transform.LookAt(rotationTarget, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
    public void attack()
    {
        anim.SetBool("move", false);
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            int randAttack = Random.Range(1, 2);
            
            
            attackTimer = attackCounter;
            if (randAttack == 1)
            {
                anim.SetTrigger("atk");
            }
            else
            {
                anim.SetTrigger("atk2");
            }
            

        }
    }
    public void takeDamage(int damage)
    {

        health -= damage;
        StartCoroutine(hurtCo());
        if (health <= 0)
        {

            StartCoroutine(dieCo());

        }
    }
    IEnumerator hurtCo()
    {
        anim.SetInteger("hurt", 1);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("hurt", 0);
    }
    
    public void Move()
    {
        if (!canAttack)
        {
            var distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            if (distanceToPlayer > 2f)
            {
                anim.SetBool("move", true);
                Vector3 destination = new Vector3(PlayerController.instance.transform.position.x + 0.5f, PlayerController.instance.transform.position.y, PlayerController.instance.transform.position.z - 0.5f);
                transform.position = Vector3.MoveTowards(transform.position, destination, 0.1f);

                setRotation(PlayerController.instance.transform);
            }
            else
            {
                anim.SetBool("move",false);
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
            
            canAttack = false;
            return;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            canAttack = false;
        }
    }
    IEnumerator dieCo()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
