using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;

    [Range(0, 1)]
    public float Volume;

    [Range(.1f, 3)]
    public float Pitch;

    [HideInInspector]
    public AudioSource Source;
}
