using UnityEngine;
using UnityEngine.UI;

public class MenuConfigurations : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider = default;
    [SerializeField] private Slider _sfxSlider = default;
    [SerializeField] private Slider _turnSlider = default;
    [SerializeField] private string _saveMusicConfig = default;
    [SerializeField] private string _saveSfxConfig = default;
    [SerializeField] private string _saveTurnConfig = default;

    void Start()
    {
        _musicSlider.onValueChanged.AddListener(x => SaveValue(_saveMusicConfig, x));
        _sfxSlider.onValueChanged.AddListener(x => SaveValue(_saveSfxConfig, x));
        _turnSlider.onValueChanged.AddListener(x => SaveValue(_saveTurnConfig, x));

        _musicSlider.value = PlayerPrefs.GetFloat("Music", _musicSlider.maxValue);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFX", _sfxSlider.maxValue);
        _turnSlider.value = PlayerPrefs.GetFloat("Turn", _turnSlider.maxValue);
    }

    private void SaveValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        GameManager.Instance.SetConfigurations();
    }
}
