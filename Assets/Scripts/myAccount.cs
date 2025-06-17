using UnityEngine;
using UnityEngine.UI;

public class GetAccount : MonoBehaviour
{
    public Text myAccount;

    void Start()
    {
        string account = PlayerPrefs.GetString("Account");
        myAccount.text = account;
    }
}
