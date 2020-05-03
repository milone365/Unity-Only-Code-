using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string sceneToload = "";

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneToload);
    }
}
