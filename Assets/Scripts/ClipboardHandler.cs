using UnityEngine;
using TMPro;

public class ClipboardHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    void Update()
    {
        if (Application.isFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalCall("RequestClipboardPaste", gameObject.name, "ReceiveClipboardData");
#endif
        }
    }

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
