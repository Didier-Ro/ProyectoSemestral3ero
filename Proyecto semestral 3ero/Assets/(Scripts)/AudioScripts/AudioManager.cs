using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _sfxSource;
    private float _defaultMusicVolume = 0.5f;
    private float _defaultSfxVolume = 1f;
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

    public void SetAudioConfigurations()
    {
        _musicSource.volume = PlayerPrefs.GetFloat("Music", _defaultMusicVolume);
        _sfxSource.volume = PlayerPrefs.GetFloat("SFX", _defaultSfxVolume);
    }
}
