using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    //シーンロード
    public void loadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
    //ゲーム終了
    public void Exit()
    {
        Application.Quit();
    }
}
