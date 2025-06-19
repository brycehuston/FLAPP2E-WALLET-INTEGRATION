using UnityEngine;
using TMPro;

public class ClipboardHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    public void OnPasteClicked()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalCall("RequestClipboardPaste", gameObject.name, "ReceiveClipboardData");
#endif
    }

    public void ReceiveClipboardData(string data)
    {
        inputField.text = data;
    }
}