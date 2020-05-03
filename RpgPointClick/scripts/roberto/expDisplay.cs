using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expDisplay : MonoBehaviour
{
    experience exp;
    private void Awake()
    {
        exp = GameObject.FindGameObjectWithTag("Player").GetComponent<experience>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = string.Format("{0:0}", exp.GetPoints());
    }
}
