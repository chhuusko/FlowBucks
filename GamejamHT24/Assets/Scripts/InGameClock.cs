using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class InGameClock : MonoBehaviour
{
    public Text clockText; 
    private int hour = 8;
    private int minute = 0;
    private float timeInterval = 1f; 

    void Start()
    {
        StartCoroutine(UpdateClock());
    }

    IEnumerator UpdateClock()
    {
        while (true)
        {
            IncrementTime(); 
            UpdateTimeDisplay();

            if (hour == 22 && minute == 0)
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
        Debug.Log("Game Over! The time is 22:00."); //load a "Game Over" scene
    }
}


