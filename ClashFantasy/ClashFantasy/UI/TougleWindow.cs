using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TougleWindow : MonoBehaviour
{
    public GameMode mode;
    int value = 0;
    //難しさの設定
    private void Start()
    {
        
        switch (mode)
        {
            case GameMode.ToEasy:
                value =(int)GameMode.ToEasy;
                break;
            case GameMode.Easy:
                value = (int)GameMode.Easy;
                break;
            case GameMode.Normal:
                value = (int)GameMode.Normal;
                break;
            case GameMode.Hard:
                value = (int)GameMode.Hard;
                break;
            case GameMode.Veryharad:
                value = (int)GameMode.Veryharad;
                break;
            case GameMode.Crazy:
                value = (int)GameMode.Crazy;
                break;
            case GameMode.Impossible:
                value = (int)GameMode.Impossible;
                break;
        }
    }
    //選択された難しさを保存する
    public void selectThis()
    {
        PlayerPrefs.SetInt(StaticStrings.gameMode, value);
    }
}
