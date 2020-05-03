using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Endurance : MonoBehaviour
{
    public bool haveLifeBar;
    public int life = 1;
    public Image bar;
    public GameObject hitparticle,destroyparticle2,drop;
    public bool haveToDrop;
    private Transform armSpawner;
    // Start is called before the first frame update
    void Start()
    {
        if (haveLifeBar)
        {
            bar.fillAmount = life;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public  void takedamage(int damage)
    {
        life -= damage;
        if (haveLifeBar)
        {
            bar.fillAmount = life / 100f;
        }
        
        Instantiate(hitparticle, transform.position, transform.rotation);
        if (life <= 0)
        {
            
            Instantiate(destroyparticle2, transform.position, transform.rotation);
            if (haveToDrop)
            {
                armSpawner = GameObject.Find("armSpawner").GetComponent<Transform>();
                Instantiate(drop, armSpawner.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
