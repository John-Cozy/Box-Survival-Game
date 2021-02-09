using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    private static AudioManager singleton;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;

        foreach (Sound s in Sounds) {
            s.Source        = gameObject.AddComponent<AudioSource>();
            s.Source.clip   = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch  = s.Pitch;
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
