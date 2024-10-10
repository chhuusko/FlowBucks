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
    private int correctItemsNeeded = 5;
    private int multiplier = 1;
    private int maxMultiplier = 8;
    private int ordersCompleted;
    private int targetScore;
    private AudioClip decreaseClip;
    private AudioClip increaseClip;
    private AudioSource source;

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
        decreaseClip = Resources.Load<AudioClip>("Audio/Effects/FlowLoss");
        increaseClip = Resources.Load<AudioClip>("Audio/Effects/Flow increase");
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            correctItemsNeeded = 1;
        }
#endif
    }

    private void SetTargetScoreForScene()
    {
        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "Main":
                targetScore = 100;
                break;
            case "Day 2":
                targetScore = 200;
                break;
            case "Day 3":
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

    public bool HasReachedTargetScore()
    {
        return score >= targetScore;
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

        if (consecutiveCorrectItems >= correctItemsNeeded)
        {
            if (multiplier < maxMultiplier) 
            {
                multiplier *= 2;
                source.pitch = 1 + (0.1f * multiplier);
                source.PlayOneShot(increaseClip, UnityEngine.Random.Range(0.9f, 1.1f));
            }
            consecutiveCorrectItems = 0;
        }
        UpdateMultiplierText();
    }

    public void ResetStreak()
    {
        consecutiveCorrectItems = 0;
        multiplier = 1;
        source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        source.PlayOneShot(decreaseClip, UnityEngine.Random.Range(0.9f, 1.1f));
        UpdateMultiplierText();
    }
}
