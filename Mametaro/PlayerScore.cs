using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScore : MonoBehaviour
{
    Text scoreText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }
    //スコアを足す
    public void addScore(int v)
    {
        score += v;
        scoreText.text = score.ToString();
    }
}
