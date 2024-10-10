using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayManager : MonoBehaviour
{
    public Text TextDay; 
    public static int currentDay = 1;
    private int targetScoreIncrement = 1000;
    private int initialTargetScore = 3000;
    AudioSource musicSource;
    AudioSource effectSource;
    AudioClip ambience;
    AudioClip newDay;

    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(ResetTimeScaleAfterDelay(3f));  // Starta en Coroutine istället för Invoke
        UpdateDayText();
        ScoreManager.instance.SetTargetScore(initialTargetScore + (currentDay - 1) * targetScoreIncrement);
        ambience = Resources.Load<AudioClip>("Audio/Effects/CafeteriaAmbience");
        newDay = Resources.Load<AudioClip>("Audio/Effects/NewDay");
        musicSource = GameObject.Find("SoundManager").GetComponents<AudioSource>()[1];
        effectSource = GameObject.Find("SoundManager").GetComponents<AudioSource>()[0];
        musicSource.PlayOneShot(ambience, 0.5f);
        effectSource.PlayOneShot(newDay, 1f);
    }

    private IEnumerator ResetTimeScaleAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);  // Vänta i realtid
        Time.timeScale = 1f;  // Återställ tidsskalan efter fördröjningen
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
