using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemActivetor : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
