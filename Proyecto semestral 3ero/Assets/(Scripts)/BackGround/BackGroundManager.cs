using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites = default;
    private SpriteRenderer _spriteRenderer = default;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
}
