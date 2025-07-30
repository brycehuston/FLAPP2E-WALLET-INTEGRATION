using System;
using Solana.Unity.SDK;
using Solana.Unity.Wallet;
using TMPro;
using UnityEngine;

public class DisplayPublicKey : MonoBehaviour
{
    private TextMeshProUGUI _txtPublicKey;

    private void Start()
    {
        _txtPublicKey = GetComponent<TextMeshProUGUI>();

        // If they connected already this session, show it:
        string savedKey = PlayerPrefs.GetString("WalletAddress", "");
        if (!string.IsNullOrEmpty(savedKey))
            _txtPublicKey.text = savedKey;
        else
            _txtPublicKey.text = "";
    }

    private void OnEnable() => Web3.OnLogin += OnLogin;
    private void OnDisable() => Web3.OnLogin -= OnLogin;
    private void OnDestroy() => Web3.OnLogin -= OnLogin;

    private void OnApplicationQuit()
    {
        // Forget it when they quit, so next launch is a fresh connect
        PlayerPrefs.DeleteKey("WalletAddress");
        PlayerPrefs.Save();
    }

    private void OnLogin(Account account)
    {
        // Save only the Phantom address here:
        string pubKey = account.PublicKey.ToString();
        _txtPublicKey.text = pubKey;
        PlayerPrefs.SetString("WalletAddress", pubKey);
        PlayerPrefs.Save();
    }
}
