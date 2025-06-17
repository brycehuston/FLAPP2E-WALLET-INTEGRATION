using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource soundPlayer;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void backButton()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }
}
