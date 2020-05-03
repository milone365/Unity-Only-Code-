using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text matchesText;
    public Text scoretext;
    public static UIManager instace;
    Slider starcounter;
    GameObject endpanel;
    GameObject copper, silver, gold;
    Text winText;
    private void Awake()
    {
        instace = this;
        starcounter = GetComponentInChildren<Slider>();
        endpanel = GameObject.Find("EndGame");
        copper = GameObject.Find("copper").gameObject;
        silver = GameObject.Find("silver").gameObject;
        gold = GameObject.Find("gold").gameObject;
        winText = endpanel.GetComponentInChildren<Text>();
        copper.SetActive(false);
        silver.SetActive(false);
        gold.SetActive(false);
        endpanel.SetActive(false);

    }
 
    public void UpdateScore(int value)
    {
        scoretext.text = "Score : " + value;
        updateStarmeter(value);
    }
    public void updateMatch(int value)
    {
       matchesText.text = value.ToString();
    }
    public void setStarMeter( int value)
    {
        starcounter.maxValue = value;
        starcounter.value = 0;
    }
    public void updateStarmeter(int values)
    {
        starcounter.value = values;
    }

    public void activeEndPane(int score)
    {
        endpanel.SetActive(true);
        winText.text = "Score : " + score;
        
    }

    public void ActiveStar(string s)
    {
        if (s == "copper")
        {
            copper.SetActive(true);
        }
        if (s == "silver")
        {
            copper.SetActive(true);
            silver.SetActive(true);
        }
        if (s == "gold")
        {
            copper.SetActive(true);
            silver.SetActive(true);
            gold.SetActive(true);
        }
    }
}
