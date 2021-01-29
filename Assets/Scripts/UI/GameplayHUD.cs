using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHUD : MonoBehaviour
{
    [SerializeField]
    Text _countdownText;

    [SerializeField]
    Text _scoreText;

    [SerializeField]
    GameplayManager _gameplayManager;

    public void Update()
    {
        UpdateCountdown();
    }

    void UpdateCountdown()
    {
        int countdown = _gameplayManager.Countdown;
        _countdownText.enabled = countdown > 0;

        _countdownText.text = countdown.ToString();

        _scoreText.text = $"{_gameplayManager.GetScore(0)} : {_gameplayManager.GetScore(1)}";
    }
}
