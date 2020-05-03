using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject _lock;
    public GameObject lid;
    public Transform spawnpoint;
    public GameObject objectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if (Bag.instance.tresuareKey > 0&&Input.GetButtonDown("Fire1"))
            {
                Bag.instance.removeItem("key", 1);
                Destroy(_lock.gameObject);
                lid.transform.rotation = new Quaternion(90f, 0f, 0f,0.5f);
                Instantiate(objectToSpawn,spawnpoint.position, spawnpoint.rotation);

            }
        }
    }
}
