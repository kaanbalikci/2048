using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager UI;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private void Awake()
    {
        UI = this;
    }

    private void OnEnable()
    {
        GameManager.UpdateHighScore += HighScoreManager;
        GameManager.UpdateScore += ScoreManager;
    }
    private void OnDisable()
    {
        GameManager.UpdateHighScore -= HighScoreManager;
        GameManager.UpdateScore -= ScoreManager;
    }

    public void ScoreManager(int Score)
    {
        scoreText.text = Score.ToString();
    }

    public void HighScoreManager(int HighScore)
    {
        highScoreText.text = HighScore.ToString();
    }
}
