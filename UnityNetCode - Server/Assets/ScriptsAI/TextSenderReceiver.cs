using Unity.Netcode;
using UnityEngine;

public class TextSenderReceiver : NetworkBehaviour
{
    // Network variable to store the text
    private NetworkVariable<string> receivedText = new NetworkVariable<string>("", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // Method to send text from the mobile scene
    public void SendText(string text)
    {
        if (IsOwner)
        {
            receivedText.Value = text;
            Debug.Log("Text sent: " + text);
        }
        
    }

    // Method to receive text in the desktop scene
    private void Update()
    {
        if (!IsOwner && !string.IsNullOrEmpty(receivedText.Value))
        {
            Debug.Log("Text received: " + receivedText.Value);
            receivedText.Value = ""; // Clear the text after receiving
        }
    }
}