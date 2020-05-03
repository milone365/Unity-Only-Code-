using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public int currentline;
    public Text dialogText;
    public bool dialogActive;
    public string[] dialogLines;
    
    
    

    public static DialogManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentline = 0;
    }

    private void Update()
    {
        if (dialogActive)
        {
            
            if (Input.GetButtonDown("Fire2"))
            {
                currentline++;
                if (currentline >= dialogLines.Length)
                {
                    
                    dialogActive = false;
                    currentline = 0;
                    dialogBox.SetActive(false);
                    PlayerController.instance.Stopmove=false;
                    


                }
                StopAllCoroutines();
                StartCoroutine(dialogCo());
            }
            
            
            
        }
    }

    IEnumerator dialogCo()
    {
        dialogText.text = "";
        foreach (char letter in dialogLines[currentline].ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
       
    }
  
    public void showDialog()
    {
        dialogActive = true;
        PlayerController.instance.Stopmove = true;
        currentline = 0;
        dialogBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(dialogCo());

    }
   
}
