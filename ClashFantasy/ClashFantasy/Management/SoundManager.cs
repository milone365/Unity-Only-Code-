﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource music, sfx;
    Dictionary<string, AudioClip> allmusic= new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> allsfx=new Dictionary<string, AudioClip>();
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    //サウンドエフェクトと音楽の辞書を作る
    void Start()
    {
        AudioClip[] se = Resources.LoadAll<AudioClip>("AUDIO/SE");
        foreach(var item in se)
        {
            allsfx.Add(item.name, item);
        }
        AudioClip[]musics= Resources.LoadAll<AudioClip>("AUDIO/BGM");
        foreach(var m in musics)
        {
            allmusic.Add(m.name, m);
        }
    }
    //選択したaudioPlay
    public void playSE(string n)
    {
        if (allsfx.ContainsKey(n))
        {
            sfx.clip =allsfx[n] ;
            sfx.Play();
        }
    }
    //選択したaudioPlay
    public void playMusic(string n)
    {

        if (music.isPlaying)
        {
            music.Stop();
        }
        if (allmusic.ContainsKey(n))
        {
            music.clip = allmusic[n];
            music.Play();
        }
    }
}
