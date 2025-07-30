using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Leaderboard leaderboard; // drag in your LeaderboardManager

    private void Start()
    {
        // As soon as the menu loads, fetch the top 10 so the panel is ready instantly
        StartCoroutine(leaderboard.FetchTopHighscoresRoutine());
    }
}
