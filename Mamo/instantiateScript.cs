using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateScript : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void generate()
    {
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
