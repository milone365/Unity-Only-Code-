using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vendor : MonoBehaviour
{
    public static vendor instance;
    public Button firstButton;
    public Sprite blankImage;
    private bool menuActive;
    public GameObject shopMenu;
    public Sprite[] imagesOFItems;
    public int numberofSlots;
    public int[] prices;
    public ItemsButtonsICONS[] itemButICON;
    public ShopButton[] sB;
    public GameObject[] objeCt;
    public string[] _description;
    public int[] _disponibleItm;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")&&menuActive)
        {
            CloseMenu();
        }
    }
    public void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1")&&!menuActive){
                Cursor.visible = true;
                shopMenu.SetActive(true);
                menuActive = true;
                PlayerController.instance.Stopmove = true;



                for (int i = 0; i < itemButICON.Length; i++)
                {

                    itemButICON[i].GetComponent<Image>().sprite = imagesOFItems[i];
                    if (_disponibleItm[i] == 0)
                    {
                        itemButICON[i].GetComponent<Image>().sprite = blankImage;
                    }
                    
                }
                for(int i = 0; i < sB.Length; i++)
                {
                    
                    sB[i].giveInfo(prices[i], objeCt[i],_description[i],_disponibleItm[i]);
                }
                firstButton.Select(); 
            }
            
        }
    }
    public void CloseMenu()
    {
        shopMenu.SetActive(false);
        menuActive = false;
        Cursor.visible = false;
        PlayerController.instance.Stopmove = false;
    }
    public void OnTriggerExit(Collider other)
    {
        CloseMenu();
    }
   
}
