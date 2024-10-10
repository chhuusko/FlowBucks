using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class InGameClock : MonoBehaviour
{
    public Text clockText;
    public float timePassed = 0f;
    private int hour = 8;
    private int minute = 0;
    private float timeInterval = 5f;

    public GameObject EndScreenWin;  
    public GameObject EndScreenLoss;
    private AudioSource effectSource;
    private AudioClip angryBoss;
    private AudioClip lose;
    private AudioClip win;

    void Start()
    {
        angryBoss = Resources.Load<AudioClip>("Audio/Effects/Angry boss");
        lose = Resources.Load<AudioClip>("Audio/Effects/LoseFailSound2");
        win = Resources.Load<AudioClip>("Audio/Effects/WinScreen");
        effectSource = GameObject.Find("SoundManager").GetComponents<AudioSource>()[0];
        StartCoroutine(UpdateClock());
        EndScreenWin.SetActive(false);
        EndScreenLoss.SetActive(false);
    }

    void Update()
    {
        timePassed += Time.deltaTime;
    }

    IEnumerator UpdateClock()
    {
        while (true)
        {
            IncrementTime(); 
            UpdateTimeDisplay();

            if (hour == 20 && minute == 0)
            {
                EndGame();
                yield break; 
            }

            yield return new WaitForSeconds(timeInterval);
        }
    }

    void IncrementTime()
    {
        minute += 15;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }
    }

    void UpdateTimeDisplay()
    {
        string formattedTime = string.Format("{0:D2}:{1:D2}", hour, minute);
        clockText.text = formattedTime;
    }

    void EndGame()
    {
        if (ScoreManager.instance.HasReachedTargetScore())
        {
            EndScreenWin.SetActive(true);
            effectSource.PlayOneShot(win, 1f);
        }
        else
        {
            EndScreenLoss.SetActive(true);
            effectSource.PlayOneShot(angryBoss, 1f);
            effectSource.PlayOneShot(lose, 1f);
        }
        Time.timeScale = 0;
    }

    public void TriggerLossCondition()
    {
        effectSource.PlayOneShot(angryBoss, 1f);
        effectSource.PlayOneShot(lose, 1f);
        EndScreenLoss.SetActive(true);
        Time.timeScale = 0;  
    }
}


