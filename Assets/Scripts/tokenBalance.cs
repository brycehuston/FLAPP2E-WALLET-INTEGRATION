using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Web3Unity.Scripts.Library.ETHEREUEM.EIP;

public class displayTokenBalance : MonoBehaviour
{
    public Text tokenBalance;
    async void Start()
    {
        // string chain = "ethereum";
        // string network = "sepolia";
        string contract = "0x1e047acd0708ab789d8329773ead8b6603c30318";
        string account = PlayerPrefs.GetString("Account");

        BigInteger balanceOf = await ERC20.BalanceOf(contract, account);
        print(balanceOf);
        tokenBalance.text = balanceOf.ToString();
    }
}
