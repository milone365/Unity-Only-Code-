using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
    [SerializeField] Text dmText;
    
    void Start()
    {
        dmText.text = "";
    }

    public void onDamageEnter(float damage)
    {
        dmText.text = damage.ToString();
        GetComponent<Animator>().SetTrigger("txt");
    }
}
