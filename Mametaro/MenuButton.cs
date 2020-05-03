using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class MenuButton : MonoBehaviour
{
    public buttonType type=buttonType.loadScene;
    public delegate void buttonAction();
    public buttonAction newAction;
    
    private void Start()
    {
        buttonSettings();
    }
   //外から呼び出すために
    public void Click()
    {
        newAction();
    }
    //switchで関数の中身を変更する
    public void buttonSettings()
    {
        switch (type)
        {
            case buttonType.loadScene:
                newAction = loadScene;
                break;
            case buttonType.applicationQuit:
                newAction = quit;
                break;
            case buttonType.Pause:
                newAction = pause;
                break;
        }
    }

    void loadScene(){ Debug.Log("loadScene"); }
    void quit(){ Debug.Log("quit"); }
    void pause() { Debug.Log("pause"); }
}
public enum buttonType
{
    loadScene,
    applicationQuit,
    Pause,
}
/* public void giveAction(buttonAction ac)
    {
        newAction = ac;
    }*/
