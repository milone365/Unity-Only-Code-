using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth=10;
    float health = 10;
    private void Start()
    {
        health = maxHealth;
    }
    public void takeDamage(float v)
    {
        health -= v;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
