using Unity.Netcode;
using UnityEngine;

public class MobileNetworkStarter : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Singleton.StartClient();
    }
}