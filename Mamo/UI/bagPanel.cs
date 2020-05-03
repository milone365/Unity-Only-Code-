using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bagPanel : MonoBehaviour
{
    public static bagPanel instance; 
    public Text iron, gold, herbs, diamond, flowers, mushrooms;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")){
            if (this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void UpdateBagPanel() 
    {
        iron.text =Bag.instance.iron.ToString();
        gold.text = Bag.instance.wood.ToString();
        herbs.text = Bag.instance.herbs.ToString();
        diamond.text = Bag.instance.gem.ToString();
        flowers.text = Bag.instance.flowers.ToString();
        mushrooms.text = Bag.instance.mushrooms.ToString();
    }
}
