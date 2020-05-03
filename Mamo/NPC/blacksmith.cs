using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class blacksmith : MonoBehaviour
{
    public static blacksmith instance;
    public Button firstbutton;
    public int[] disponibleItems;
    public string[] type;
    public int[] prices;
    public Sprite blankImage;
    public ItemsButtonsICONS[] icons;
    public tradeButton[] buttons;
    public Sprite[] itemImages;
    public int[] numberOfItemRequired;
    public string[] description;
    private bool menuIsActive=false;
    public GameObject tradePanel;
    public GameObject[] objectToSPAWN;
    public GameObject _bagPanel;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("save") == 1)
        {
            for (int i = 0; i < disponibleItems.Length; i++)
            {
               disponibleItems[i]= PlayerPrefs.GetInt("blackSmithItems" + i.ToString());
            }
        }


        for (int i = 0; i < buttons.Length; i++)
        {
            
            buttons[i].GetComponent<tradeButton>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menuClose();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Fire1"))
        {
            menuOpen();
        }   
    }

    public void menuClose()
    {
        tradePanel.SetActive(false);
        _bagPanel.SetActive(false);
        PlayerController.instance.Stopmove = false;
        Cursor.visible = false;
    }
    public void removeItem(int valor)
    {
        disponibleItems[valor]--;
        if (disponibleItems[valor] <= 0) { icons[valor].GetComponent<Image>().sprite = blankImage; }
    }
    public void menuOpen()
    {
        if (!menuIsActive)
        {
            tradePanel.SetActive(true);
            _bagPanel.SetActive(true);
            
            bagPanel.instance.UpdateBagPanel();
            for(int i = 0; i < itemImages.Length; i++)
            {
                if (disponibleItems[i] > 0)
                {
                    icons[i].GetComponent<Image>().sprite = itemImages[i];
                }
                
            }
            for (int i = 0; i < buttons.Length; i++)
            {
              buttons[i].getItemInfo(type[i],prices[i],objectToSPAWN[i],numberOfItemRequired[i],description[i]);
                
            }
            firstbutton.GetComponent<Button>().Select();
            PlayerController.instance.Stopmove = true;
            Cursor.visible = true;
        }
    }
    public void saveDisponibleItems()
    {
        for(int i = 0; i < disponibleItems.Length; i++)
        {
            PlayerPrefs.SetInt("blackSmithItems" + i.ToString(), disponibleItems[i]);
        }
    }
}
