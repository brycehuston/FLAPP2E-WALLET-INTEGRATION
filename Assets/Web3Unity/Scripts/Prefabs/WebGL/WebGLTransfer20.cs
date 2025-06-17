using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Web3Unity.Scripts.Library.ETHEREUEM.EIP;
using Web3Unity.Scripts.Prefabs;
using UnityEngine.SceneManagement;

#if UNITY_WEBGL
public class WebGLTransfer20 : MonoBehaviour
{
    [SerializeField]
    private string contract = "0xADD225e949f1650F96e6BE70CcD5c266f786D052";
    [SerializeField]
    private string toAccount = "0xE1D48F8aFE5Ee3D35Cb3527Bf3D858f809A1E08d";
    [SerializeField]
    private string amount = "1500000000000000000";

    private readonly string abi = ABI.ERC_20;

    public AudioSource soundPlayer;

    private void OnEnable() 
    {
        // Reset or reinitialize necessary components when the scene is enabled
        InitializeScene();
    }

    private void InitializeScene()
    {
        // Add any initialization logic here if needed
        // For example, resetting variables, UI elements, etc.
    }

    async public void Transfer()
    {
        string method = "transfer";
        string[] obj = { toAccount, amount };
        string args = JsonConvert.SerializeObject(obj);
        string value = "0";
        string gasLimit = "";
        string gasPrice = "";

        try
        {
            string response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }
}
#endif
