using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBullet : MonoBehaviour
{
    public int damage = 5;
    [SerializeField]
    protected float speedMultipler = 1f;
    [SerializeField]
    protected float bulletSpeed = 0.4f, rotateSpeed = 1;


    Vector3 _target;
    Vector3 direction;
    private void Start()
    {
        Destroy(gameObject, 3);
        
    }
    public void givetarget(Vector3 tg)
    {
        _target = tg;
        direction = _target- transform.position;
        
    }

    private void Update()
    {
        transform.position += direction * Time.deltaTime*bulletSpeed*speedMultipler;
    }
}

    
