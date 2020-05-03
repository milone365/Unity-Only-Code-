using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spellBook : MonoBehaviour
{
    public static spellBook instance;
    public GameObject[] spells;
    public int spellSelected=0;
    public bool haveWeapon;
    public Sprite[] spellIMG;
    private Image spellbox;
    public Transform spellPawner;
    public static int _spellSelected;
    public alabardMaster alabard;
    public Sprite[] magicImages;
    public GameObject spellImage;
    public Image playerSpellImage;
    private void Start()
    {
        spellbox = GameObject.FindGameObjectWithTag("spellbox").GetComponent<Image>();
        alabard = GetComponent<alabardMaster>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("3"))
        {
            changeSpell();
        }
        if (!GameManager.instace.manaIsFold && (Input.GetKeyDown(KeyCode.C)||Input.GetButtonDown("spell")))
        {
               if (GameManager.instace.weaponEquipped)
            {
                if (GameManager.instace.currentmp >= (spellSelected * 3))
                {
                    checkSpellImage();
                    if (spellSelected == 4)
                    {
                        spells[4].SetActive(true);
                        HealthManager.instace.giveICEShield(10);
                    }else if(spellSelected == 2)
                    {
                        GameObject spell = Instantiate(spells[spellSelected],new Vector3( spellPawner.position.x, spellPawner.position.y, spellPawner.position.z), Quaternion.identity);
                    }
                    else
                    {
                        GameObject spell = Instantiate(spells[spellSelected], spellPawner.position, spellPawner.rotation);
                    }
                    
                    GameManager.instace.useMana(spellSelected * 3);


                    UIManager.instance.UpdateUI();
                }
                else
                {
                    UIManager.instance.getShortMessage("mana is too lower");
                }
            }
           

        }
    }
  
    public void checkSpellImage()
    {
        switch (spellSelected)
        {
            case 1: spellImage.SetActive(true);playerSpellImage.sprite = magicImages[1]; break;
            case 2: spellImage.SetActive(true); playerSpellImage.sprite = magicImages[2]; break;
            case 3: spellImage.SetActive(true); playerSpellImage.sprite = magicImages[3]; break;
            case 4: spellImage.SetActive(true); playerSpellImage.sprite = magicImages[4]; break;
            case 5: spellImage.SetActive(true); playerSpellImage.sprite = magicImages[5]; break;
            default: playerSpellImage.sprite = magicImages[0]; break;
        }
    }
    public void changeSpell()
    {
        spellSelected++;
        _spellSelected = spellSelected;
        if (GameManager.instace.weaponEquipped)
        {
            alabard = FindObjectOfType<alabardMaster>();
                alabard.activeAlabardParticle(spellSelected);
        }
        if (spellSelected >= GameManager.instace.intellect)
        {
            spellSelected = 0;
        }

        switch (spellSelected)
        {
            case 0:
                spellbox.sprite = spellIMG[spellSelected];
                break;
            case 1:
                spellbox.sprite = spellIMG[spellSelected];
                break;
            case 2:
               spellbox.sprite = spellIMG[spellSelected];
                break;
            case 3:
                spellbox.sprite = spellIMG[spellSelected];
                break;
            case 4:
                spellbox.sprite = spellIMG[spellSelected];
                break;
            case 5:
                spellbox.sprite = spellIMG[spellSelected];
                break;
        }
    }
}
    
