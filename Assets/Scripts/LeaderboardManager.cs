// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;


// public class LeaderboardManager : MonoBehaviour
// {
//     [SerializeField] private List<TextMeshProUGUI> names;
//     [SerializeField] private List<TextMeshProUGUI> scores;
//     [SerializeField] private GameObject leaderboardCanvas;
//     [SerializeField] private string publicLeaderboardKey = "de31835277de185c540fa6598c69407a30f0fad09aea21a7476fdc569ee7396b";


//     public void LoadEntries()
//     {
//         LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, entries =>
//         {
//             int count = Mathf.Min(entries.Length, names.Count);
//             for (int i = 0; i < count; ++i)
//             {
//                 names[i].text = entries[i].Username;
//                 scores[i].text = entries[i].Score.ToString();
//             }
//         }, error =>
//         {
//             Debug.LogError("LoadEntries failed: " + error); // 🪵 log the error here
//         });
//     }

//     public void SubmitScore(string playerName, int score)
//     {
//         if (string.IsNullOrEmpty(playerName)) return;

//         LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, playerName, score, success =>
//         {
//             if (success) LoadEntries();
//         }, error =>
//         {
//             Debug.LogError("SubmitScore failed: " + error); // 🪵 log error if submit fails
//         });
//     }

//     public void ShowLeaderboard()
//     {
//         if (leaderboardCanvas != null)
//         {
//             leaderboardCanvas.SetActive(true);
//         }
//     }

//     public void HideLeaderboard()
//     {
//         if (leaderboardCanvas != null)
//         {
//             leaderboardCanvas.SetActive(false);
//         }
//     }

//     public void FetchHighScore(Action<int> onHighScoreFetched)
//     {
//         LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, entries =>
//         {
//             int highScore = (entries.Length > 0) ? entries[0].Score : 0;
//             onHighScoreFetched?.Invoke(highScore);
//         }, error =>
//         {
//             Debug.LogError("FetchHighScore failed: " + error); // 🪵 log fetch error
//             onHighScoreFetched?.Invoke(0);
//         });
//     }
// }
