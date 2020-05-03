using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public Rigidbody rb;
    private bool isCameBack;
    Vector3 playerpose;
    public float returnRadius = 1.5f;
    public GameObject playerHand;
    bool returning;
    public float returnTime = 3f;
    public Animator anim;
    private void Start()
    {
        isCameBack = true;
        returning = false;
    }
    void Update()
    {
        playerpose = playerHand.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerHand.transform.position);
        if (returning)
        {
            if (distanceToPlayer <= returnRadius)
            {
                transform.position = playerHand.transform.position;
                isCameBack = true;
                returning = false;
                anim.SetInteger("rotate", 0);
            }
            else if(distanceToPlayer > returnRadius&& returnTime>0) {
                returnTime -= Time.deltaTime;
                if (returnTime <= 0)
                {
                    transform.position = playerHand.transform.position;
                    isCameBack = true;
                    returning = false;
                    returnTime = 3f;
                    anim.SetInteger("rotate", 0);
                }
                
            }
            
        }
        if (Input.GetButtonDown("Fire1")&&isCameBack)
        {
            StartCoroutine(Throw(18.0f, 1.0f, Camera.main.transform.forward, 2.0f));
        }
    }

    IEnumerator Throw(float dist, float width, Vector3 direction, float time)
    {
        isCameBack = false;
        anim.SetInteger("rotate", 1);
        Vector3 pos =  transform.position;
        float height = transform.position.y;
        Quaternion q = Quaternion.FromToRotation(Vector3.forward, direction);
        float timer = 0.0f;
        rb.AddTorque(0.0f, 400.0f, 0.0f);
        while (timer < time)
        {
            float t = Mathf.PI * 2.0f * timer / time - Mathf.PI / 2.0f;
            float x = width * Mathf.Cos(t);
            float z = dist * Mathf.Sin(t);
            Vector3 v = new Vector3(x, height, z + dist);
            rb.MovePosition(pos + (q * v));
            timer += Time.deltaTime;
            yield return null;
        }

        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        rb.MovePosition(playerpose);
        returning = true;
        
        
    }
}

