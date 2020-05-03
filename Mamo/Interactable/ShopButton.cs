using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private string dontHaveMoney;
    public int itemPrice;
    public int disponibleItems;
    public Transform spawnPoint;
    public Text itemPriceText, descriptionText;
    private GameObject objectToSpawn;
    private string itemDescription;
    public int buttonNumber;
    public vendor _vendor;

    public void Start()
    {
        _vendor.GetComponent<vendor>();
    }


    public void giveInfo(int pr, GameObject gm, string description, int  num)
    {
        itemPrice = pr;
        objectToSpawn = gm;
        itemDescription = description;
        disponibleItems = num;
        if (disponibleItems > 0)
        {
            itemPriceText.text = "" + pr;
        }
        else
        {
            itemPriceText.text = "";
        }
        
    }
   
    public void buy()
    {
        if (disponibleItems > 0)
        {
            if (GameManager.instace.currentCoin >= itemPrice)
            {
                GameManager.instace.currentCoin -= itemPrice;
                disponibleItems--;
                _vendor._disponibleItm[buttonNumber]--;
                Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
                UIManager.instance.UpdateUI();
            }
            else

            {
                dontHaveMoney = "i don't have a money";
                UIManager.instance.getShortMessage(dontHaveMoney);
            }

        }
        else
        {
            UIManager.instance.getShortMessage("SoldOut");
        }


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (disponibleItems > 0)
        {
            descriptionText.text = itemDescription;
        }
        else
        {
            descriptionText.text = "SoldOut";
        }
    }
}
