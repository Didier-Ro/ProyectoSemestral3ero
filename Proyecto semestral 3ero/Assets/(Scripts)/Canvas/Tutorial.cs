using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _movementTutorial = default;
    [SerializeField] private GameObject _turboTutorial = default;
    private int _delayTime = 7;

    void Start()
    {
        StartCoroutine(TutorialUI());
    }

    IEnumerator TutorialUI()
    {
        _movementTutorial.SetActive(true);
        yield return new WaitForSeconds(_delayTime);
        _movementTutorial.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _turboTutorial.SetActive(true);
        yield return new WaitForSeconds(_delayTime);
        _turboTutorial.SetActive(false);
    }
}
