using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [Header("Popup List UI")]
    public TextMeshProUGUI playerNames;       // Drag in LeaderBoard → Names → Text (TMP)
    public TextMeshProUGUI playerScores;      // Drag in LeaderBoard → Scores → Text (TMP)
    public Text topHighScoreText; // Drag in your top‑left high score Text (UI.Text or TMPUGUI)
    public GameObject leaderboardCanvas; // Drag in your Leaderboard Panel

    [HideInInspector] public int playerRank = -1;
    private string leaderboardID = "31420";

    /// <summary>
    /// Public entry point to show the leaderboard in the Main Menu.
    /// Fetches top 10, then enables the panel.
    /// </summary>
    public void ShowLeaderboard()
    {
        StartCoroutine(DoFetchAndShow());
    }

    private IEnumerator DoFetchAndShow()
    {
        // 1) Fetch the top 10 list
        yield return FetchTopHighscoresRoutine();

        // 2) Show the panel
        leaderboardCanvas.SetActive(true);
    }

    /// <summary>
    /// Submit the given score under the current PlayerID.
    /// </summary>
    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, response =>
        {
            done = true;
        });

        yield return new WaitWhile(() => !done);
    }

    /// <summary>
    /// Fetches the top 10 scores and fills the UI.
    /// </summary>
    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;

        LootLockerSDKManager.GetScoreList(leaderboardID, 10, response =>
        {
            if (response.success && response.items != null)
            {
                string tempNames = "Solana Address\n";
                string tempScores = "Scores\n";

                foreach (var member in response.items)
                {
                    tempNames += $"{member.rank}. " +
                                  $"{(string.IsNullOrEmpty(member.player.name) ? member.player.id : member.player.name)}\n";
                    tempScores += $"{member.score}\n";
                }

                playerNames.text = tempNames;
                playerScores.text = tempScores;

                if (response.items.Length > 0 && topHighScoreText != null)
                    topHighScoreText.text = $"Top Score: {response.items[0].score}";
            }
            done = true;
        });

        yield return new WaitWhile(() => !done);
    }

    /// <summary>
    /// Retrieves the current player's rank.
    /// </summary>
    public IEnumerator GetPlayerRankRoutine(System.Action<bool> callback)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.GetMemberRank(leaderboardID, playerID, response =>
        {
            if (response.success)
            {
                playerRank = response.rank;
                callback(true);
            }
            else callback(false);
            done = true;
        });

        yield return new WaitWhile(() => !done);
    }

    /// <summary>
    /// Hide the leaderboard popup.
    /// </summary>
    public void HideLeaderboard()
    {
        leaderboardCanvas.SetActive(false);
    }

    /// <summary>
    /// Utility to check if a score would land in the top 10.
    /// </summary>
    public bool IsTop10(int score)
    {
        if (playerScores == null || string.IsNullOrEmpty(playerScores.text))
            return false;

        var lines = playerScores.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (int.TryParse(lines[i], out int listed) && score > listed)
                return true;
        }

        return lines.Length < 11;
    }
}
