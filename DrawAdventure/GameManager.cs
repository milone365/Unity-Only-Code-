using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    bool gameClear = false;
    public bool enduranceMode = false;
    bool gameOver=false;
    EnduranceGameMode endurance;
    bool canStart = false;
    [SerializeField]
    GameObject[] hazardPrefabs=null;
    int score = 0;
    [SerializeField]
    int lives = 3;
    public static GameManager instance;
    [SerializeField]
    Text scoreTxt=null, lifeTxt=null,startMessage=null;
    Transform player;
    Vector2 startPosition = Vector2.zero;
   
   
    List<GameObject> traplist= new List<GameObject>();
   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //文書
        scoreTxt.text = "Score: " + score.ToString();
        lifeTxt.text = "Lives: "+lives.ToString();
        //プレーヤーreference
        player = GameObject.Find("Player").transform;
        if (player == null) return;
        startPosition = player.position;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        //ゲームモード無限
        if (!enduranceMode) return;
        endurance = new EnduranceGameMode(player, hazardPrefabs.Length);
    }
    private void Update()
    {
        if (!canStart && !gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //玉が動けるようにｙ座標自由にする
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                canStart = true;
            }
        }
        if (startMessage != null)
        {
            updateMessage();
        }
        if (!enduranceMode)
        {
            return;
        }
        else
        {
            //なければ作る
            if(endurance==null)
            {
                endurance = new EnduranceGameMode(player, hazardPrefabs.Length);
            }
            endurance.GameUpdate();
        }
         
    }
    //スコアアップ
    public void AddScore(int point)
    {
        score += point;
        scoreTxt.text = "Score: "+ score.ToString();
    }

    //命をエラス
    public int loseLife()
    {
        lives = Mathf.Max(0, lives - 1);
        lifeTxt.text = "Lives: "+lives.ToString();
        if (lives<=0)
        {
            resetGame();
            GameOver();
        }
        else
        {
            resetGame();
        }
        return lives;
    }
    //状況によってテキストは違う
    void updateMessage()
    {
        if(gameClear)
        {
            startMessage.text = "ClearLevel";
            return;
        }
        if(gameOver)
        {
            startMessage.text = "GameOver";
            startMessage.color = Color.red;
            return;
        }

        if (canStart)
        {
            startMessage.text = "";
        }
        else
        {
            startMessage.text = "Press Space for Start";
        }
    }
    //ゲームオーバ
    void GameOver()
    {
        gameOver = true;
        Pen pen = FindObjectOfType<Pen>();
        pen.gameOver();
    }

    //ギミックをスポンする
    public void spawnHazard(int rnd,Vector2 spawnPos)
    {
        GameObject h = Instantiate(hazardPrefabs[rnd], spawnPos, Quaternion.identity) as GameObject;
        traplist.Add(h);
    }
    //最初のポイントに戻って、ワナのリストを消す
    public void resetGame()
    {
        if (player == null) return;
        player.position = startPosition;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        endurance.oldPosition = player.position;
        foreach(GameObject g in traplist)
        {
            Destroy(g);
        }
        traplist.Clear();
        canStart = false;
    }
    //次のステージへ行く
    public void loadNewLevel(string s)
    {
        gameClear = true;
        Pen p = FindObjectOfType<Pen>();
        p.gameOver();
        StartCoroutine(goToNextLevel(s));
    }
    IEnumerator goToNextLevel(string nextLevelName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextLevelName);
    }
}

