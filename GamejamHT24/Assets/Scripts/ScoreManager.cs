using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public Text scoreText;
    public Text highscoreText;
    public Text multiplierText;

    int score = 0;
    int highscore = 0;

    private int consecutiveCorrectItems = 0;
    private int multiplier = 1;
    private int ordersCompleted;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
        UpdateMultiplierText();
    }

    public void AddScore(int points)
    {
        score += points * multiplier;
        scoreText.text = "SCORE: " + score.ToString();
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void UpdateMultiplierText()
    {
        multiplierText.text = multiplier + "x"; 
    }

    public void IncreaseStreak()
    {
        consecutiveCorrectItems++;

        if (consecutiveCorrectItems >= 5)
        {
            multiplier *= 2;
            consecutiveCorrectItems = 0;
        }
        UpdateMultiplierText();
    }

    public void ResetStreak()
    {
        consecutiveCorrectItems = 0;
        multiplier = 1;
        UpdateMultiplierText();
    }
}
