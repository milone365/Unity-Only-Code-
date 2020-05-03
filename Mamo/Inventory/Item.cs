using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item :MonoBehaviour
{
    public string itemName;
   
    public int numberOfItems;
    public GameObject particle;
    public int sfxToPlay;
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Bag.instance.Add(itemName, numberOfItems);
            UIManager.instance.UpdateUI();
            Destroy(gameObject);
            AudioManager.instance.playSFX(sfxToPlay);
            Instantiate(particle, transform.position, transform.rotation);
        }
        
    }
}
