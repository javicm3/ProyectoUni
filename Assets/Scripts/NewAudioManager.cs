using UnityEngine.Audio;
using System;
using UnityEngine;

public class NewAudioManager : MonoBehaviour
{
    private static NewAudioManager _instance;

    public static NewAudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<NewAudioManager>();
            }

            return _instance;
        }
    }

    public Sound[] sounds;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
            foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = s.mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    private void Start()
    {       
        Play("Theme");
        GameManager.Instance.CargarVolumenGuardado();
    }

    public void Play (string name)
    {        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " tiene COVID!");
            return;
        }
       if(s.source!=null) s.source.Play();
    }
    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " tiene COVID!");
            return;
        }
        if (s.source != null) s.source.Stop();
    }
    public void Change (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("Master/Music/In Cables");
        GameManager.Instance.CargarVolumenGuardado();
    }
}
