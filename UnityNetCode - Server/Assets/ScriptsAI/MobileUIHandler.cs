using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MobileUIHandler : MonoBehaviour
{
    public InputField inputField;
    public GameObject textSenderReceiver;

    public void OnSendButtonClicked()
    {
        string textToSend = inputField.text;

        GameObject handler = Instantiate(textSenderReceiver);
        TextSenderReceiver Tsr = handler.GetComponent<TextSenderReceiver>();

        Tsr.SendText(textToSend);
    }
}