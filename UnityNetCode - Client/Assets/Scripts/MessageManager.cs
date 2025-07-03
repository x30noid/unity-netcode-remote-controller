using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : NetworkBehaviour
{
    private GameObject MobileCanvas;
    private GameObject DesktopCanvas;
    private GameObject InputText;
    private InputField InputTextValue;
    private GameObject submitButton;

    public override void OnNetworkSpawn()
    {
        MobileCanvas = GameObject.Find("MobileCanvas");
        DesktopCanvas = GameObject.Find("DesktopCanvas");
        InputText = MobileCanvas.transform.GetChild(1).gameObject;
        submitButton = MobileCanvas.transform.GetChild(2).gameObject;

        //Debug.Log("Input Name : " + InputText.name + "Submit Name : " +  submitButton.name);

        InputTextValue = InputText.GetComponent<InputField>();
        submitButton.GetComponent<Button>().onClick.AddListener(OnSubmit);
    }

    public void OnSubmit()
    {
        //Debug.Log("Button Clicked");
        string message = InputTextValue.text;
        MessageSenderServerRPC(message);
    }

    [ServerRpc]
    private void MessageSenderServerRPC(string message)
    {
        GameObject newMessage = Instantiate(this.gameObject);
        newMessage.GetComponent<NetworkObject>().Spawn(); // Spawn the object on the network

        newMessage.GetComponent<Text>().text = message;
        newMessage.transform.SetParent(DesktopCanvas.transform);

        //this.gameObject.GetComponent<Text>().text = message;
        //this.gameObject.transform.SetParent(DesktopCanvas.transform);
        //this.gameObject.transform.localPosition = Vector2.zero;

        Debug.Log("Sender (" + OwnerClientId + ") : " + message);
    }
}
