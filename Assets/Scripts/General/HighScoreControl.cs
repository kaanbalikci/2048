using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreControl : MonoBehaviour
{
    [SerializeField] private TMP_Text MhighScoreText;

    private int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score", 0);
        MhighScoreText.text = highScore.ToString();
    }
  
}
