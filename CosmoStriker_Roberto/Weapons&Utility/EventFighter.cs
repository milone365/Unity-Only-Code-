using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EventFighter : MonoBehaviour
{

    Transform target;
    EventFighter[] otherFighter;
    [SerializeField] EventBullet bullet;
    float firetime=2;
    // Start is called before the first frame update
    void Start()
    {
        otherFighter = FindObjectsOfType<EventFighter>();
        var a = otherFighter.Where(c => c != this).FirstOrDefault();
        target = a.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //transform.LookAt(target, Vector3.up);
            firetime -= Time.deltaTime;
            if(firetime<=0){
                firetime = Random.Range(1, 2);
                fire();
            }
            
        }
    }

    public void fire()
    {
        EventBullet newBullet = Instantiate(bullet, transform.position, transform.rotation) as EventBullet;
        newBullet.GetComponent<EventBullet>().givetarget(target.position);
    }

}
