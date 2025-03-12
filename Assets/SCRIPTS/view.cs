using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;           // Reference to the player's Transform
    public float mouseSensitivity = 100f; // Mouse sensitivity for rotation
    public float verticalRotationLimit = 80f; // Limit for up/down camera rotation

    private float rotationX = 0f;      // Current X-axis rotation for the camera
    private float rotationY = 0f;      // Current Y-axis rotation for the camera


    void Start()
    {
        // Lock the cursor to the center of the screen for first-person control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply horizontal rotation (rotate player around the Y-axis)
        player.Rotate(Vector3.up * mouseX);
        
        // Apply vertical rotation (rotate camera around the X-axis, limited)
        rotationY -=mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    void LateUpdate()
    {
        // Keep the camera at the same position as the player (no offset)
        transform.position = player.position;
    }
}
