using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="MPack/Audio Clip Set")]
public class AudioClipSet : ScriptableObject
{
    public AudioClip[] Clips;
    public ClipPlayMode Mode;
    [Range(0, 1)]
    public float Volume = 1;
    [System.NonSerialized]
    private int _index = 0;

    public enum ClipPlayMode { Sequence, Random }

    public AudioClip ChooseOneClip()
    {
        if (Clips.Length == 1)
            return Clips[0];
        
        if (Mode == ClipPlayMode.Random)
            return Clips[Random.Range(0, Clips.Length)];

        if (_index >= Clips.Length)
            _index = 0;
        return Clips[_index++];
    }
}

public static class AudioClipSetExtension
{
    public static void PlayOneShot(this AudioSource audioSource, AudioClipSet clipSet, float volume=1)
    {
        if (clipSet && clipSet.Clips.Length > 0)
            audioSource.PlayOneShot(clipSet.ChooseOneClip(), clipSet.Volume * volume);
    }

    public static void Play(this AudioSource audioSource, AudioClipSet clipSet)
    {
        if (!clipSet)
            return;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clipSet.ChooseOneClip();
        audioSource.volume = clipSet.Volume;
        audioSource.Play();
    }

    public static void PlayClipAtPoint(AudioClipSet clipSet, Vector3 position, float volume=1)
    {
        AudioSource.PlayClipAtPoint(clipSet.ChooseOneClip(), position, clipSet.Volume * volume);
    }
}