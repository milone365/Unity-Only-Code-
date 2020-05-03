using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ButtonNavigator : MonoBehaviour
{
    MenuButton [] buttonList;
     List<Delegate> allFunctions;
    
    float upValue = 0;
    int buttonIndex = 0;
    bool stop = false;

    bool up_Input()
    {
       　upValue = upValue > 0 ? 1 : upValue;
        return upValue > 0;
    }
    bool down_Input()
    {
        
        upValue = upValue < 0 ? -1 : upValue;
        return upValue < 0;
    }

    bool A_input()
    {
        return Input.GetButtonDown("Fire1");
    }
    
 
    void Start()
    {
        //buttonを探す
        buttonList = GetComponentsInChildren<MenuButton>();
        buttonList[buttonIndex].GetComponent<Button>().Select();
    }

    
    void Update()
    {
        upValue = Input.GetAxis("Vertical");
        if (up_Input())
        {
            if (!stop)
            {
                stop = true;
                buttonIndex++;
                //lengthを乗り越えられないように
                buttonIndex %= buttonList.Length;
                //button Hilighted
                buttonList[buttonIndex].GetComponent<Button>().Select();
            }
        }
        if(down_Input())
        {
            if (!stop)
            {
                stop = true;
                buttonIndex--;
                if(buttonIndex < 0)
                {
                    buttonIndex = buttonList.Length-1;
                        
                }
                //button Hilighted
                buttonList[buttonIndex].GetComponent<Button>().Select();
            }
        }
        
        if (upValue == 0)
        {
            stop = false;
        }

        if (A_input())
        {
            //関数を呼び出す
            buttonList[buttonIndex].Click();
        }
    }
  
}
