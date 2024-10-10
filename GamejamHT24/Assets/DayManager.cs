using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public Text TextDay; 
    public static int currentDay = 1;

    void Start()
    {
        UpdateDayText();
    }

    public void IncrementDay()
    {
        currentDay++;
        UpdateDayText();
    }

    private void UpdateDayText()
    {
        TextDay.text = "Day: " + currentDay.ToString();
    }

    void OnApplicationQuit()
    {
        ScoreManager.instance.ResetTargetScore();
    }
}
