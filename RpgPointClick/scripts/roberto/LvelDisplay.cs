using rpg.Stato;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvelDisplay : MonoBehaviour
{
    BaseStats bstat;
    private void Awake()
    {
         bstat = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = string.Format("{0:0}",bstat.GetLevel());
    }
}
