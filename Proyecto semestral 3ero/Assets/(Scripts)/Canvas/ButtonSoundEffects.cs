using UnityEngine;

public class ButtonSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField] private AudioClip[] _audioClips = default;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PointetEnter()
    {
        _audioSource.PlayOneShot(_audioClips[0], 1);
    }

    public void PointerClick()
    {
        _audioSource.PlayOneShot(_audioClips[1], 1);
    }
}
