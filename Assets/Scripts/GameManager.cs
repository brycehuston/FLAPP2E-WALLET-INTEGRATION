using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject backButton;
    public GameObject gameOver;
    public GameObject leaderboardCanvas;
    public Camera mainCamera;
    public HighScoreDisplay highScoreDisplay;
    public ParticleSystem highScoreEffect;
    public TextPulse highScoreTextPulse;
    public Text livesText;

    public static int Score { get; private set; }

    private int highScore;
    private int totalLives = 3;
    private int currentLives;
    private bool hasPlayedHighScoreSound = false;
    private bool hasNewHighScore = false;
    private Vector3 originalCamPos;
    public static float currentPipeSpeed = 5f;
    public float initialPipeSpeed = 5f;
    public float maxPipeSpeed = 30f;
    public float shakeAmount = 0.2f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        highScoreDisplay = FindObjectOfType<HighScoreDisplay>();
        originalCamPos = Camera.main.transform.position;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreDisplay != null)
        {
            highScoreDisplay.UpdateHighScore(highScore);
        }

        Pause();
        ResetLives();
    }

    public void Play()
    {
        Time.timeScale = 1f;
        if (player != null) player.enabled = true;

        Score = 0;
        UpdateScoreText();
        playButton.SetActive(false);
        gameOver.SetActive(false);
        backButton.SetActive(false);
        leaderboardCanvas.SetActive(false);
        ResetPipes();
    }

    void Update() => ShakeCamera();

    public void IncreaseScore(int amount)
    {
        Score += amount;
        UpdateScoreText();
        CheckForHighScore();
        AdjustDifficulty();
    }

    private void CheckForHighScore()
    {
        if (Score > highScore)
        {
            Debug.Log("🔥 NEW HIGH SCORE! Logging and storing...");

            highScore = Score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            if (highScoreDisplay != null)
                highScoreDisplay.UpdateHighScore(highScore);

            if (!hasPlayedHighScoreSound)
            {
                highScoreEffect?.Play();
                highScoreTextPulse?.StartPulsing();
                hasPlayedHighScoreSound = true;
            }

            hasNewHighScore = true;
        }
    }

    private void ShakeCamera()
    {
        mainCamera.transform.localPosition = Score >= highScore
            ? originalCamPos + UnityEngine.Random.insideUnitSphere * shakeAmount
            : originalCamPos;
    }

    public void AdjustDifficulty()
    {
        if (Score % 20 == 0)
        {
            currentPipeSpeed += 1f;
            if (currentPipeSpeed > maxPipeSpeed)
                currentPipeSpeed = maxPipeSpeed;
        }
    }

    public void GameOver()
    {
        currentLives--;
        UpdateLivesDisplay();

        if (hasNewHighScore && leaderboardCanvas != null)
        {
            leaderboardCanvas.SetActive(true);
            hasNewHighScore = false;
        }

        if (currentLives <= 0)
        {
            SceneManager.LoadScene(0);
            return;
        }

        gameOver.SetActive(true);
        playButton.SetActive(true);
        backButton.SetActive(true);

        hasPlayedHighScoreSound = false;
        currentPipeSpeed = initialPipeSpeed;
        Pause();
    }

    private void ResetLives()
    {
        currentLives = totalLives;
        UpdateLivesDisplay();
    }

    private void UpdateLivesDisplay()
    {
        livesText.text = new string('I', currentLives).Replace("I", "I ");
    }

    private void UpdateScoreText() => scoreText.text = Score.ToString();

    public void Pause()
    {
        Time.timeScale = 0f;
        if (player != null) player.enabled = false;
    }

    private void ResetPipes()
    {
        foreach (var pipe in FindObjectsOfType<Pipes>())
        {
            Destroy(pipe.gameObject);
        }
    }
}
