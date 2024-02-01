using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountdownTimer : MonoBehaviour
{
    
    public float timeRemaining = 60f;
    [SerializeField] TextMeshProUGUI countdownText;

    private bool isTimerPaused = false;

    void Start()
    {
        // Load the timer state from PlayerPrefs
        if (PlayerPrefs.HasKey("TimeRemaining"))
        {
            timeRemaining = PlayerPrefs.GetFloat("TimeRemaining");
        }

        UpdateUI();
    }


    void Update()
    {
        if (!isTimerPaused)
        {

            if(timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
            }else if(timeRemaining <= 0){
                timeRemaining = 0;
    
            }

            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            UpdateUI();
            
        }
    }

    void UpdateUI()
    {
        countdownText.text = Mathf.FloorToInt(timeRemaining).ToString();
    }

    public void PauseTimer()
    {
        isTimerPaused = true;
    }

    public void ResumeTimer()
    {
        isTimerPaused = false;
    }
}
