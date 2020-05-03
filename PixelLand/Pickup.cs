using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public int Valor = 1;
    public bool isLife;
    public bool isMoney;
    public bool isHealt;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isMoney)
            {
                LevelManager.instance.addCoin(Valor);
                
            }
            else if (isLife)
            {
                LevelManager.instance.Addlife();
            }
            else if (isHealt)
            {
                LevelManager.instance.giveHealt(Valor);
            }
            
            gameObject.SetActive(false);
        }
    }
}
