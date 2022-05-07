using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioSource _musicSource;

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
    }
    
    public void SFXSelection(AudioClip clip, float volume)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }

    public void MusicSelection(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }
}
