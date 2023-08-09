using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    //ensures one instance of audio manager in the scene

    void Start()
    {
        Play("theme");
    }
    void Awake()
    {
        //Make sure theres only 1 audio manager between scenes
        if (instance == null) {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        //persist on load, this can be used between all scenes

        foreach (Sound s in sounds)
        {//for each sound add an audio source is created 
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {return;} //if the name that is specified does not exist then just ignore to prevent null error
        else{s.source.Play();}
        
    }
}
