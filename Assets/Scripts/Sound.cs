using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
//custom class: If you want this to appear in the inspector then use System.serializable
public class Sound
{//class used as data for AudioManager

    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;
    //we dont want the audiosource to appear in the inspector
    [HideInInspector]
    public AudioSource source;


}
