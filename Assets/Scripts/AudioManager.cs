using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private void Awake ()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    private void Start ()
    {
        Play("Amb",true);
        EventManager.BlobDestroyed.AddListener(PlayDestroyBlob);
        EventManager.FailAction.AddListener(PlayFailAction);
    }
    public void Play (string name, bool isloop)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
        s.source.loop = isloop;
    }
    private void PlayFailAction ()
    {
        Play("FailAction",false);
    }
    private void PlayDestroyBlob ()
    {
        Play("DestroyBlob",false);
    }
}

//SOUND:

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}