using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public duckSpawnSciript_new spawner; // Assign this in the inspector
    public Text timerText; // UI Text to display the timer
    public GameObject questionPanel; // Panel containing the question and input field
    public Text questionText; // Text for the question
    public InputField answerInput; // Input field for the answer
    public Button submitButton; // Button to submit the answer
    public GameObject feedbackPanel; // Panel containing the feedback
    public Text feedbackText; // Text to display feedback
    public GameObject levelCompletePanel; // Panel to show on level completion
    public GameObject levelFailPanel; // Panel to show on level failure
    public GameObject gameOverPanel; // Panel to show when out of lives
    public GameObject dialoguePanel;
    public Text levelText; // Text element to display current level

    public GameObject dialogueBackground;
    public GameObject gameBackground;

    public int currentLevel = 1;
    public int playerLives = 3;
    private float roundTime;
    private bool roundActive;
    private string[] questions = new string[]
    {
        "How many ducks moved from left to right?",
        "How many ducks moved from right to left?"
    };

    private float[] timeLimits = new float[] { 20f, 15f, 10f }; // Time limits for each level
    private float[] duckSpeedRanges = new float[] { 3f, 4f, 6f }; // Max speeds for each level


    void AskRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Length);
        questionText.text = questions[randomIndex];
    }

    void ShowDialogueBackground()
    {
        dialogueBackground.SetActive(true);
        gameBackground.SetActive(false);
    }

    void ShowGameBackground()
    {
        dialogueBackground.SetActive(false);
        gameBackground.SetActive(true);
    }


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        submitButton.onClick.AddListener(CheckAnswer);
        ShowDialogueBackground();
        HideAllPanels();
        dialoguePanel.SetActive(true);
        spawner.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartRound());
        //StartLevel(currentLevel);
    }

    void HideAllPanels()
    {
        questionPanel.SetActive(false);
        feedbackPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        levelFailPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    public void StartGameplay()
    {
        // Logic to start or resume the game
        // This could mean enabling player controls, starting timers, etc.
        HideAllPanels(); // Hide UI panels like dialogue box, etc.
        spawner.enabled = true;
        StartLevel(currentLevel);
        ShowGameBackground();
    }

    void StartLevel(int level)
    {
        currentLevel = level;
        spawner.maxSpeed = duckSpeedRanges[level - 1];
        roundTime = timeLimits[level - 1];
        playerLives = 3; // Reset lives at the start of each level
        levelText.text = "Level " + currentLevel;
        StartCoroutine(StartRound());
    }

    public void NextLevel()
    {
        if (currentLevel < timeLimits.Length)
        {
            StartLevel(currentLevel + 1);
        }
        else
        {
            // Implement what happens when all levels are completed (e.g., show final win screen)
        }
    }

    public void RetryLevel()
    {
        StartLevel(currentLevel);
    }

    IEnumerator StartRound()
    {
        roundActive = true;
        float timer = roundTime;

        while (timer > 0 && roundActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
            yield return null;
        }

        if (roundActive)
        {
            EndRound();
        }
    }

    void EndRound()
    {
        roundActive = false;
        spawner.enabled = false; // Stop spawning ducks
        questionPanel.SetActive(true);
        AskRandomQuestion(); // Call this method to display a random question
    }

    void CheckAnswer()
    {
        int playerAnswer;
        feedbackPanel.SetActive(true);
        if (int.TryParse(answerInput.text, out playerAnswer))
        {
            bool isCorrect = false;
            if (questionText.text == questions[0]) // "How many ducks moved from left to right?"
            {
                isCorrect = (playerAnswer == spawner.countLeftToRight);
            }
            else if (questionText.text == questions[1]) // "How many ducks moved from right to left?"
            {
                isCorrect = (playerAnswer == spawner.countRightToLeft);
            }

            if (isCorrect)
            {
                feedbackText.text = "Correct Answer!";
                feedbackText.color = Color.green;
                //levelCompletePanel.SetActive(true); // Show level completion panel
            }
            else
            {
                //feedbackText.text = "Incorrect Answer.";
                //feedbackText.color = Color.red;
                playerLives--; // Decrement lives
                feedbackText.text = "Incorrect Answer. You have " + playerLives + " lives left.";
                feedbackText.color = Color.red;

                if (playerLives <= 0)
                {
                    //gameOverPanel.SetActive(true); // Show game over panel
                }
                else
                {
                    //levelFailPanel.SetActive(true); // Show level fail panel with retry and end game options
                }
            }
        }
        else
        {
            feedbackText.text = "Please enter a valid number.";
            feedbackText.color = Color.yellow;
        }
    }

    public void EndGame()
    {
        // Implement logic to end the game, such as loading the main menu or closing the application
        // For example, to load a main menu scene: SceneManager.LoadScene("MainMenuSceneName");
        // To quit the game (works only in built game, not in the Unity Editor): Application.Quit();
    }


}
