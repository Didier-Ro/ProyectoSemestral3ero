using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetHighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScoreText = default;
    private string _highScore = "High Score: ";
    private int _score = default;

    void Start()
    {
        _score = PlayerPrefs.GetInt("HighScore");

        _highScoreText.text = _highScore + _score.ToString();
    }
}
