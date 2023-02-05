using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource abmience;
    [SerializeField] private AudioSource destroyBlob;
    [SerializeField] private AudioSource failAction;
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

        
    }
    private void Start ()
    {
        abmience.Play();
        EventManager.BlobDestroyed.AddListener(PlayDestroyBlob);
        EventManager.FailAction.AddListener(PlayFailAction);
    }
    private void PlayFailAction ()
    {
        failAction.Play();
    }
    private void PlayDestroyBlob ()
    {
        destroyBlob.Play();
    }
}

