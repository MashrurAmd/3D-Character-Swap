using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;


    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;

    private float yVelocity;
    private float xRotation = 0f;


    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float z = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 move = transform.right * x + transform.forward * z;

        // Check walking
        bool isWalking = move.magnitude > 0f;
        animator.SetBool("isWalking", isWalking);

        // Check running
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && z > 0;
        animator.SetBool("isRunning", isRunning);

        float speed = isRunning ? runSpeed : walkSpeed;

        // Gravity
        if (controller.isGrounded && yVelocity < 0)
            yVelocity = -2f;

        yVelocity += gravity * Time.deltaTime;

        // Apply gravity
        Vector3 velocity = move * speed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }


    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Rotate the camera up/down (pitch)
        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the character left/right (yaw)
        transform.Rotate(Vector3.up * mouseX);
    }



}
