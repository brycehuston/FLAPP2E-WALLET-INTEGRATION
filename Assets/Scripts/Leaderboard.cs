// Leaderboard.cs
using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNames;
    [SerializeField] private TextMeshProUGUI playerScores;
    [SerializeField] private GameObject leaderboardCanvas;

    string leaderboardID = "31420";

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
            }
            else
            {
                Debug.Log("Failed to upload score: " + response.text);
            }
            done = true;
        });

        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;

        LootLockerSDKManager.GetScoreList(leaderboardID, 10, (response) =>
        {
            if (response.success && response.items != null)
            {
                string tempNames = "Solana Address\n";
                string tempScores = "Scores\n";

                foreach (var member in response.items)
                {
                    tempNames += member.rank + ". ";
                    tempNames += string.IsNullOrEmpty(member.player.name) ? member.player.id : member.player.name;
                    tempScores += member.score + "\n";
                    tempNames += "\n";
                }

                playerNames.text = tempNames;
                playerScores.text = tempScores;
            }
            else
            {
                Debug.Log("Failed to fetch scores: " + response.text);
            }

            done = true;
        });

        yield return new WaitWhile(() => done == false);
    }

    public void ShowLeaderboard()
    {
        leaderboardCanvas?.SetActive(true);
    }

    public void HideLeaderboard()
    {
        leaderboardCanvas?.SetActive(false);
    }
}
