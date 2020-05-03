using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
   
    Text winningMessage = null;
    AI cpu = null;
    DeckController playerDeck=null;
    Player[] allPlayers = null;
    Text ClockTXT;
    float TimerCount = 180;
    bool gameEnd = false;
    PlayerSta playerA, playerB;
    FadeScreen fadeScreen = null;
    GameObject endPanel;
    void Start()
    {
        //終了画面を消す
        endPanel = GameObject.Find("EndPanel");
        endPanel.SetActive(false);
        //fadescreeを消す
        fadeScreen = GetComponentInChildren<FadeScreen>();
        fadeScreen.fadeOut();
        //
        playerDeck = FindObjectOfType<DeckController>();
        cpu = FindObjectOfType<AI>();
        //プレーヤーのチームを渡す
        allPlayers = FindObjectsOfType<Player>();
        foreach(var p in allPlayers)
        {
            if (p.thisTeam == team.Ateam)
            {
                playerA.player = p;
            }
            else
            {
                playerB.player = p;
            }
        }
        //時計、勝つメッセージ、音楽Play
        winningMessage = GameObject.Find("WinMessage").GetComponent<Text>();
        ClockTXT = GameObject.Find("Clock").GetComponent<Text>();
        if(SoundManager.instance!=null)
        SoundManager.instance.playMusic("duel");
    }
    private void Update()
    {
        //時計のアップデート
        if (ClockTXT == null||gameEnd) return;
        TimerCount -= Time.deltaTime;
        //分と秒
        string minutes = ((int)TimerCount / 60).ToString();
        string seconds= ((int)TimerCount % 60).ToString();
        ClockTXT.text = minutes + ":" + seconds;
        if (TimerCount <= 0)
        {
            pointCheck();
        }
    }

    //プレーヤーのポイントを確認する
    private void pointCheck()
    {
        //if draw give Extra Time ad duplicate mana regeneration
        if (playerA.point == playerB.point)
        {
            TimerCount += 60;
            duplicateMpRegeneration();
        }
        else
        {
            //勝ちを決める
            if (playerA.point > playerB.point)
            {
                LoseTeam(team.Bteam);
            }
            else
            {
                LoseTeam(team.Ateam);
            }
        }
    }
    //マナー回復は二倍になる
    private void duplicateMpRegeneration()
    {
        cpu.upgradeManaRegeneration();
        playerDeck.upgradeManaRegeneration();
    }

    //処理を止まる
    public void endGame()
    {
        cpu.GAMEEND = true;
        playerDeck.GAMEEND = true;
        gameEnd = true;
    }
    public void addPointToPlayer(team tm)
    {
        //反対チームにポイントをあげます
        if (tm != team.Ateam)
        {
            playerA.point++;
        }
        else
        {
            playerB.point++;
        }
    }
    //負けたチームを渡す
    public void LoseTeam(team tm)
    {
        endGame();
        string message="";
        if (tm == team.Ateam)
        {
             message = "You Lose";
        }
        else
        {
            message = "You Win";
        }
        winningMessage.text = message;
        Invoke("ExitfromGame", 3);
    }
    //終了パネル
    void ExitfromGame()
    {
        winningMessage.text = "";
        endPanel.SetActive(true);
    }
}
struct PlayerSta
{
   public Player player;
   public int point;
}