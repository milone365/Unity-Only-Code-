using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStatus : MonoBehaviour
{
    public static CharStatus instance;
    public int currentLevel, currentEXP;
    public int[] toLevelUp;
    public GameObject levelUpEffect;
   public Text levelText,expText;
    public Slider levelSlider;
    public Transform aura;
    [SerializeField] int maxLevel;
    public bool reachMaxLevel = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("save")&&PlayerPrefs.GetInt("save") == 1)
        {
            
                currentLevel = PlayerPrefs.GetInt("level");
                currentEXP = PlayerPrefs.GetInt("exp");
                if (PlayerPrefs.HasKey("maxlevel")&& PlayerPrefs.GetInt("maxlevel")==1)
                {
                    reachMaxLevel = true;
                }
                else
                {
                    reachMaxLevel = false;
                }
            }
               
           
        
        
        maxLevel = toLevelUp.Length-1;
        levelText.text = "Lv: " + currentLevel;
        levelSlider.maxValue = toLevelUp[currentLevel];
        levelSlider.value = currentEXP;
        expText.text = currentEXP + " / " + toLevelUp[currentLevel];
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!reachMaxLevel)
        {

        
            if (currentLevel >= toLevelUp.Length)
            {
                currentLevel = maxLevel;
                reachMaxLevel = true;
            }
        }

    }
    
    public void addExperience(int expToAdd)
    {
        if (!reachMaxLevel)
        {
            currentEXP += expToAdd;
            while (currentEXP >= toLevelUp[currentLevel])
            {
                currentEXP -= toLevelUp[currentLevel];
                currentLevel++;
                GameManager.instace.maxmp++;
                GameManager.instace.restoreMana(1);
                HealthManager.instace.MaxHealth++;
                HealthManager.instace.restoreHealth();

                if (currentLevel >= toLevelUp.Length)
                {
                    currentLevel = maxLevel;
                    reachMaxLevel = true;
                }
                Instantiate(levelUpEffect, PlayerController.instance.transform.position, transform.rotation);
            }

            expText.text = currentEXP + " / " + toLevelUp[currentLevel];
            levelText.text = "Lv: " + currentLevel;
            levelSlider.maxValue = toLevelUp[currentLevel];
            levelSlider.value = currentEXP;
        }
        else
        {
            expText.text = "Max" + " / " + "Max";
            levelSlider.maxValue = 1;
            levelSlider.value = 1;
        }
        
}
   public void saveLevel()
    {
        PlayerPrefs.SetInt("level", currentLevel);
        PlayerPrefs.SetInt("exp", currentEXP);
        if (reachMaxLevel)
        {
            PlayerPrefs.SetInt("maxlevel", 1);
        }
    }
}
        
    

