using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public Dictionary<string, ParticleSystem> effects = new Dictionary<string, ParticleSystem>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Transform[] particles = GetComponentsInChildren<Transform>();
        foreach(var p in particles)
        {
            ParticleSystem particle = p.GetComponent<ParticleSystem>();
            if (particle!=null)
            {
                effects.Add(p.gameObject.name,particle);
            }
            
        }
     
    }
    public void playEffect(Vector3 position,Quaternion rotation, string _name)
    {
              if(effects.ContainsKey(_name))
            {
                effects[_name].transform.position = position;
                effects[_name].transform.rotation = rotation;
                effects[_name].Play();
           }
        }
}
       
 
    

