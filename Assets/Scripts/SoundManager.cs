
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Create singleton
    public static SoundManager instance;

    public Sound[] sounds;

    public Sound[] whooshSounds;

    public Sound[] gruntSounds;

    public Sound[] hurtSounds;

    public Sound[] eatingSounds;

    public Sound[] ratSounds;

    public Sound theme1;

    public Sound theme2;

    public Sound death1;

    // Keep track of all playing rat sounds with a list
    public List<AudioSource> ratSoundsPlaying;

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
            sound.source.loop = sound.loop;
        }

        foreach (Sound sound in whooshSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (Sound sound in gruntSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (Sound sound in hurtSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (Sound sound in eatingSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        foreach (Sound sound in ratSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

        }

        theme1.source = gameObject.AddComponent<AudioSource>();
        theme1.source.clip = theme1.clip;
        theme1.source.volume = theme1.volume;
        theme1.source.pitch = theme1.pitch;
        theme1.source.loop = theme1.loop;

        theme2.source = gameObject.AddComponent<AudioSource>();
        theme2.source.clip = theme2.clip;
        theme2.source.volume = theme2.volume;
        theme2.source.pitch = theme2.pitch;
        theme2.source.loop = theme2.loop;

        death1.source = gameObject.AddComponent<AudioSource>();
        death1.source.clip = death1.clip;
        death1.source.volume = death1.volume;
        death1.source.pitch = death1.pitch;
        death1.source.loop = death1.loop;


        ratSoundsPlaying = new List<AudioSource>();
    }

    void Update()
    {
        // Get the number of active rats by getting the count of the enemies list in the game manager
        int numActiveRats = GameManager.instance.enemies.Count;

        // If numActiveRats is less than the number of active rat sounds, stop the extra sounds
        if (numActiveRats < ratSoundsPlaying.Count)
        {
            for (int i = numActiveRats; i < ratSoundsPlaying.Count; i++)
            {
                //Turn off looping
                ratSoundsPlaying[i].loop = false;
                ratSoundsPlaying[i].Stop();
            }
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

    public void PlayWhoosh()
    {
        // Randomly select a sound from the whoosh array
        Sound s = whooshSounds[UnityEngine.Random.Range(0, whooshSounds.Length)];
        s.source.Play();
    }

    public void PlayGrunt()
    {
        // Randomly select a sound from the grunt array
        Sound s = gruntSounds[UnityEngine.Random.Range(0, gruntSounds.Length)];
        s.source.Play();
    }

    public void PlayHurt()
    {
        // Randomly select a sound from the hurt array
        Sound s = hurtSounds[UnityEngine.Random.Range(0, hurtSounds.Length)];
        s.source.Play();
    }

    public void PlayEating()
    {
        // Randomly select a sound from the eating array
        Sound s = eatingSounds[UnityEngine.Random.Range(0, eatingSounds.Length)];
        s.source.Play();
    }

    public void PlayRat()
    {
        // Randomly select a sound from the rat array
        Sound s = ratSounds[UnityEngine.Random.Range(0, ratSounds.Length)];
        s.source.Play();
        // Add the sound to the list of active rat sounds
        ratSoundsPlaying.Add(s.source);

    }

    public void PlayTheme1()
    {
        theme1.source.Play();
    }

    public void StopTheme1()
    {
        theme1.source.Stop();
    }

    public void PlayTheme2()
    {
        theme2.source.Play();
    }

    public void StopTheme2()
    {
        // Fade the volume down over 1 second
        StartCoroutine(FadeOut(theme2.source, 1f));

        theme2.source.Stop();
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void PlayDeath1()
    {
        death1.source.Play();
    }

}
