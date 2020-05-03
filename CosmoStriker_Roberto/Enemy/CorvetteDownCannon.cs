using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvetteDownCannon : MonoBehaviour
{
    [SerializeField]
    Transform Torret;
    public Transform cannon;
    protected float RotationSpeed = 10;
    protected Transform player;
    public Corvette corvette;
    float fireRate = 0.5f;
    float fireDelay = 0.5f;
    [SerializeField] Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.instance!=null)
        player = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Torret.rotation.y < 50)
            {
                Torret.LookAt(player, Vector3.up);
                Torret.rotation = Quaternion.Euler(0, Torret.eulerAngles.y, 0);
            }
            
            findPlayer();

        }
    }
    public virtual void findPlayer()
    {

        if (player.position.y < corvette.transform.position.y)
        {
            Quaternion targetRot = Quaternion.LookRotation(player.position - cannon.position);
            cannon.rotation = Quaternion.Lerp(cannon.rotation, targetRot, RotationSpeed * Time.deltaTime);
            fire();
        }
               
     }


    public void fire()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            fireRate = fireDelay;
            Instantiate(corvette.bullet, spawnpoint.position, spawnpoint.rotation);
        }
    }
}
