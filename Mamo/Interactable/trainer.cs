using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trainer : interactable
{
    public GameObject trainPanel;
    public Sprite blankImage;
    public ItemsButtonsICONS[] buttonIcons;
    public Sprite[] powersImage;
    public trainerButton[] trb;
    public int[] price;
    public string[] skill;
    public string[] description;
 
    public override void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Cursor.visible = true;
                trainPanel.SetActive(true);
                PlayerController.instance.Stopmove = true;
                for(int i = 0; i < buttonIcons.Length; i++)
                {
                    buttonIcons[i].GetComponent<Image>().sprite = powersImage[i];


                }
                for(int j = 0; j < trb.Length; j++)
                {
                    trb[j].infoUpdate(price[j], skill[j], description[j]);
                }
            }
        }
    }

    public void closeMneu()
    {
        trainPanel.SetActive(false);
        PlayerController.instance.Stopmove = false;
    }
}
