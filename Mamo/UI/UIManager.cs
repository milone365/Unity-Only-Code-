using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text coinText,shortText,ammotext,comsumableText,lifetext,manatext;
    public Text enemyname;
    public Slider enemyhealthBar;
    public GameObject shortTextBox,enemyBar;
    public static UIManager instance;
    public Slider healthBar,manaBar;
    
    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

 
    public void getShortMessage(string message)
    {
        shortTextBox.SetActive(true);
        shortText.text = message;
        
    }

    public void activeEnemyBar(int max,int current,string _name)
    {
        enemyhealthBar.maxValue = max;
        enemyhealthBar.value = current;
        enemyname.text = _name; 
        enemyBar.SetActive(true);
    }
    public void deactiveEnemyBar()
    {
        
           // enemyBar.SetActive(false);
        
        
    }
    public void updateEnemyStatus(int cur)
    {
        enemyhealthBar.value = cur;
    }

    public void UpdateAmmo(int value)
    {
        ammotext.text = "" + value;
    }
    public void UpdateUI()
    {
        healthBar.maxValue = HealthManager.instace.MaxHealth;
        healthBar.value = HealthManager.instace.currentHealth;
        manaBar.maxValue = GameManager.instace.maxmp;
        manaBar.value = GameManager.instace.currentmp;
        coinText.text = " "+GameManager.instace.currentCoin;
        manatext.text = GameManager.instace.currentmp + "/" + GameManager.instace.maxmp;
        lifetext.text = HealthManager.instace.currentHealth + "/" + HealthManager.instace.MaxHealth;
        if (Bag.instance.ammo <= 0)
        {
            ammotext.text = "" ;
        }
        else
        {
            ammotext.text = "" + Bag.instance.ammo;
        }
        if (Bag.instance.reserve <= 0)
        {
            comsumableText.text = "";
        }
        else
        {
            comsumableText.text = "" + Bag.instance.reserve;
        }
       
        
    }
}
