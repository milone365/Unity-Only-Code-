using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {
    
    private Rigidbody2D playerRB;
    public float bounceForce = 2f;
    public GameObject destroyEffect;
    public SpiderController spidercon;
   

	// Use this for initialization
	void Start () {
        playerRB = transform.parent.GetComponent<Rigidbody2D>();
        spidercon = FindObjectOfType<SpiderController>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, 0f);
            
        }
        if (collision.tag == "Spider")
        {
            spidercon.generate();
            collision.gameObject.SetActive(false);
            playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, 0f);


        }
        if (collision.tag == "Boss")
        {  
            playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, 0f);
            collision.transform.parent.GetComponent<BossController>().takeDamage = true;
        }

        if (collision.tag == "Destroyable")
        {
            Destroy(collision.gameObject);
            playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, 0f);
           Instantiate(destroyEffect, transform.position, transform.rotation);
            LevelManager.instance.splatSound.Play();

        }
    }
}
