using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWidleController : MonoBehaviour {
    public Transform leftPoint;
    public Transform rightPoint;
    public float moveSpeed;
    private Rigidbody2D rb;
    public bool movingright;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (movingright&&transform.position.x>rightPoint.position.x)
        {
            movingright = false;
        }
        if (!movingright && transform.position.x < leftPoint.position.x)
        {
            movingright = true;
        }
        if (movingright)
        {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);
        }
        else
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
        }
	}
}
