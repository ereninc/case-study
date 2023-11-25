using UnityEngine;

[System.Serializable]
public class SoundManager : Singleton<SoundManager>
{
    public SoundAudioClip[] Sounds;

    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioController.Sound sound;
        public AudioClip AudioClip;
    }
}