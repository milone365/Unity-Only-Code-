using rpg.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]protected string impactEffect;
    protected Health target = null;
    [SerializeField] protected float speed = 1;
    [SerializeField] protected float damage = 5;
    protected GameObject instigator=null;
    private void Start()
    {
        Destroy(gameObject, 10);
        if (target == null) return;
        transform.LookAt(getAimLocation());
        
    }
    void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void setTarget(Health tag,float _damage,GameObject instig)
    {
        target = tag;
        damage = _damage;
        instigator = instig;
    }
    public Vector3 getAimLocation()
    {
       CapsuleCollider targetColl = target.GetComponent<CapsuleCollider>();
        if (targetColl == null) return target.transform.position;

        return target.transform.position+Vector3.up*targetColl.height;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth == null) return;
            if (enemyHealth.isdead()) return;
           enemyHealth.takeDamage(instigator,damage);
            if (impactEffect != null)
            {
                EffectManager.instance.playEffect(transform.position, transform.rotation, impactEffect);
            }
            Destroy(gameObject);
        }
    }
}
