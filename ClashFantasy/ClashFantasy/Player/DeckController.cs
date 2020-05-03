using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour
{
    #region values
    string deckName = "";
    public team Team = team.Ateam;
    //Deck
    List<Card> deck = new List<Card>();
    //マナー
    int mp = 1;
    int maxMp = 10;
    //マナー回復
    float recoverTime = 1;
    float recoverCounter = 1;
    //マナーバー
    [SerializeField]
    Slider s = null;
    //選択されたカード
    Card selectedCard=null;
    //自分の土
    [SerializeField]
    LayerMask myGround=0;
    //ハンド
    [SerializeField]
    UIButton[] buttons = null;
    UIButton selectedButton = null;
    public List<Card> hand = new List<Card>();
    //player references
    Player player = null;
    Player enemy = null;
    //
    int cardIndex = 0;
    [SerializeField]
    Text manaText = null;
    Vector3 spawnposition;
    bool gameEnd = false;
    public bool GAMEEND { get { return gameEnd; } set { gameEnd = value; } }
    int manaRegeneration = 1;
    #endregion
    void Start()
    {
        UIButton[] b = GetComponentsInChildren<UIButton>();
        //デック種類
        if (PlayerPrefs.HasKey(StaticStrings.savedDeck))
        {
            deckName = PlayerPrefs.GetString(StaticStrings.savedDeck);
        }
        else
        {
            deckName = StaticStrings.holy;
        }
        //カードをロードする
        Card[] c = Resources.LoadAll<Card>("Decks/"+ deckName);
        foreach(var item in c)
        {
            deck.Add(item);
        }
        //デックを混ぜる
        shuffle();
        s.maxValue = maxMp;
        //プレーヤーのチームの設定
        Player[] allplayers = FindObjectsOfType<Player>();
        foreach(var p in allplayers)
        {
            if (p.thisTeam == Team)
            {
                player = p;
            }
            else
            {
                enemy = p;
            }
        }
    }
    void shuffle()
    {
        List<int> numbers = new List<int>();
        //カードを設定する
        while (numbers.Count < 4)
        {
            int rnd = Random.Range(0, deck.Count - 1);
            if (!numbers.Contains(rnd))
            {
                numbers.Add(rnd);
            }
        }
        //ボータンにカードを渡す
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].giveInformation(deck[numbers[i]]);
            buttons[i].init(this);
            hand.Add(deck[numbers[i]]);
        }
    }
    //情報を渡す
    public void selectCard(Card _c,UIButton b)
    {
      selectedButton = b;
      selectedCard =_c; 
    }
    
    void Update()
    {
        if (gameEnd) return;
        //マナーupdate
        if (s != null)
        {
          s.value = mp;
        }
        //マナーがあったらカードを使う
        if (selectedCard != null && selectedCard.cost <= mp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
                RaycastHit hitInfo;
                if(Physics.Raycast(ray,out hitInfo))
                {
                    Transform t = hitInfo.collider.GetComponent<Transform>();
                    Evoche(hitInfo.point,t);
                }
            }
        }
        //マナー回復
        if (mp < maxMp)
        {
            recoverCounter -= Time.deltaTime;
            if (recoverCounter <= 0)
            {
                recoverCounter = recoverTime;
                mp+=manaRegeneration;
            }
            
            
        }
        //テキストupdate
        if (manaText != null)
        {
            if (mp < 10)
            {
                manaText.text = mp.ToString();
            }
            else
            {
                manaText.text = "1" + "\n" + "0";
            }
           
        }
    }
    //モンスターを作る
    void Evoche(Vector3 pos,Transform t)
    {
      //魔法カードの場合は
        if(selectedCard is SpellCard)
        {
           if (t == null&&!t.GetComponent<Health>()) return;
           
           GameObject newCard = Instantiate(selectedCard.prefab[0], player.getKingTower().transform.position, Quaternion.identity) as GameObject;
            //カードに値を渡す
           AreaBullet b= newCard.GetComponent<AreaBullet>();
           b.passValues(player.thisTeam, selectedCard.spellCard.radius, selectedCard.spellCard.areaDamage, selectedCard.spellCard.crownTowerDamage, t);
        }
        else if(selectedCard is BuildCard)
        {
            GameObject newCard = Instantiate(selectedCard.prefab[0], spawnposition, Quaternion.identity) as GameObject;
            player.addToEnemyList(newCard.transform);
            Build c = newCard.GetComponent<Build>();
            if (c != null)
            {
                c.Init(Team);
            }
        }
        else
        {
            float x = 0;
            for(int i=0;i< selectedCard.prefab.Length;i++)
            {
                if (i < 1)
                {
                    spawnposition = pos;
                }
                GameObject newCard = Instantiate(selectedCard.prefab[i], spawnposition, Quaternion.identity) as GameObject;
                player.addToEnemyList(newCard.transform);
                //複数キャラクターの場合は、スポンポジションをを変わる
                Character c = newCard.GetComponent<Character>();
                if (c != null)
                {
                    c.Initialization(enemy);
                }
                if (i % 2 == 0)
                {
                    x = 1;       
                }
                else if (i % 3 == 0)
                {
                    x = -1; 
                }
                else
                {
                    x = 0;
                }
                spawnposition = new Vector3(newCard.transform.position.x + x, newCard.transform.position.y, newCard.transform.position.z);
            }
            
            
        }
        mp -= selectedCard.cost;
        selectedCard = null;
      　　Invoke("drawNewCard",1);
    }

    void drawNewCard()
    {
       Card newCard = null;
        cardIndex++;
        cardIndex %= deck.Count;
        newCard = deck[cardIndex];
        //同じカードが出ないように
        bool isDifferent = false;
        //ランダムでカードを引く
        while (!isDifferent)
        {
            foreach(var h in hand)
            {
                if (deck[cardIndex].name != h.name)
                {
                    isDifferent = true;
                    newCard = deck[cardIndex];
                }
                else
                {
                    isDifferent = false;
                    int rand = Random.Range(0, deck.Count);
                    cardIndex = rand;
                }
            }
            
        }
        //情報を渡す
       selectedButton.giveInformation(newCard);
        selectedButton = null;
        for(int i=0;i<hand.Count;i++)
        {
            hand[i] = buttons[i].getCard();
        }
    }
    //プレーヤーを渡す
    public void givePlayer(Player p)
    {
        player = p;
    }
    //upgrade
    public void upgradeManaRegeneration()
    {
        manaRegeneration++;
    }
}

public enum Decks
{
    holy,
    dark
}