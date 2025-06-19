using System.Collections;
using UnityEngine;
using TMPro;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField walletTextDisplay;
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private GameObject leaderboardCanvas;

    private void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    private IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    private IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
            }
            else
            {
                Debug.Log("Could not start session");
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
    }

    public void SubmitScore()
    {
        string playerName = walletTextDisplay.text;

        if (!IsValidSolAddress(playerName))
        {
            Debug.LogError(" Invalid Solana wallet address.");
            return;
        }

        LootLockerSDKManager.SetPlayerName(playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log(" Wallet address submitted: " + playerName);
            }
            else
            {
                Debug.Log("Failed to set player name: " + response.text);
            }
        });

        StartCoroutine(SubmitAndReturn());
    }

    private IEnumerator SubmitAndReturn()
    {
        yield return leaderboard.SubmitScoreRoutine(GameManager.Score);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(0);
    }

    public void ShowLeaderboardPanel()
    {
        leaderboardCanvas.SetActive(true);
    }

    private bool IsValidSolAddress(string address)
    {
        if (string.IsNullOrEmpty(address)) return false;
        if (address.Length < 32 || address.Length > 44) return false;

        const string base58 = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        foreach (char c in address)
        {
            if (!base58.Contains(c.ToString())) return false;
        }

        return true;
    }
}
