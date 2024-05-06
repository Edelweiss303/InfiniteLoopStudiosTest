using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEditor;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    private Hashtable soundTable;

    private void Awake()
    {
        soundTable = new Hashtable();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            soundTable.Add(s.name, s);
        }

        Array.Clear(sounds, 0, soundTable.Count);
    }

    public void PlaySound(string name)
    {
        if(!soundTable.Contains(name))
        {
            Debug.LogError("Sound: " + name + " not found");
            return;
        }

        ((Sound)soundTable[name]).source.Play();
    }

    public void StopSound(string name)
    {
        if(!soundTable.Contains(name))
        {
            Debug.LogError("Sound: " + name + " not found");
            return;
        }

        ((Sound)soundTable[name]).source.Play();
    }

}
