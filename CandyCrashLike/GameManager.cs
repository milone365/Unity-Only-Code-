using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int score = 0;
    public int matches = 44;
    public int maxScore = 1000;
    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        UIManager.instace.updateMatch(matches);
        UIManager.instace.UpdateScore(score);
        UIManager.instace.setStarMeter(maxScore);
    }
    public void addScore(int value)
    {
        score += value;
        UIManager.instace.UpdateScore(score);
    }

    public void updateMatches()
    {
        matches--;
        UIManager.instace.updateMatch(matches);
        if (matches <= 0)
        {
            UIManager.instace.activeEndPane(score);
            StartCoroutine(ActiveStar());
        }
    }
    IEnumerator ActiveStar()
    {
        yield return null;
        if (score > (float)maxScore * 0.25f)
        {
            UIManager.instace.ActiveStar("copper");
        }
        if(score>(float)maxScore* 0.65f)
        {
            UIManager.instace.ActiveStar("silver");
        }
        if (score > (float)maxScore * 0.9f)
        {
            UIManager.instace.ActiveStar("gold");
        }
    }
   
}
