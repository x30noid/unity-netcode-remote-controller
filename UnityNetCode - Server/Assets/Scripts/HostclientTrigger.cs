using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class HostclientTrigger : NetworkBehaviour
{
    public Button HostButton;
    public Button ClientButton;
    public InputField IPAddressInput;

    void Start()
    {
        HostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        ClientButton.onClick.AddListener(() =>
        {
            string ip = IPAddressInput.text;
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ip;
            NetworkManager.Singleton.StartClient();
        });
    }
}
