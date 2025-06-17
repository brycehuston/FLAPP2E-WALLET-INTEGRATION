using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text highScoreText; // Assign this in the Unity Inspector

    public void UpdateHighScore(int highScore)
    {
        highScoreText.text = "~ " + highScore + " ~";
    }
}
