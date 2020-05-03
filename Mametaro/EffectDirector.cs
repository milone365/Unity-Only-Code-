using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDirector : MonoBehaviour
{
    Dictionary<string, Transform> particleEffects = new Dictionary<string, Transform>();

    void Start()
    {
        Transform[] allEffects = GetComponentsInChildren<Transform>();
        foreach(var item in allEffects)
        {
            if (item.GetComponent<ParticleSystem>())
            {
                particleEffects.Add(item.name, item);
            }
        }
            
    }

    public void playInPlace(Vector3 pos,string effectName)
    {
        if (particleEffects.ContainsKey(effectName))
        {
            particleEffects[effectName].transform.position = pos;
            particleEffects[effectName].GetComponent<ParticleSystem>().Play();
        }
    }
}
