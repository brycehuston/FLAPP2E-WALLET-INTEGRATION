// using UnityEngine;
// using TMPro;
// using System.Runtime.InteropServices;

// public class WebGLPasteSupport : MonoBehaviour
// {
// #if UNITY_WEBGL && !UNITY_EDITOR
//     [DllImport("__Internal")]
//     private static extern void RequestClipboardPaste(string objName, string methodName);
// #endif

//     public TMP_InputField inputField;

//     void Update()
//     {
// #if UNITY_WEBGL && !UNITY_EDITOR
//         if (inputField != null && inputField.isFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
//         {
//             RequestClipboardPaste(gameObject.name, "ReceivePasteData");
//         }
// #endif
//     }

// #if UNITY_WEBGL && !UNITY_EDITOR
//     [AOT.MonoPInvokeCallback(typeof(System.Action<string>))]
// #endif
//     public void ReceivePasteData(string data)
//     {
//         inputField.text += data;
//     }
// }
