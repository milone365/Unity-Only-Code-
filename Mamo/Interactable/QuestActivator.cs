using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestActivator : MonoBehaviour
{
    public int questNumber;
    public bool questStarter, questEnder;
    public int questSong;
   

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!QuestManager.instance.questCompleted[questNumber])
                {
                    if (questStarter && !QuestManager.instance.quest[questNumber].gameObject.activeSelf)
                    {
                        QuestManager.instance.quest[questNumber].gameObject.SetActive(true);
                        QuestManager.instance.quest[questNumber].startingQuest();
                        AudioManager.instance.giveCurrentSong(questSong);
                    }

                    else if (questEnder)
                    {
                        QuestManager.instance.quest[questNumber].endQuest();
                    }
                   
                }
            }
        }
    }
}
