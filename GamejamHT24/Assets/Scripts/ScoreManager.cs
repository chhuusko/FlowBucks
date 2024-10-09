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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
        UpdateMultiplierText(1);
    }

    public void AddScore(int multiplier)
    {
        score += 1 * multiplier; //Change to correct value for order completion
        scoreText.text = "SCORE: " + score.ToString();
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void UpdateMultiplierText(int multiplier)
    {
        multiplierText.text = multiplier + "x"; 
    }
}
