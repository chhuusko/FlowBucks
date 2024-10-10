using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public Text TextDay; 
    public static int currentDay = 1;
    private int targetScoreIncrement = 1000;
    private int initialTargetScore = 3000;

    void Start()
    {
        UpdateDayText();
        ScoreManager.instance.SetTargetScore(initialTargetScore + (currentDay - 1) * targetScoreIncrement); 
    }

    public void IncrementDay()
    {
        currentDay++;
        UpdateDayText();
        ScoreManager.instance.SetTargetScore(initialTargetScore + (currentDay - 1) * targetScoreIncrement); 
    }

    private void UpdateDayText()
    {
        TextDay.text = "Day: " + currentDay.ToString();
    }
}
