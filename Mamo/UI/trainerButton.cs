using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class trainerButton : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public Text priceText, Nametext, descriptionTXT;
    public bool lifePot, manaPot, combo, str, stamina, mana;
    private int price;
    private string Skillname, description;
    public trainer _trainer;
    private int buyedMultipler=1;
    public int buttoNumber;
   

    public void infoUpdate(int _price, string _Skill,string _description)
    {
        price = _price;
        Skillname = _Skill;
        description = _description;
        priceText.text = "" + price*buyedMultipler;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _trainer = GetComponent<trainer>();
        if (PlayerPrefs.GetInt("save") == 1)
        {
            if (PlayerPrefs.HasKey("multipler" + buttoNumber.ToString()))
            {
                buyedMultipler = PlayerPrefs.GetInt("multipler" + buttoNumber.ToString());
            }
            else
            {

                buyedMultipler = 1;
            }
        }
        else
        {
            buyedMultipler = 1;
        }
        
    }

  public void Buy()
    {
        if (GameManager.instace.currentCoin >= price*buyedMultipler)
        {
            if (lifePot)
            {
                Bag.instance.healingPotionPower++;
            }else if (manaPot)
            {
                Bag.instance.manaPotionPower++;
            }else if (stamina)
            {
                HealthManager.instace.MaxHealth++;
            }else if (str)
            {
                GameManager.instace.addForce();
            }else if (mana)
            {
                GameManager.instace.maxmp++;
            
            }

            GameManager.instace.currentCoin -= (price * buyedMultipler);
            UIManager.instance.UpdateUI();
            buyedMultipler++;
            priceText.text = "" + price * buyedMultipler;
            saveAcquist();


        }
        else
        {
            UIManager.instance.getShortMessage("you don't have money");
        }
    }

    public void learn()
    {
        if (GameManager.instace.currentCoin >= price * buyedMultipler)
        {
            if (combo)
            {
                if (!GameManager.instace.comboActive)
                {
                    GameManager.instace.comboActive = true;
                    GameManager.instace.currentCoin -= (price * buyedMultipler);
                    
                }
                else
                {
                    UIManager.instance.getShortMessage("is just buyed");
                }
          
            
                UIManager.instance.UpdateUI();
                priceText.text = "";
            }
        }


    }
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionTXT.text = "";
        Nametext.text = "Skills";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionTXT.text = description;
        Nametext.text = Skillname;
    }
 
    public void saveAcquist()
    {
        PlayerPrefs.SetInt("multipler" + buttoNumber.ToString(), buyedMultipler);
    }
}
