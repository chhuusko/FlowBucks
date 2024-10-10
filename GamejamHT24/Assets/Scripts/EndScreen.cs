
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public GameObject EndScreenCanvas;
    public DayManager dayManager;

    void Start()
    {
        if (dayManager == null)
        {
            dayManager = FindObjectOfType<DayManager>();
        }
    }

    public void ReloadSceneLoss()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);

        Time.timeScale = 1; 
        EndScreenCanvas.SetActive(false);
    }

    public void ReloadSceneWin()
    {
        dayManager.IncrementDay();

        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);

        Time.timeScale = 1;
        EndScreenCanvas.SetActive(false);
    }

    public void LoadNextScene()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //int nextSceneIndex = currentSceneIndex + 1;

        //// Check if the next scene index is valid
        //if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        //{
        //    SceneManager.LoadScene(nextSceneIndex);
        //    Time.timeScale = 1; // 
        //    EndScreenCanvas.SetActive(false);
        //}
        //else
        //{
        //    Debug.Log("No more scenes to load!"); 
        //}
    }
}
