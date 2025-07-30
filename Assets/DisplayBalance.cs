using System;
using Solana.Unity.SDK;
using TMPro;
using UnityEngine;

public class DisplayBalance : MonoBehaviour
{
    private TextMeshProUGUI _txtBalance;

    private void Start()
    {
        _txtBalance = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Web3.OnBalanceChange += OnBalanceChange;
    }

    private void OnDisable()
    {
        Web3.OnBalanceChange -= OnBalanceChange;
    }

    private void OnDestroy()
    {
        Web3.OnBalanceChange -= OnBalanceChange;
    }

    private void OnBalanceChange(double amount)
    {
        if (_txtBalance != null)
            _txtBalance.text = amount.ToString();
    }
}
