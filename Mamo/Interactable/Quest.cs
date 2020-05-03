using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    public int experience;
    public int questNumber;
    [Header("Info")]
    public string startText;
    public string endText,questName;
    [Header("Conditions")]
    public bool itemQuest;
    public bool enemyQuest;
    public string targetEnemy;
    public string targetItem;
    public int enemyToKill, itemToCollect;
    private int enemyKiledCount, itemCollectedCount;
    [Header("Sounds")]
    public int questCompleteSound;
    public int questAcceptedSound;
    [Header("Ricompense")]
    public bool haveRicompense;
    public GameObject ricompense;
    public Transform ricompenseSpawnPoint;
    [Header("ActiveWhithQuest/DeactivewhithQuest")]
    public bool objAfetrQuest;
    public bool objBeforQuest;
    
    public GameObject[] objectToactive;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (itemQuest)
        {
            if (QuestManager.instance.itemCollected == targetItem)
            {
                QuestManager.instance.itemCollected = null;
                itemCollectedCount++;
                collectedItemShortText();
                
                if (itemCollectedCount >= itemToCollect)
                {

                    endQuest();
                
                }
            }
        }else if (enemyQuest)
        {
            if (QuestManager.instance.EnemyKilled == targetEnemy)
            {
                QuestManager.instance.EnemyKilled = null;
                enemyKiledCount++;
                KilledEnemyShortText();
               
                if (enemyKiledCount >= enemyToKill)
                {

                    endQuest();
                 
                }
            }
        }
    }
    public void KilledEnemyShortText()
    {
        UIManager.instance.getShortMessage(targetEnemy + ": " + enemyKiledCount + "/" + enemyToKill + " 敗北");
    }
    public void collectedItemShortText()
    {
        UIManager.instance.getShortMessage(targetItem + ": " + itemCollectedCount + "/" + itemToCollect + " 集まった");

    }
    public void startingQuest()
    {
        
        QuestManager.instance.showQuestText(startText);
        UIManager.instance.getShortMessage(questName);
        AudioManager.instance.playSFX(questAcceptedSound);
        if (objBeforQuest)
        {
            for(int i = 0; i < objectToactive.Length; i++)
            {
                if (!objectToactive[i].activeInHierarchy)
                {
                    objectToactive[i].SetActive(true);
                }
                else
                {
                    objectToactive[i].SetActive(false);
                }
                   
                
                
               
            }
            
        }
    }
    
    public void endQuest()
    {
        if (QuestManager.instance.quest[questNumber].endText != null)
        {
            QuestManager.instance.showQuestText(endText);
        }
        QuestManager.instance.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
        UIManager.instance.getShortMessage(questName+ " :クエスト完成!");
        AudioManager.instance.playSFX(questCompleteSound);
        if (haveRicompense)
        {
            Instantiate(ricompense, ricompenseSpawnPoint.position, Quaternion.identity);
        }
        
        CharStatus.instance.addExperience(experience);

        AudioManager.instance.giveCurrentSong(0);
        if (objAfetrQuest)
        {
            for (int i = 0; i < objectToactive.Length; i++)
            {
                if (!objectToactive[i].activeInHierarchy)
                {
                    objectToactive[i].SetActive(true);
                }
                else
                {
                    objectToactive[i].SetActive(false);
                }
               
            }
            
        }
    }
}
