using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int hp = 100;
    private int power = 50;
    public void attack()
    {
        Debug.Log(this.power + "damage");
    }
    public void damage(int damageToGive)
    {
        this.hp -= damageToGive;
        Debug.Log(damageToGive + "taken Damage");
    }
    public Vector2 getPosition(Vector2 position)
    {

        return position;
    }
}

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player myplayer = new Player();
        Vector2 playerPos = new Vector2(3.0f, 4.0f);
        playerPos.x += 8f;
        playerPos.y += 5f;
        Debug.Log(myplayer.getPosition(playerPos));
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
