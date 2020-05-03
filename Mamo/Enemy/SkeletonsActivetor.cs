using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsActivetor : MonoBehaviour
{
    public GameObject[] skeletons;
    public GameObject explosionEffect;
    public GameObject treasuare;
    private bool Active;
    [SerializeField]
    int enemynumber;
    private void Start()
    {
        Active = false;
        enemynumber = skeletons.Length;
    }
    private void Update()
    {
        if (Active)
        {
            if (enemynumber <= 0)
            {
                treasuare.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
    public void killed()
    {
        enemynumber--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!Active)
            {
                Active = true;
                for (int i = 0; i < skeletons.Length; i++)
                {

                    skeletons[i].SetActive(true);
                    Instantiate(explosionEffect, transform.position, transform.rotation);
                    treasuare.SetActive(false);
                }
            }
            

        }
    }
}
