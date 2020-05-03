using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trasparence : MonoBehaviour
{
   SkinnedMeshRenderer rend = null;
    [SerializeField]
    float blinkingTime= 3;
    float blinckingCounter = 3;
    bool isTrasparent = false;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        blinckingCounter = blinkingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrasparent)
        {
            blinckingCounter -= Time.deltaTime;
            if (Mathf.FloorToInt(blinckingCounter * 10) % 2 == 0)
            {
                rend.enabled = true;
            }
            else
            {
                rend.enabled = false;
            }
            if (blinckingCounter <= 0)
            {
                isTrasparent = false;
                blinckingCounter = blinkingTime;
                rend.enabled = true;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            becameTransparent();
        }
    }
    public void becameTransparent()
    {
        isTrasparent = true;
    }
}
