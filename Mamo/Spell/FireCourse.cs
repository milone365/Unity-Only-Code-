using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCourse : MonoBehaviour
{
    int damage;
    public GameObject fireprefab;
    private void Start()
    {
        damage = GameManager.instace.intellect*2;
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().giveCurse(damage);
            GameObject newFire = Instantiate(fireprefab, other.transform.position, Quaternion.identity);
            newFire.gameObject.transform.SetParent(other.transform);
        }
    }
}
