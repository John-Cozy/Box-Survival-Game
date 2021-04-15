using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound {
        public string Name;

        public AudioClip Clip;

        [Range(0, 1)]
        public float Volume;

        [Range(.1f, 3)]
        public float Pitch;

        [HideInInspector]
        public AudioSource Source;
    }

    public Sound[] Sounds;

    private static AudioManager singleton;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }

    public static void SetSounds(float volume) {
        foreach (Sound s in singleton.Sounds) {
            s.Source = singleton.gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume * volume;
            s.Source.pitch = s.Pitch;
        }
    }

    public static void Play(string name) {
        Sound s = Array.Find(singleton.Sounds, sound => sound.Name == name);
        if (s == null) {
            Debug.LogError("Sound \'" + name + "\' not found");
        } else {
            s.Source.Play();
        }
    }
}
