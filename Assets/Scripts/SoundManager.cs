using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Create singleton
    public static SoundManager instance;

    public Sound[] sounds;

    void Awake()
    {
        // Create singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Create AudioSources for each sound
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            //sound.source.loop = sound.loop;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();

    }

}
