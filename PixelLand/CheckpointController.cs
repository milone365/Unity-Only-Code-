using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
    public Sprite closeFlag;
    public Sprite openFlag;
    private SpriteRenderer spriteRen;
    public bool checkPointActivive;
	// Use this for initialization
	void Start () {
        spriteRen = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spriteRen.sprite = openFlag;
            checkPointActivive = true;
        }
    }
    
}
