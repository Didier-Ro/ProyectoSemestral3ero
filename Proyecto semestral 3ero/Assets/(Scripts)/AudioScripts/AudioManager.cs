using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource _audioSource;

    [SerializeField] AudioClip[] _audioClips = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }
    
    public void AudioSelection(int index, float volume)
    {
        _audioSource.PlayOneShot(_audioClips[index], volume);
    }
}
