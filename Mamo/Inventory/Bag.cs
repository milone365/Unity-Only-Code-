using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public Vector3 shotDirection = Vector3.zero;
    public float range;
    public bool crossHairIsActive = true;
    public Image crossHair;
    public int bombs, daggers, hppotionNumber, mppotionNumber;
    private string bomb_name = "bomb", dagger_name = "dagger", potion_name = "healingPotion", mpPotion_name = "manaPotion";
    public GameObject pickPrefab;
    public static Bag instance;
    public int tresuareKey;
    private Image trowBox, potionBox;
    public Sprite bombImg, daggerImg, hppotionImg, manapotionImg, shieldImage, pickImage;
    private bool potionActive = false, manapotionActive, pickActive;
    public Text ammotext, potiontext;
    public Sprite BlanckImage;
    public int manaPotionPower = 3, healingPotionPower = 3, pickNumber;
    public int ammo, reserve;
    private bool daggerActive = false, bombActive = false;
    public Bomb _bomb;
    public dagger _DAGGER;
    public float bulletSpeed = 500f;
    public Transform armSpawner;
    public Quaternion spawnerStartPos;
    public bool haveAObject;
    public int equipped = 0, selected = 0;
    [Header("TradableObject")]
    public int gem, wood, iron;
    public int herbs, mushrooms, flowers;
    public int gears = 0;
    private string _herbs="herbs", _mushrooms="mushrooms", _flowers="flowers";
    public GameObject manapotionPrefab, LifepotionPefab;
    public Text middleImageText;
    public Image middleImage;
    public Sprite gearImage;
    public Sprite goldIcon, IronIcon, FlowerIcon, HerbIcon, DiamondIcon, MushRoomSIcon;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        armSpawner = GameObject.Find("armSpawner").GetComponent<Transform>();
        crossHairIsActive = true;
        spawnerStartPos = armSpawner.rotation;
        trowBox = GameObject.FindGameObjectWithTag("trowbox").GetComponent<Image>();
        potionBox = GameObject.FindGameObjectWithTag("potionbox").GetComponent<Image>();

       if (PlayerPrefs.HasKey("save")&&PlayerPrefs.GetInt("save")==1)
        {
            loadBag();
            changeConsumable();
            change();
       }
       
        UIManager.instance.UpdateUI();
        ammotext.text = "";
        potiontext.text = "";
        activeCrossHair();
        armSpawner.rotation = Quaternion.Euler(0, 0, 0);

    }
    public void restSpawnerPos()
    {
        armSpawner.rotation = spawnerStartPos;
    }
    public void activeCrossHair()
    {
        if (crossHairIsActive)
        {
            crossHairIsActive = false;
            crossHair.enabled = false;
            armSpawner.rotation = Camera.main.transform.rotation;

        }
        else
        {
            crossHairIsActive = true;
            crossHair.enabled = true;
            restSpawnerPos();
        }
    }
    private void Update()
    {
        use();
    }
    public void use()
    {
        if (crossHairIsActive)
        {
            
            armSpawner.rotation = Camera.main.transform.rotation;
        }
        if (Input.GetMouseButtonDown(2))
        {
            activeCrossHair();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)||Input.GetButtonDown("1"))
        {
            change();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)|| Input.GetButtonDown("2"))
        {
            changeConsumable();
        }
        if (ammo > 0 && (Input.GetKeyDown(KeyCode.Z)|| Input.GetButtonDown("trow")))
        {

            if (bombActive || daggerActive)
            {

                if (bombActive)
                {
                    bombs--;
                    ammo = bombs;

                    Bomb newBomb = Instantiate(_bomb, armSpawner.position, armSpawner.rotation) as Bomb;
                   // newBomb.speed = bulletSpeed;
                }
                else
                {
                    daggers--;
                    ammo = daggers;
                   dagger newDagger = Instantiate(_DAGGER, armSpawner.position, armSpawner.rotation) as dagger;
                   // newDagger.speed = bulletSpeed;

                }
                UIManager.instance.UpdateUI();
            }
        }

        if (reserve > 0 && (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("use")))
        {

            if (potionActive || manapotionActive)
            {

                if (potionActive)
                {
                    hppotionNumber--;
                    HealthManager.instace.HealPlayer(healingPotionPower);
                    reserve = hppotionNumber;

                }
                else
                {
                    mppotionNumber--;
                    GameManager.instace.restoreMana(manaPotionPower);
                    reserve = mppotionNumber;

                }

            }
        }
        UIManager.instance.UpdateUI();
    }

    public void usePick()
    {
        pickNumber--;
        reserve = pickNumber;
        if (reserve == 0)
        {
            potionBox.sprite = BlanckImage;
            pickPrefab.SetActive(false);
        }
        UIManager.instance.UpdateUI();
    }

    public void change()
    {
        equipped++;
        if (equipped > 2)
        {
            equipped = 0;
        }
        switch (equipped)
        {
            case 0:
                trowBox.sprite = shieldImage;
                bombActive = false;
                daggerActive = false;
                HealthManager.instace.activeShield();
                ammo = HealthManager.instace.shieldEndurance;
                checkIfISAmmoBlanck();
                UIManager.instance.UpdateUI();

                break;
            case 1:
                trowBox.sprite = bombImg;
                bombActive = true;
                daggerActive = false;
                HealthManager.instace.deactiveShield();
                ammo = bombs;
                checkIfISAmmoBlanck();
                UIManager.instance.UpdateUI();
                break;
            case 2:
                trowBox.sprite = daggerImg;
                ammo = daggers;
                checkIfISAmmoBlanck();
                bombActive = false;
                daggerActive = true;
                HealthManager.instace.deactiveShield();
                UIManager.instance.UpdateUI();


                break;
        }
    }

    public void checkIfISAmmoBlanck()
    {
        if (ammo == 0)
        {
            trowBox.sprite = BlanckImage;
        }
    }
    public void checkifnotHaveReserve()
    {
        if (reserve == 0)
        {
            potionBox.sprite = BlanckImage;
        }
    }
    public void changeConsumable()
    {
        selected++;
        if (selected > 2)
        {
            selected = 0;
        }
        switch (selected)
        {
            case 0:
                potionBox.sprite = pickImage;
                potionActive = false;
                manapotionActive = false;
                pickActive = true;
                pickPrefab.SetActive(true);
                reserve = pickNumber;
                if (reserve == 0)
                {
                    potionBox.sprite = BlanckImage;
                    pickPrefab.SetActive(false);

                }
                UIManager.instance.UpdateUI();

                break;
            case 1:
                potionBox.sprite = hppotionImg;
                potionActive = true;
                manapotionActive = false;
                pickActive = false;
                pickPrefab.SetActive(false);
                reserve = hppotionNumber;
                checkifnotHaveReserve();
                UIManager.instance.UpdateUI();
                break;
            case 2:
                potionBox.sprite = manapotionImg;
                reserve = mppotionNumber;
                potionActive = false;
                manapotionActive = true;
                pickPrefab.SetActive(false);
                pickActive = false;
                checkifnotHaveReserve();
                UIManager.instance.UpdateUI();


                break;
        }
    }
    public void removeItem(string objectToRemove, int valor)
    {

        if (objectToRemove == "key")
        {
            tresuareKey -= valor;
        }
    }

    public void Add(string objectToAdd, int valor)
    {
        if (objectToAdd == bomb_name)
        {
            bombs += valor;
            if (bombActive)
            {
                ammo = bombs;
                trowBox.sprite = bombImg;
            }

        }
        else if (objectToAdd == dagger_name)
        {
            daggers += valor;
            if (daggerActive)
            {
                ammo = daggers;
                trowBox.sprite = daggerImg;
            }

        }
        else if (objectToAdd == "key")
        {
            tresuareKey += valor;
        }
        else if (objectToAdd == potion_name)
        {
            hppotionNumber += valor;
            if (potionActive)
            {
                reserve = hppotionNumber;
                potionBox.sprite = hppotionImg;
            }

        }
        else if (objectToAdd == mpPotion_name)
        {
            mppotionNumber += valor;
            if (manapotionActive)
            {
                reserve = mppotionNumber;

                potionBox.sprite = manapotionImg;
            }
        }
        else if (objectToAdd == "shield")
        {
            HealthManager.instace.giveShield(valor);
            if (HealthManager.instace.shieldActive || equipped == 0)
            {
                ammo = HealthManager.instace.shieldEndurance;
                trowBox.sprite = shieldImage;
                if (equipped == 0)
                {
                    HealthManager.instace.activeShield();
                }

            }
        }
        else if (objectToAdd == "pick")
        {
            pickPrefab.SetActive(true);
            pickNumber += valor;
            if (pickActive || selected == 0)
            {
                reserve = pickNumber;
                potionBox.sprite = pickImage;
            }
        }
        UIManager.instance.UpdateUI();
    }

    public void addResource(string objectToAdd,int amount)
    {
        if (objectToAdd == "wood")
        {
            wood+=amount;
            addLittleImage(goldIcon, wood);
        }
        else if (objectToAdd == "gem")
        {
            gem += amount;
            addLittleImage(DiamondIcon, gem);
        }
        else if (objectToAdd == "iron")
        {
            iron += amount;
            addLittleImage(IronIcon, iron);
        }
        else if (objectToAdd == _mushrooms)
        {
            mushrooms += amount;
            addLittleImage(MushRoomSIcon, mushrooms);
        }
        else if (objectToAdd == _herbs)
        {
            herbs += amount;
            addLittleImage(HerbIcon, herbs);
        }
        else if (objectToAdd == _flowers)
        {
            flowers += amount;
            addLittleImage(FlowerIcon, flowers);
        }
        else if(objectToAdd == "gear")
        {
            gears += amount;
            addLittleImage(gearImage, gears);
        }
    }
    public void makeLifePotion(int Herbs,int secondElement)
    {
        if (herbs >= Herbs&&mushrooms>=secondElement)
        {
            herbs -= Herbs;
            mushrooms -= secondElement;
            Instantiate(LifepotionPefab, armSpawner.transform.position, armSpawner.transform.rotation);
            bagPanel.instance.UpdateBagPanel();
        }
        else
        {
            UIManager.instance.getShortMessage("you don't have elements");
        }
       
    }
    public void makeManaPotion(int Herbs, int secondElement)
    {
        if(herbs >= Herbs && mushrooms >= secondElement)
        {
            herbs -= Herbs;
            flowers -= secondElement;
            Instantiate(manapotionPrefab, armSpawner.transform.position, armSpawner.transform.rotation);
            bagPanel.instance.UpdateBagPanel();
        }
        else
        {
            UIManager.instance.getShortMessage("you don't have elements");
        }
        
    }

    public void SaveBagContents()
    {
        PlayerPrefs.SetInt("iron", iron);
        PlayerPrefs.SetInt("mushrooms", mushrooms);
        PlayerPrefs.SetInt("flowers", flowers);
        PlayerPrefs.SetInt("herbs", herbs);
        PlayerPrefs.SetInt("bombs", bombs);
        PlayerPrefs.SetInt("daggers", daggers);
        PlayerPrefs.SetInt("hppotion", hppotionNumber);
        PlayerPrefs.SetInt("mpPot", mppotionNumber);
        PlayerPrefs.SetInt("gem", gem);
        PlayerPrefs.SetInt("wood", wood);
        PlayerPrefs.SetInt("mpPot", mppotionNumber);
        PlayerPrefs.SetInt("mPotionPOw", manaPotionPower);
        PlayerPrefs.SetInt("hpP", healingPotionPower);
       PlayerPrefs.SetInt("selected", 0);
        PlayerPrefs.SetInt("equipped", 0);
        PlayerPrefs.SetInt("key", tresuareKey);
        PlayerPrefs.SetInt("pick", pickNumber);
        PlayerPrefs.SetInt("gears", gears);

    }
    public void loadBag()
    {
        gears = PlayerPrefs.GetInt("gears");
        iron = PlayerPrefs.GetInt("iron");
        mushrooms = PlayerPrefs.GetInt("mushrooms");
        flowers = PlayerPrefs.GetInt("flowers");
        herbs = PlayerPrefs.GetInt("herbs");
        bombs = PlayerPrefs.GetInt("bombs");
        daggers = PlayerPrefs.GetInt("daggers");
        hppotionNumber = PlayerPrefs.GetInt("hppotion");
        mppotionNumber = PlayerPrefs.GetInt("mpPot");
        gem = PlayerPrefs.GetInt("gem");
        wood = PlayerPrefs.GetInt("wood");
        mppotionNumber = PlayerPrefs.GetInt("mpPot");
        selected = PlayerPrefs.GetInt("selected");
        equipped = PlayerPrefs.GetInt("equipped");
        tresuareKey = PlayerPrefs.GetInt("key");
        pickNumber = PlayerPrefs.GetInt("pick");
        manaPotionPower = PlayerPrefs.GetInt("mPotionPOw");
        manaPotionPower = PlayerPrefs.GetInt("hpP");
    }

        public void addLittleImage(Sprite imageToGive,int amountCollected)
    {
            middleImage.sprite = imageToGive;
            middleImage.enabled = true;
            middleImageText.text = "" + amountCollected;
        }
}