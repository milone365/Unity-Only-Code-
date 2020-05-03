using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FootmanMiniQuest:MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject ricompense;
    [SerializeField]
    Transform spawnpoint;
    bool isSick = true;
    public string message;
    private GameObject littlePanel, smalltext;
    [SerializeField]
    GameObject objectToActive;
   

    public void Awake()
    {
        littlePanel = GameObject.FindGameObjectWithTag("lp");
        smalltext = GameObject.FindGameObjectWithTag("sm");
    }
    public void interact(string text)
    {
        littlePanel.GetComponent<Image>().enabled = true;

        smalltext.GetComponent<Text>().text = text;


    }
    public void deactiveDialog()
    {
        littlePanel.GetComponent<Image>().enabled = false;
        smalltext.GetComponent<Text>().text = "";
    }
    private void Start()
    {
        anim.GetComponent<Animator>();
        isSick = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isSick)
            {
                interact(message);
                if (Input.GetButtonDown("Fire1"))
                {

                    if (Bag.instance.hppotionNumber > 0)
                    {
                        Bag.instance.hppotionNumber--;
                        anim.SetBool("cure", true);
                        Instantiate(ricompense, spawnpoint.position, spawnpoint.rotation);
                        isSick = false;
                        UIManager.instance.UpdateUI();
                        objectToActive.SetActive(true);
                       
                    }
                    else
                    {
                        UIManager.instance.getShortMessage("Hpポーションはありません");
                    }
                }


            }
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        deactiveDialog();

    }
}
