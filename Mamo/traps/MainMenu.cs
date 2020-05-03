using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public void NewGame()
    {
        PlayerPrefs.SetInt("save", 0);
        SceneManager.LoadScene(levelToLoad);
    }

    public void loadGame()
    {
        SceneManager.LoadScene("FirstScene");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
