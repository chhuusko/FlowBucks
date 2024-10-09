using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public Text scoreText;
    //public Text highscoreText;
    public Text multiplierText;
    public Text TextTargetScore;

    int score = 0;
    //int highscore = 0;

    private int consecutiveCorrectItems = 0;
    private int multiplier = 1;
    private int ordersCompleted;
    private int targetScore;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //highscore = PlayerPrefs.GetInt("highscore", 0);
        SetTargetScoreForScene();
        scoreText.text = "$ made: " + score.ToString();
        //highscoreText.text = "Target $: " + highscore.ToString();
        UpdateMultiplierText();
        UpdateTargetScoreText();
    }

    private void SetTargetScoreForScene()
    {
        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "Main":
                targetScore = 100;
                break;
            case "Day2":
                targetScore = 200;
                break;
            case "Day3":
                targetScore = 300;
                break;
        }
    }

    public void AddScore(int points)
    {
        score += points * multiplier;
        scoreText.text = "$ made: " + score.ToString();
        //if (highscore < score)
        //{
        //    PlayerPrefs.SetInt("highscore", score);
        //}
    }

    public void UpdateMultiplierText()
    {
        multiplierText.text = "Flow " + multiplier + "x"; 
    }
    
    public void UpdateTargetScoreText()
    {
        TextTargetScore.text = "Target $: " + targetScore.ToString();
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
