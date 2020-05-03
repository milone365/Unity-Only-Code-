using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoors : MonoBehaviour
{
    public string levelToLoad;
    public bool unLocked;
    public SpriteRenderer doorTop, doorBottom;
    public Sprite doorBottomOpen, doorTopOpen, doorBottomClosed, doorTopClosed;
   
    
    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("Level1", 1);
        if (PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            unLocked = true;
        }
        else
        {
            unLocked = false;
        }
        if (unLocked)
        {
            doorTop.sprite = doorTopOpen;
            doorBottom.sprite = doorBottomOpen;
        }
        else
        {
            doorTop.sprite = doorTopClosed;
            doorBottom.sprite = doorBottomClosed;
        }

    }
    // Update is called once per frame
    void Update()
    {
        

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetButtonDown("Jump")&&unLocked)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}      

    
