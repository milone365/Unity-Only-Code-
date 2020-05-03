using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletsSpawner : MonoBehaviour
{
    //dekita
    [SerializeField] GameObject bullet, rayGun, missiles,cannonBulletPrefab;
    [SerializeField] Transform bulletSpawnPoint, raygunSpawnPoint;
    [SerializeField] Transform[] missilesSpawnPoints;
    [SerializeField] Transform[] cannonSpawnPoints;

    public void bulletFire()
    {
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
    public void raygunAttack()
    {
        Instantiate(rayGun, raygunSpawnPoint.position, raygunSpawnPoint.rotation);
   }
    public void cannonAttack()
    {
        foreach (Transform point in missilesSpawnPoints)
        {
            Instantiate(cannonBulletPrefab, point.position, point.rotation);

        }
        
    }
    public void MissileAttack()
    {
        foreach (Transform point in missilesSpawnPoints)
        {
            Instantiate(missiles, point.position, point.rotation);

        }
    }
}