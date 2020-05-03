using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sfx;
    public AudioSource[] music;
    public int currentSong;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentSong = 0;
        playCurrentsong();
    }
    public void playSFX(int num)
    {
        sfx[num].Play();
    }
    public void playMusic(int num)
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }
        music[num].Play();
    }
    public void giveCurrentSong(int _currentSong)
    {
        currentSong = _currentSong;

    }
    public void playCurrentsong()
    {
        playMusic(currentSong);
    }
}