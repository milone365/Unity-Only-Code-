using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;
    public float waittoRespown = 2f;
    private PlayerController player;
    public GameObject deathExplosion;
    public int coinCout = 0;
    public Text coinText, livesText;
    public GameObject gameOverScreen;
    public int Sum;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;
    public bool responing = false;
    public int maxHealt = 6;
    public int healthCount;
    public ResetOnRespawn[] objectsToReset;
    public bool invincible;
    public int startingLives = 3;
    public int currentLives;
    public AudioSource gameOverMusic, levelMusic;
    public bool respawnCoActive = false;
    public AudioSource bossMusic;
    public AudioSource splatSound;


    private void Awake()
    {
        instance = this;
    }
    // Use this for initializatfion
    void Start() {
        invincible = false;
        player = FindObjectOfType<PlayerController>();
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();
        healthCount = maxHealt;
        if (PlayerPrefs.HasKey("CoinCount"))
        {
           coinCout= PlayerPrefs.GetInt("CoinCount");
        }
        if (PlayerPrefs.HasKey("Lives"))
        {
           currentLives= PlayerPrefs.GetInt("Lives");
        }
        else
        {
            currentLives = startingLives;
        }
        
        coinText.text = "Coins: " + coinCout;
        livesText.text = "X " + currentLives;
        UpdateHeartMeter();
    }

    // Update is called once per frame
    void Update() {


        if (healthCount <= 0 && !responing)
        {
            responing = true;
            respawn();
        }
    }
    public void respawn()
    {
        currentLives--;
        livesText.text = "X " + currentLives;
        
        if (currentLives > 0) {
            
            StartCoroutine(respawnCo());
        }
        else
        {
            bossMusic.Stop();
            levelMusic.Stop();
            gameOverMusic.Play();
            player.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);

        }
    }
    public IEnumerator respawnCo()
    {
        respawnCoActive = true;
        player.gameObject.SetActive(false);
        Instantiate(deathExplosion, player.transform.position, player.transform.rotation);
        player.resetScale();
        yield return new WaitForSeconds(waittoRespown);
        respawnCoActive = false;
        healthCount = maxHealt;
        UpdateHeartMeter();
        responing = false;
        player.transform.position = player.respawnPosition;
        
        player.gameObject.SetActive(true);
        

        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }
        
        
    }
    
    
    public void addCoin(int coinsToAdd)
    {
        PlayerController.instance.pickUpSound.Play();
        coinCout += coinsToAdd;
        if (coinCout >= 100)
        {
            Addlife();
            Sum = coinCout;
            Sum -= 100;
             coinCout = Sum;
        }
        coinText.text = "Coins: " + coinCout;
    }
    public void hurtPlayer(int damageToTake)
    {
        if (!invincible)
        {
            healthCount -= damageToTake;
            UpdateHeartMeter();
            player.KnockBack();

            
        }
    }
    public void UpdateHeartMeter()
    {
       
        switch (healthCount)
        {
            case 6: heart1.sprite = heartFull;
                    heart2.sprite = heartFull;
                    heart3.sprite = heartFull;
                    return;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite =  heartEmpty;
                return;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;
            case 2:
                heart1.sprite =  heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }
   
    public void Addlife()
    {
        currentLives++;
        livesText.text = "X " + currentLives;
    }
    public void giveHealt( int healtToGive)
    {
        PlayerController.instance.powerUp.Play();
        healthCount += healtToGive;
        if (healthCount > maxHealt)
        {
            healthCount = maxHealt;
        }
        UpdateHeartMeter();
    }
}
