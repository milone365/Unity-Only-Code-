using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   protected Transform target=null;
    [SerializeField]
    float bulletSpeed = 1;
    [SerializeField]
   protected float damage = 0;
    [SerializeField]
   protected string particleToPlay = "Explosion";

    private void Start()
    {
        Destroy(gameObject, 3);
    }
    public void getTarget(Transform t)
    {
        target = t;
       
    }
  //移動
    private void Update()
    {
        if (target == null)
        {  return; }
        Vector3 pos = target.GetComponent<Collider>().transform.position;
        transform.position = Vector3.MoveTowards(transform.position, pos,bulletSpeed*Time.deltaTime);
        
    }
    //ダメージ
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Health h = other.GetComponent<Health>();
            if (h == null) return;
            h.takeDamage(damage);
            EffectManager.instance.playInPlace(transform.position, particleToPlay);
            Destroy(gameObject);
        }
    }
}
