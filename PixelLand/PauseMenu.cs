using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public string MainMenu, LevelSelect;
    public GameObject pauseScreen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel")) {
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
	}
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        if (BossController.instance.bossActive)
        {
            LevelManager.instance.bossMusic.Play();
        }
        else
        {
            LevelManager.instance.levelMusic.Play();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        LevelManager.instance.levelMusic.Stop();
        LevelManager.instance.bossMusic.Stop();
    }
    public void quitTomainMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CoinCounter", LevelManager.instance.coinCout);
        PlayerPrefs.SetInt("Lives", LevelManager.instance.currentLives);
        SceneManager.LoadScene(MainMenu);
    }
    public void levelSel()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CoinCounter", LevelManager.instance.coinCout);
        PlayerPrefs.SetInt("Lives", LevelManager.instance.currentLives);
        SceneManager.LoadScene(LevelSelect);
    }
}
