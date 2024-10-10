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

    void Start()
    {
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
        }
        else
        {
            EndScreenLoss.SetActive(true); 
        }
        Time.timeScale = 0;
    }

    public void TriggerLossCondition()
    {
        EndScreenLoss.SetActive(true);
        Time.timeScale = 0;  
    }
}


