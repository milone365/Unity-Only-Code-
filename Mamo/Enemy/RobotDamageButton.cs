using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDamageButton : MonoBehaviour
{
    public RobotController rbCon;

    private void Start()
    {
        rbCon = GameObject.FindObjectOfType<RobotController>();
    }
    private void Update()
    {
        
    }

    public void getDamage()
    {
        rbCon.goToNextPhase();
    }
   



}
