using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Popup UI")]
    [SerializeField] private TextMeshProUGUI titleText;        // LeaderBoard → TitleMsg
    [SerializeField] private TextMeshProUGUI finalScoreText;   // LeaderBoard → FinalScore
    [SerializeField] private Button backButton;                // “Flap Again” button
    [SerializeField] private GameObject leaderboardCanvas;     // Leaderboard Panel root

    [Header("Leaderboard Logic")]
    [SerializeField] private Leaderboard leaderboard;          // Your LootLocker script

    private void Start()
    {
        backButton.onClick.AddListener(OnBackToMenu);
        StartCoroutine(SetupRoutine());
    }

    private IEnumerator SetupRoutine()
    {
        // Log in as guest
        bool done = false;
        LootLockerSDKManager.StartGuestSession(resp =>
        {
            if (resp.success)
                PlayerPrefs.SetString("PlayerID", resp.player_id.ToString());
            done = true;
        });
        yield return new WaitWhile(() => !done);

        // Pre‑fetch main‑menu top 10
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    // Called by GameManager as soon as they hit Top 10
    public void ShowLeaderboardPanel()
    {
        int score = GameManager.Score;

        // 1) Show & pause right away
        leaderboardCanvas.SetActive(true);
        Time.timeScale = 0f;

        // 2) Display raw score immediately
        finalScoreText.text = score.ToString();

        // 3) Show a loading title while network calls happen
        titleText.text = "⏳ Checking your rank...";

        // 4) Kick off the background work
        StartCoroutine(SubmitAndThenRank(score));
    }

    private IEnumerator SubmitAndThenRank(int score)
    {
        // A) Set player name to wallet
        string wallet = PlayerPrefs.GetString("WalletAddress", "");
        bool nameDone = false;
        LootLockerSDKManager.SetPlayerName(wallet, resp => nameDone = true);
        yield return new WaitUntil(() => nameDone);

        // B) Submit the score
        yield return leaderboard.SubmitScoreRoutine(score);

        // C) Fetch your rank
        bool rankSuccess = false;
        yield return leaderboard.GetPlayerRankRoutine(s => rankSuccess = s);
        int rank = rankSuccess ? leaderboard.playerRank : -1;

        // D) Pick a friendly title
        string title;
        switch (rank)
        {
            case 1: title = "NEW HIGH SCORE!"; break;
            case 2: title = "SO CLOSE! #2"; break;
            case 3: title = "ALMOST MADE IT #3!"; break;
            default:
                title = (rank > 0 && rank <= 10)
                    ? "NICE! You cracked Top 10"
                    : "Better luck next time";
                break;
        }

        // E) Finally update the title
        titleText.text = title;
    }

    private void OnBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
