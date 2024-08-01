using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioExtensions 
{
    public static AudioSource PlayClip2D(this AudioClip clip,MonoBehaviour mono,float volume = 1,float pitch = 1)
    {
        GameObject go = new GameObject();
        AudioSource audioSource= go.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.Play();
        UnityEngine.Object.Destroy(go, audioSource.clip.length);
        return audioSource;
    }
    public static AudioSource PlayMusic2D(this AudioClip clip, MonoBehaviour mono, float volume = 1, float pitch = 1)
    {
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        return audioSource;
    }
}
