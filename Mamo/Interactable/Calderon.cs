using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calderon : interactable
{
    public bool isMana, IsLife;
    public int requiredHerbs, requiredMushrooms, requiredFlowers;
    public GameObject _bagPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (!_bagPanel.activeInHierarchy)
        {
            _bagPanel.SetActive(true);
        }
        
        bagPanel.instance.UpdateBagPanel();
        if (other.tag == "Player" && Input.GetButtonDown("Fire1")){

            if (isMana)
            {
                Bag.instance.makeManaPotion(requiredHerbs, requiredFlowers);
            }
            else if (IsLife)
            {
                Bag.instance.makeLifePotion(requiredHerbs, requiredMushrooms);
            }
        }
       
    }
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (_bagPanel.activeInHierarchy)
        {
            _bagPanel.SetActive(false);
        }
        
    }
}
