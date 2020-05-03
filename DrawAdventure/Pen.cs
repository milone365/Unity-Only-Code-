using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public GameObject dotPrefab;
    bool gameover = false;
    // Update is called once per frame
    void Update()
    {
        if (gameover) return;
        if(Input.GetMouseButton(0))
        {
            //マウス座標のところで球を作る
            Vector2 objPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(dotPrefab,objPos, Quaternion.identity);
        }
    }
    public void gameOver()
    {
        gameover = true;
    }
}
