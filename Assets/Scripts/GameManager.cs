using System.Collections;
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
    public Camera mainCamera;
    public HighScoreDisplay highScoreDisplay;
    public Leaderboard leaderboard;
    public ParticleSystem highScoreEffect;
    public TextPulse highScoreTextPulse;
    public Text livesText;
    public AudioSource gameOverSound;

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
        PlayerPrefs.SetInt("LastSubmittedScore", 0); // Reset for testing
        PlayerPrefs.Save();

        leaderboard = leaderboard != null ? leaderboard : FindObjectOfType<Leaderboard>();
        player = player != null ? player : FindObjectOfType<Player>();
        highScoreDisplay = highScoreDisplay != null ? highScoreDisplay : FindObjectOfType<HighScoreDisplay>();

        if (highScoreEffect != null)
            highScoreEffect.Stop();

        if (Camera.main != null)
            originalCamPos = Camera.main.transform.position;

        if (leaderboard != null)
            StartCoroutine(InitializeLeaderboardTopScore());

        Pause();
        ResetLives();
    }

    private IEnumerator InitializeLeaderboardTopScore()
    {
        yield return StartCoroutine(leaderboard.FetchTopHighscoresRoutine());

        int topScore = 0;

        if (leaderboard != null && leaderboard.playerScores != null)
        {
            string[] lines = leaderboard.playerScores.text.Split('\n');
            if (lines.Length > 1 && int.TryParse(lines[1], out int parsedScore))
            {
                topScore = parsedScore;
            }
        }

        highScore = topScore;

        if (highScoreDisplay != null)
            highScoreDisplay.UpdateHighScore(highScore);
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
        ResetPipes();
    }

    void Update()
    {
        ShakeCamera();
    }

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

    private void StopHighScoreEffects()
    {
        highScoreEffect?.Stop();
        highScoreTextPulse?.StopPulsing();
        hasPlayedHighScoreSound = false;
    }

    private void ShakeCamera()
    {
        if (mainCamera == null) return;
        mainCamera.transform.localPosition = hasNewHighScore
            ? originalCamPos + Random.insideUnitSphere * shakeAmount
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
        // 1) Lose a life
        currentLives--;
        UpdateLivesDisplay();

        // 2) Debug log for clarity
        int lastSubmitted = PlayerPrefs.GetInt("LastSubmittedScore", 0);
        Debug.Log($"Score: {Score}");
        Debug.Log($"LastSubmittedScore: {lastSubmitted}");
        Debug.Log($"IsTop10: {leaderboard.IsTop10(Score)}");

        // 3) If this run cracked the Top‑10 (and beats your last submission), submit it
        if (leaderboard != null
            && leaderboard.IsTop10(Score)
            && Score > lastSubmitted
            && Score >= 10)
        {
            // Remember we already submitted this run so we don’t do it again
            PlayerPrefs.SetInt("LastSubmittedScore", Score);
            PlayerPrefs.Save();

            // Hand off to PlayerManager to show the leaderboard popup
            FindObjectOfType<PlayerManager>()
                .ShowLeaderboardPanel();

            // Stop the player from moving
            player.enabled = false;
            return;
        }

        // 4) If you’ve run out of lives, go back to main menu
        if (currentLives <= 0)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        // 5) Otherwise it’s a standard “game over” screen
        gameOverSound?.Play();
        gameOver.SetActive(true);
        playButton.SetActive(true);
        backButton.SetActive(true);

        // Refresh the top‑left high‑score display
        if (highScoreDisplay != null)
            highScoreDisplay.UpdateHighScore(highScore);

        // Reset pipe speed, stop any ongoing effects, and pause
        hasPlayedHighScoreSound = false;
        currentPipeSpeed = initialPipeSpeed;
        Pause();
    }

    private void ResetLives()
    {
        currentLives = totalLives;
        UpdateLivesDisplay();
    }

    public void UpdateLivesDisplay()
    {
        int safeLives = Mathf.Max(0, currentLives);
        livesText.text = new string('♥', safeLives);
    }

    private void UpdateScoreText()
    {
        scoreText.text = Score.ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if (player != null) player.enabled = false;
    }

    private void ResetPipes()
    {
        foreach (var pipe in FindObjectsOfType<Pipes>())
            Destroy(pipe.gameObject);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
