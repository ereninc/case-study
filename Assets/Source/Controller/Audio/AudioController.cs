using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    public AudioSource AudioSource;
    public bool HasSound;

    [Button]
    public void PlaySound(Sound sound)
    {
        if (!HasSound) return;
        AudioSource.PlayOneShot(GetClip(sound));
    }

    public enum Sound
    {
        Select,
        Sewing,
        Completed,
        MovePaintArea,
        ButtonClick,
        Painting,
        CollectionUpdate
    }

    private AudioClip GetClip(Sound sound)
    {
        for (int i = 0; i < SoundManager.Instance.Sounds.Length; i++)
        {
            if (SoundManager.Instance.Sounds[i].sound == sound)
            {
                return SoundManager.Instance.Sounds[i].AudioClip;
            }
        }

        return null;
    }
}