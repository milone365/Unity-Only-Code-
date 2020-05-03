using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    public bool bossActive;
    public static BossController instance;
    public float timeBetweenDrops;
    public float timeBetwenDropStore;
    private float dropCount;

    public float waitToPlatforms;
    private float platFormCount;

    public Transform leftPoint;
    public Transform rightPoint;
    public Transform dropSawSpawnpoint;

    public GameObject DropSaw;
    public GameObject theBoss;
    public bool bossRight;

    public GameObject rightPlatform;
    public GameObject leftPlatform;

    public bool takeDamage;
    public int startingHealth;
    [SerializeField] private int currentHealth;
    public GameObject levelExit;
    private CameraController cameracon;
    public bool waitingForRespawn;
    public bool musicIsPlaing=false;
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        timeBetwenDropStore = timeBetweenDrops;
        dropCount = timeBetweenDrops;
        platFormCount = waitToPlatforms;
        currentHealth = startingHealth;
        cameracon = FindObjectOfType<CameraController>();
        theBoss.transform.position = rightPoint.position;
        bossRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (LevelManager.instance.respawnCoActive)
        {
            bossActive = false;
            waitingForRespawn = true;
            

        }
        if (waitingForRespawn && !LevelManager.instance.respawnCoActive)
        {
            theBoss.SetActive(false);
            leftPlatform.SetActive(false);
            rightPlatform.SetActive(false);
            platFormCount = waitToPlatforms;
            dropCount = timeBetweenDrops;
            timeBetweenDrops = timeBetwenDropStore;
            theBoss.transform.position = rightPoint.position;
            bossRight=true;
            currentHealth = startingHealth;
            cameracon.followTarget = true;
            waitingForRespawn = false;
        }
        if (bossActive)
        {
            if (!musicIsPlaing)
            {
                LevelManager.instance.levelMusic.Stop();
                LevelManager.instance.bossMusic.Play();
                musicIsPlaing = true;
            }
            
            cameracon.followTarget = false;
            cameracon.transform.position = Vector3.Lerp(cameracon.transform.position, new Vector3(transform.position.x, cameracon.transform.position.y, cameracon.transform.position.z), cameracon.smoothing * Time.deltaTime);
            theBoss.SetActive(true);
            if (dropCount > 0)
            {
                dropCount -= Time.deltaTime;
            }
            else
            {
                dropSawSpawnpoint.position = new Vector3(Random.Range(leftPoint.position.x, rightPoint.position.x), dropSawSpawnpoint.position.y, dropSawSpawnpoint.position.z);
                Instantiate(DropSaw, dropSawSpawnpoint.position, dropSawSpawnpoint.rotation);
                dropCount = timeBetweenDrops;
            }
            if (bossRight)
            {
                if (platFormCount > 0)
                {
                    platFormCount -= Time.deltaTime;
                }else
                { rightPlatform.SetActive(true);}
            }
            else
            {
                if (platFormCount > 0)
                {
                    platFormCount -= Time.deltaTime;
                }
                else
                { leftPlatform.SetActive(true); }
            }
            if (takeDamage)
            {
                currentHealth--;
                if (currentHealth <= 0)
                {
                    levelExit.SetActive(true);
                    bossActive = false;
                    cameracon.followTarget = true;
                    gameObject.SetActive(false);
                }
               if (bossRight){ theBoss.transform.position = leftPoint.position; }
                else { theBoss.transform.position = rightPoint.position; }
                bossRight =! bossRight;
                rightPlatform.SetActive(false);
                leftPlatform.SetActive(false);
                platFormCount = waitToPlatforms;
                timeBetweenDrops = timeBetweenDrops / 2;
                takeDamage = false;
            }
        }
        
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        { bossActive = true;}
    }
}
