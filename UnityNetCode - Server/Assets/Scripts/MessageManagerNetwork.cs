using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MessageManagerNetwork : NetworkBehaviour
{
    private GameObject MobileCanvas;
    private GameObject DesktopCanvas;
    private GameObject InputText;
    private InputField InputTextValue;
    private GameObject submitButton;
    private GameObject PlaceButton;
    private string message;
    public GameObject PointerPrefab;
    private Transform PointerTransfrom;
    private Vector2 PointerPos;
    private bool isSpawned = false;

    public override void OnNetworkSpawn()
    {
        MobileCanvas = GameObject.Find("MobileCanvas");
        DesktopCanvas = GameObject.Find("DesktopCanvas");
        InputText = MobileCanvas.transform.GetChild(1).gameObject;
        submitButton = MobileCanvas.transform.GetChild(2).gameObject;
        PlaceButton = MobileCanvas.transform.GetChild(3).gameObject;
        //Debug.Log("Input Name : " + InputText.name + "Submit Name : " +  submitButton.name);
        PlaceButton.SetActive(false);
        InputTextValue = InputText.GetComponent<InputField>();
        submitButton.GetComponent<Button>().onClick.AddListener(OnSubmit);
        PlaceButton.GetComponent<Button>().onClick.AddListener(OnClickTarget);
    }

    public void OnSubmit()
    {
        InputText.SetActive(false);
        submitButton.SetActive(false);
        PlaceButton.SetActive(true);
        InstantiatePointerServerRPC();
    }

    public void OnClickTarget()
    {
        message = InputTextValue.text;
        MessageSenderServerRPC(message);
        PlaceButton.SetActive(false);
        InputText.SetActive(true);
        submitButton.SetActive(true);
    }

    public void Update()
    {
        
        GameObject pointer = GameObject.Find("Pointer(Clone)");
        
        if (pointer != null)
        {
            Vector2 pointerPos = pointer.GetComponent<MoveRawImageGyro>().newPosition;
            PointerPos = pointerPos;

            Debug.Log(pointerPos);

            SetPointerPosServerRpc(PointerPos);
        }

    }

    [ServerRpc]
    private void InstantiatePointerServerRPC()
    {
        PointerTransfrom = Instantiate(PointerPrefab.transform);
        PointerTransfrom.GetComponent<NetworkObject>().Spawn(); // Spawn the object on the network
        PointerTransfrom.transform.SetParent(DesktopCanvas.transform);

        isSpawned = true;
        
    }

    [ServerRpc]
    private void SetPointerPosServerRpc(Vector2 PointerPosition)
    {
        if (PointerTransfrom != null && isSpawned)
        {
            PointerTransfrom.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(PointerPosition.x, PointerPosition.y);

        }

    }

    [ServerRpc]
    private void MessageSenderServerRPC(string message)
    {

        Transform newMessage = Instantiate(gameObject.transform);
        newMessage.GetComponent<NetworkObject>().Spawn(); // Spawn the object on the network

        newMessage.GetComponent<Text>().text = message;
        newMessage.transform.SetParent(DesktopCanvas.transform);

        newMessage.gameObject.transform.localPosition = PointerPos;

        PointerTransfrom.GetComponent<NetworkObject>().Despawn();

        isSpawned = false;

        Debug.Log("Sender (" + OwnerClientId + ") : " + message);
    }
}
