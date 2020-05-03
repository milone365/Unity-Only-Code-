using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{

    [SerializeField]
    Transform Torret;
    public Transform[] DownElement;
    public Transform[] cannon;
    protected float RotationSpeed = 10;
    public bool canMove;
    public bool reassetting;
    protected Transform player;
    
    // Start is called before the first frame update
    void Start()
    {

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
       
    }

    public virtual void Tick()
    {
        if (canMove)
        {
            if (player != null)
            {
                if (Torret.rotation.y < 50)
                {
                    Torret.LookAt(player, Vector3.up);
                    Torret.rotation = Quaternion.Euler(0, Torret.eulerAngles.y, 0);
                }
                //左と右の移動
              
             
                    findPlayer();

            }
        }
        if (reassetting)
        {
            reasset();

        }
    }
    public virtual void findPlayer()
    {
        if (player.position.y >= cannon[0].position.y)
        {
            for (int i = 0; i < cannon.Length; i++)
            {

                Quaternion targetRot = Quaternion.LookRotation(player.position - cannon[i].position);
                cannon[i].rotation = Quaternion.Lerp(cannon[i].rotation, targetRot, RotationSpeed * Time.deltaTime);

            }
        }
   }
    

    public void Init()
    {
        canMove = true;
        if (PlayerController.instance != null)
        {
            player = PlayerController.instance.transform;
        }
    }
    public virtual void reasset()
        {
            for (int i = 0; i < cannon.Length; i++)
            {
               //下へ行かないのにまたリミットをつくりました
                cannon[i].rotation = Quaternion.Lerp(cannon[i].rotation, DownElement[i].rotation, RotationSpeed * Time.deltaTime);

                if (cannon[cannon.Length - 1].rotation == DownElement[cannon.Length - 1].rotation)
                {
                    reassetting = false;
                }
            }
        }

   

}
 

    

