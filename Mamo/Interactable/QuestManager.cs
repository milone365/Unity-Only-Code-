using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public Quest[] quest;
    public bool[] questCompleted;
    public string itemCollected;
    public string EnemyKilled;
    private int[] questSaver;

    private void Awake()
    {
        
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
        questCompleted = new bool[quest.Length];
        EnemyKilled = "";
        loadData();

    }

    public void showQuestText(string text) {
        
        DialogManager.instance.dialogLines = new string[1];
        DialogManager.instance.dialogLines[0] = text;
        DialogManager.instance.currentline= 0;
        DialogManager.instance.showDialog();

    } 

    public void saveData()
    {
       
        for(int i = 0; i < quest.Length; i++)
        {
           if( questCompleted[i] == true)
            {
                PlayerPrefs.SetInt("questcompleted"+quest[i].ToString(), 1);
            }
            else
            {
                PlayerPrefs.SetInt("questcompleted" + quest[i].ToString(), 0);
            }
      }
    }
    public void loadData()
    {
        if (PlayerPrefs.GetInt("save") == 1)
        {
            
            for (int i = 0; i < questCompleted.Length; i++)
            {
                int valueToset = 0;
                if (PlayerPrefs.GetInt("questcompleted" + quest[i].ToString()) == 1)
                {  valueToset
                   = PlayerPrefs.GetInt("questcompleted" + quest[i].ToString());


                }
                if (valueToset == 0)
                {
                    questCompleted[i] = false;
                }
                else
                {
                    questCompleted[i] = true;
                }

            }
        }
    }
  
}
