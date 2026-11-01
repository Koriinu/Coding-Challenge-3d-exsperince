using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform cam;
    public float speed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;
    public float mouseSensitivity = 2f;

    CharacterController controller;
    float verticalVelocity;
    float xRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Look
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation - my, -85f, 85f);
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mx);

        // Move
        Vector3 move = transform.right * Input.GetAxis("Horizontal")
                     + transform.forward * Input.GetAxis("Vertical");

        // Jump + gravity
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        verticalVelocity += gravity * Time.deltaTime;

        move = move * speed + Vector3.up * verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}