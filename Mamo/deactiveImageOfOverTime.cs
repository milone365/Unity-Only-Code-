using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deactiveImageOfOverTime : MonoBehaviour
{
    public Image thisImage;
    public Text thisImageText;
    public float timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thisImage.enabled == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 2f;
                thisImage.enabled = false;
                thisImageText.text = "";
            }
        }
    }
}
