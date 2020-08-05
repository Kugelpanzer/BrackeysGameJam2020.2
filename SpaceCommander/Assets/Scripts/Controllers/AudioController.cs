using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

using Random = UnityEngine.Random;
public class AudioController : MonoBehaviour
{
    public Sound[] sounds;
    private int rand;
    public static AudioController instance;

	

    public void SetAllVolume()
    {
        foreach (Sound s in sounds)
        {

            if (s.type == Music.sound)
            {
                s.source.volume = PlayerPrefs.GetFloat("SoundVolume", 1f) * s.volume;
            }
            else if(s.type==Music.effects)
            {
                s.source.volume = PlayerPrefs.GetFloat("EffectVolume", 1f) * s.volume;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = PlayerPrefs.GetFloat("SoundVolume", 1f) * s.volume;
            s.source.pitch = 1f;
            s.source.loop = s.loop;
        }

    }
    void Start()
    {

        SetAllVolume();
    }


    public void PlaySound(string name)
    {
        Sound currSound=Array.Find(sounds, sound => sound.name == name);
        currSound.source.Play();
    }

    void Update()
    {
    }
}
