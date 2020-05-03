using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {
    public float moveSpeed;
    private bool canMove;
    private Rigidbody2D rb;
    public GameObject deathSpider;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
       
    }
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
        }
	}
    private void OnBecameVisible()
    {
        canMove = true;
    }
    private void OnEnable()
    {
        canMove = false;
    }
    public void generate()
    {
        Instantiate(deathSpider, transform.position, transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
            
            gameObject.SetActive(false);
            
        }
    }
    
}
