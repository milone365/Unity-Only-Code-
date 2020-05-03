using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationDroneSpawner : MonoBehaviour
{
    [SerializeField] float spawningTime;
    [SerializeField] GameObject dronePrefab;
    [SerializeField] Transform spawnningPoint;
    float spawncounter;
    bool canSpawn;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        spawncounter = spawningTime;
        canSpawn = true;
        player = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            spawncounter -= Time.deltaTime;
            if (spawncounter <= 0)
            {
                spawncounter = spawningTime;
                Instantiate(dronePrefab, spawnningPoint.position, spawnningPoint.rotation);
            }
        }
        if (transform.position.z < player.position.z)
        {
            canSpawn = false;
        }

    }
}
