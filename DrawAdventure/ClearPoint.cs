using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPoint : MonoBehaviour
{
    public string nextLevelName = "";
    
    //クリアポイントについて終わり

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            GameManager.instance.loadNewLevel(nextLevelName);
        }
    }
}
