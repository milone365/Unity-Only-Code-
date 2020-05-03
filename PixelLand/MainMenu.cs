using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public string level1, levelselect;
    public int startingLives = 3;
    public string[] LevelNames;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GameStart()
    {
        
        for(int i = 0; i < LevelNames.Length; i++)
        {
            PlayerPrefs.SetInt(LevelNames[i], 0);
        }
        SceneManager.LoadScene(level1);
        PlayerPrefs.SetInt("CoinCount",0);
        PlayerPrefs.SetInt("Lives", startingLives);
        
    }
   public  void QuitGame()
    {
        Application.Quit();
    }
   public  void Continue()
    {
        SceneManager.LoadScene(levelselect);
    }
}
