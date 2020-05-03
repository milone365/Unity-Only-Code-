using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public int force = 0;
    public GameObject alabard;
    private bool haveweapon;
    public int currentCoin;
    public static GameManager instace;
    public Vector3 respawnPoint;
    public bool comboActive, respawning;
    public int maxmp=5;
    public int currentmp;
    private float manaResCounter;
    private float manaRestoreTime=20f;
    public bool manaisFull;
    public bool manaIsFold = true;
    public GameObject manaBar;
    public int intellect = 0;
    ResetOnRespawn[] objectToreset;
    public bool weaponEquipped;
    Quaternion startRotation;
    public bool haveEgg=false,haveMinion=false;
    public GameObject minion;
    [SerializeField]
    private float eggTimer=120f;

    private void Awake()
    {
        instace = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        manaResCounter = manaRestoreTime;
        startRotation = PlayerController.instance.transform.rotation;
        
        respawnPoint = PlayerController.instance.transform.position;
        manaisFull=true;
        
        objectToreset = FindObjectsOfType<ResetOnRespawn>();
        if (PlayerPrefs.HasKey("save") && PlayerPrefs.GetInt("save") == 1)
        {
            LoadData();
        }
        else
        {
            currentCoin = 0;
            maxmp = 5;
        }
        weaponEquipped = false;
        currentmp = maxmp;
        UIManager.instance.UpdateUI();
    }
    public void haveLegendaryWeapons()
    {
       haveweapon=true;
        alabard.SetActive(true);
        weaponEquipped = true;
    }
    private void Update()
    {
        if (haveEgg)
        {
            eggTimer -= Time.deltaTime;
            if (eggTimer <= 0)
            {
                haveEgg = false;
                haveMinion = true;
                Instantiate(minion, new Vector3(PlayerController.instance.transform.position.x+0.5f, PlayerController.instance.transform.position.y+0.75f, PlayerController.instance.transform.position.z - 0.5f), PlayerController.instance.transform.rotation);
                UIManager.instance.getShortMessage("Dragon is Born!!!");
            }
        }
        if (haveweapon)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (!alabard.activeInHierarchy)
                {
                    alabard.SetActive(true);
                    weaponEquipped = true;
                }
                else
                {
                    alabard.SetActive(false);
                    weaponEquipped = false;
                }
                
            }
        }
        if (!manaisFull)
        {
            manaResCounter -= Time.deltaTime;
            if (manaResCounter <= 0)
            {
                manaResCounter = manaRestoreTime;
                currentmp++;
                manaIsFold = false;
                if (currentmp >= maxmp)
                {
                    manaisFull = true;
                    currentmp = maxmp;
                }
            }
        }
    }

   



    public void addIntellect()
    {
        intellect++;
    }
    public void Respawn()
    {
        StartCoroutine(resoawnCo());
    }
    public void addCoint(int coinToAdd)
    {
        currentCoin += coinToAdd;
        UIManager.instance.UpdateUI();
    }
    public IEnumerator resoawnCo()
    {
        respawning = true;
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
         for(int i = 0; i < objectToreset.Length; i++)
        {
            objectToreset[i].gameObject.SetActive(true);
            objectToreset[i].ResetObject();
        }
        PlayerController.instance.transform.position = respawnPoint;
        HealthManager.instace.restoreHealth();
        PlayerController.instance.transform.rotation = startRotation;
         PlayerController.instance.gameObject.SetActive(true);

        

    }
  
    public void setCheckPoint(Vector3 pointToSet)
    {
        respawnPoint = pointToSet;

    }
    public void restoreMana(int value)
    {
        currentmp += value;
        if (currentmp >= maxmp)
        {
            currentmp = maxmp;
        }
        UIManager.instance.UpdateUI();
    }
    
    public void upGradeManaPoint()
    {
        maxmp++;
        manaisFull = false;
    }
    public void addForce()
    {
        force++;
        }
    public void useMana(int amount)
    {
        currentmp -= amount;
        manaisFull = false;
        if (currentmp <= 0)
        {
            manaIsFold = true;
        }
    }
    public void LoadData()
    {
        intellect= PlayerPrefs.GetInt("intellect");
        
        currentCoin = PlayerPrefs.GetInt("coin");
        maxmp = PlayerPrefs.GetInt("mana");
        force= PlayerPrefs.GetInt("force");
        if (PlayerPrefs.GetInt("weapon") == 1)
        {
            haveweapon = true;
        }
        else
        {
            haveweapon = false;
        }
        if (PlayerPrefs.GetInt("combo") == 1)
        {
            comboActive= true;
        }
        else
        {
            comboActive = false;
        }
        if (PlayerPrefs.GetInt("minion") == 1)
        {
            haveMinion = true;
            Instantiate(minion, new Vector3(PlayerController.instance.transform.position.x + 0.5f, PlayerController.instance.transform.position.y + 0.75f, PlayerController.instance.transform.position.z - 0.5f), PlayerController.instance.transform.rotation);

        }
        else
        {
            haveMinion = false;
        }
    }
    public void SaveGame()
    {
        PlayerPrefs.SetInt("coin", currentCoin);
        PlayerPrefs.SetInt("mana", maxmp);
        PlayerPrefs.SetInt("intellect", intellect);
        
        PlayerPrefs.SetInt("force", force);
        if (comboActive)
        {
            PlayerPrefs.SetInt("combo", 1);
        }
       if(haveMinion)
        {
            PlayerPrefs.SetInt("minion", 1);
        }
        else
        {
            PlayerPrefs.SetInt("minion", 0);
        }
        
        if (haveweapon)
        {
            PlayerPrefs.SetInt("weapon",1);
        }
        else
        {
            PlayerPrefs.SetInt("weapon", 0);
        }
    }
   
}
