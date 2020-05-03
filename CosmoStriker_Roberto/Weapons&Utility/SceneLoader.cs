using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public string mainMenu;
    public void loadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
