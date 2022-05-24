using UnityEngine;
using UnityEngine.UI;

public class MusicConfig : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider = default;
    [SerializeField] private Slider _sfxSlider = default;
    [SerializeField] private float _sliderMusicValue = default;
    [SerializeField] private float _sliderSfxValue = default;
    [SerializeField] private float _defaultMusicVolume = 0.5f;
    [SerializeField] private float _defaultSfxVolume = 1.0f;
    [SerializeField] private AudioSource _musicVolume = default;
    [SerializeField] private AudioSource _sfxVolume = default;

    void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("audioVolume", _defaultMusicVolume);
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", _defaultSfxVolume);
        _musicVolume.volume = _musicSlider.value;
        _sfxVolume.volume = _sfxSlider.value;
    }

    public void ChangeMusicVolume(float value)
    {
        _sliderMusicValue = value;
        PlayerPrefs.SetFloat("audioVolume", _sliderMusicValue);
        _musicVolume.volume = _musicSlider.value;
    }

    public void ChangeSfxVolume(float value)
    {
        _sliderSfxValue = value;
        PlayerPrefs.SetFloat("sfxVolume", _sliderSfxValue);
        _sfxVolume.volume = _sfxSlider.value;
    }
}
