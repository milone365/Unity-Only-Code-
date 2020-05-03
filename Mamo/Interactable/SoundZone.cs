using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundZone : MonoBehaviour
{
    public int musicToPlay;
    private bool isPlayng;
    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayng)
        {
            AudioManager.instance.playMusic(musicToPlay);
            isPlayng = true;
        }
        

     }
    private void OnTriggerExit(Collider other)
    {
        AudioManager.instance.playCurrentsong();
        isPlayng = false;
    }
}
