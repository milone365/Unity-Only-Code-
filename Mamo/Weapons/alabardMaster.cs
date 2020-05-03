using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alabardMaster : MonoBehaviour
{
    int value = 0;
    public Animator anim;
    public HurtCollider hurtcoll;
    public ParticleSystem[] particles;
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        hurtcoll = GetComponent<HurtCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            hurtcoll.damage = value;
            anim.SetInteger("atk", value);
            value++;
            if (value > 6)
            {
                value = 0;
            }
        }
   }


   public void activeAlabardParticle(int spellSelected)
    {
        switch (spellSelected)
        {
            case 0: offAllParticle(); break;
            case 1: playParticle(spellSelected); break;
            case 2: playParticle(spellSelected); break;
            case 3: playParticle(spellSelected); break;
            case 4: playParticle(spellSelected); break;
            case 5: playParticle(spellSelected); break;
            default: offAllParticle(); break;
        }
    }
    void playParticle(int spellSelected)
    {
        offAllParticle();
        particles[spellSelected].Play();
    }
    void offAllParticle()
    {
        for(int i= 0;i < particles.Length; i++)
        {
            particles[i].Stop();
        }
    }
}
