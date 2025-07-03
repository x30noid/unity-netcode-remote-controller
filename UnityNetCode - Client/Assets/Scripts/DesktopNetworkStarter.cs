using Unity.Netcode;
using UnityEngine;

public class DesktopNetworkStarter : MonoBehaviour
{

    void Start()
    {
        NetworkManager.Singleton.StartServer();
    }

}