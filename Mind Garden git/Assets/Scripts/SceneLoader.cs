using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        Debug.Log("SceneLoader Start");

    // Check if TimerManager is null
        if (TimerManager.Instance == null)
        {
            Debug.Log("TimerManager is null");
        }else{
           
            var countdownTimer = TimerManager.Instance.GetComponent<CountdownTimer>();
            if (countdownTimer != null)
            {
                countdownTimer.PauseTimer();
                Debug.Log("CountdownTimer Paused");
            }
            else
            {
                Debug.Log("CountdownTimer is null");
            }
        }
        // Example of pausing the timer when switching scenes
        TimerManager.Instance.GetComponent<CountdownTimer>().PauseTimer();
    }
    
    public void LoadScene(string sceneName)
    {
        PlayerPrefs.SetFloat("TimeRemaining", TimerManager.Instance.GetComponent<CountdownTimer>().timeRemaining);
        SceneManager.LoadScene(sceneName);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Example of resuming the timer when the new scene is loaded
        TimerManager.Instance.GetComponent<CountdownTimer>().ResumeTimer();
    }
}