using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveRawImageGyro : MonoBehaviour
{
    //public RectTransform uiElement; // Assign your UI RawImage RectTransform here
    public float rotationSensitivity = 20.0f; // Adjust sensitivity as needed
    public float movementSensitivity = 1.0f; // Adjust sensitivity as needed
    public float maxOffsetX = 900f; // Maximum offset X from the center
    public float maxOffsetY = 500f; // Maximum offset Y from the center
    public Vector2 newPosition;
    private Vector2 initialPosition;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        initialPosition = Vector2.zero;
    }

    void Update()
    {
        // Enable the gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

        // Get the current device rotation rate around the Y-axis
        float rotationRateX = Input.gyro.rotationRateUnbiased.y;
        float rotationRateY = Input.gyro.rotationRateUnbiased.x;

        // Calculate the movement along the X-axis based on Y rotation
        float deltaX = -rotationRateX * rotationSensitivity;
        float deltaY = rotationRateY * rotationSensitivity;

        // Update the UI element's position
        newPosition = rectTransform.anchoredPosition;
        newPosition.x += deltaX;
        newPosition.y += deltaY;

        // Optionally, clamp the position to keep the UI element within screen bounds
        //newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        //newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.x = Mathf.Clamp(newPosition.x, initialPosition.x - maxOffsetX, initialPosition.x + maxOffsetX);
        newPosition.y = Mathf.Clamp(newPosition.y, initialPosition.y - maxOffsetY, initialPosition.y + maxOffsetY);

        rectTransform.anchoredPosition = new Vector2(newPosition.x, newPosition.y);

    }

}
