using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            this.gameObject.SetActive(false);
        }
        
    }
    private void OnEnable()
    {
        lifetime = 10f;
        HealthManager.instace.ActiveBUBBLE();
    }
    private void OnDisable()
    {
            HealthManager.instace.deactiveBubble();
    }
}
