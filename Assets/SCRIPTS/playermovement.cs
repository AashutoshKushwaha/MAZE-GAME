using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;                  // Reference to the player's Rigidbody
    public float sidewaysForce = 10f;     // Force for lateral movement (sideways and forward)
    public float jumpForce = 5f;          // Force for jumping
    public Transform cameraTransform;     // Reference to the camera's Transform

    private bool isGrounded = true;       // Check if the player is on the ground

    void Start()
    {
        // Lock and hide the cursor for first-person-style control
        Cursor.lockState = CursorLockMode.Locked;

        // Freeze rotation in Rigidbody to prevent rolling or tipping
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        // Get movement input from the keyboard (W, A, S, D)
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey("w"))
            moveVertical = 1f;  // Move forward
        if (Input.GetKey("s"))
            moveVertical = -1f; // Move backward
        if (Input.GetKey("a"))
            moveHorizontal = -1f; // Move left
        if (Input.GetKey("d"))
            moveHorizontal = 1f;  // Move right

        // Calculate movement direction relative to the camera's orientation
        Vector3 moveDirection = cameraTransform.forward * moveVertical + cameraTransform.right * moveHorizontal;
        moveDirection.y = 0f; // Ensure no vertical movement
        rb.AddForce(moveDirection.normalized * sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);

        // Jumping: Apply upward force only when grounded
        if (Input.GetKey("space") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false; // Prevent further jumps until grounded
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is back on the ground
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.9f)
        {
            isGrounded = true;
        }
    }
}
