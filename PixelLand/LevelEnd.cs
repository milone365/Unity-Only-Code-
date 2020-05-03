using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
    private CameraController camCon;
    public string levelToLoad;
    public string levelToUnlock;
    public PlayerController player;
    public bool canMove;
    
	// Use this for initialization
	void Start () {
        camCon = FindObjectOfType<CameraController>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            PlayerController.instance.rb.velocity = new Vector3(5f, 0f, 0f);
        }
	}
    public IEnumerator loadLevelCo()
    {   
        PlayerController.instance.canMove = false;
        LevelManager.instance.invincible = true;
        camCon.followTarget = false;
        yield return new WaitForSeconds(0.5f);
        LevelManager.instance.levelMusic.Stop();
        LevelManager.instance.bossMusic.Stop();
        LevelManager.instance.gameOverMusic.Play();
        PlayerPrefs.SetInt(levelToUnlock, 1);
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        PlayerPrefs.SetInt("CoinCount", LevelManager.instance.coinCout);
        PlayerPrefs.SetInt("Lives", LevelManager.instance.currentLives);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelToLoad);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            StartCoroutine(loadLevelCo());
            
        }
    }
}
