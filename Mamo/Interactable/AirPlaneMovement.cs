using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneMovement : MonoBehaviour
{
    public Transform point1, point2,playerDownposition1, playerDownposition2;
    private Transform destination;
    public GameObject player;
    public float moveSpeed=10f;
    private bool going, isAtThePoint2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (going)
        {
            if (!isAtThePoint2)
            {

                destination = point2;
                transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime * 19f);
                Quaternion newRot = Quaternion.Euler(-90f, -90f, 0f);
                transform.rotation = newRot;
            }
            else
            {
                destination = point1;
                transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime * 19f);
                Quaternion newRot =Quaternion.Euler(-90f, -270f, 0f);
                transform.rotation = newRot;
            }
            if (transform.position == destination.position)
            {
                Plane.instance.engenOn = false;
                if (destination == point1)
                {
                    PlayerController.instance.transform.position = playerDownposition1.position;
                    GameManager.instace.setCheckPoint(playerDownposition1.position);
                    isAtThePoint2 = false;
                }
                else
                {
                    PlayerController.instance.transform.position = playerDownposition2.position;
                    GameManager.instace.setCheckPoint(playerDownposition2.position);
                    isAtThePoint2 = true;
                }
                
                player.SetActive(true);
                
                going = false;
            }
      
        }
            
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1")&&!going)
            {
                Plane.instance.engenOn = true;
                player.SetActive(false);
                going = true;
               
            }
        }
    }
}
