using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebWalletLogOut : MonoBehaviour
{
    public AudioSource soundPlayer;
    public void OnLogOut()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }
}
