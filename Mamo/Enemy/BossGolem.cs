using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGolem : EnemyAI
{

    public EnemyHealthManager em;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.playMusic(7);
        em = GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (em.heath <= 0)
        {
            BossDeactivetor deactivator = FindObjectOfType<BossDeactivetor>();
            deactivator.isDead = true;
            AudioManager.instance.playCurrentsong();
        }
    }
    public override void Move()
    {
        StartCoroutine(inflictdamage());
        transform.LookAt(PlayerController.instance.playerModelChild.transform.position, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + rotationgrade, 0f);
    }
  

}
