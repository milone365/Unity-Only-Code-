using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenu;
    void Start()
    {
        pauseMenu = GameObject.Find("PAUSE");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    public void GameExit()
    {
        Application.Quit();
    } 
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
