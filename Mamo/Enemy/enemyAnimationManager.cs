using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimationManager : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void animationIdle(bool value)
    {
        
        anim.SetBool("idle", true);
    }
    public void animationMove(bool value) {
        
        anim.SetBool("move", value); }

    public void animationAttak()
    {
        
        TriggerAnim("atk");
    }
    public void animationAttak2()
    {
       
        TriggerAnim("atk2");
    }
    public void animationHurt(int value)
    {
        
        anim.SetInteger("hurt", value);
    }
   
   
    public void animationCure()
    {
        
        TriggerAnim("cure");
    }
    public void stopBoolAnimations(string name)
    {

        anim.SetBool(name, false);
        
    }
    public void stopIntegerAnimations(string _name)
    {
        anim.SetInteger(_name, 0);
    }
    public void randomAttack()
    {
        
        int i = Random.Range(1, 10);
        if (i > 5)
        {
            TriggerAnim("atk2");
        }
        else
        {
            TriggerAnim("atk");
        }
        
        
    }
    public void animationDie()
    {
        TriggerAnim("die");
    }

    /////
    ///BossAnimations
    ///
    public void animationPhase(int _value)
    {
        anim.SetInteger("phase", _value);
    }
   
    public void TriggerAnim(string name)
    {

        anim.SetTrigger(name);
    }

}
