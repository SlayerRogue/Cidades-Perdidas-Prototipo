using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    public float walkSpeed = 3.5f;
    public float crouchSpeed = 1.8f;
    public float gravity = -15f;

    [Header("Agachar")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchTransitionSpeed = 10f;
    public float standingCameraY = 1.6f;
    public float crouchCameraY = 0.9f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;

    [Header("Referências")]
    public Transform cameraTransform;
    public CharacterController controller;

    private Vector3 velocity;
    private bool isCrouching = false;
    private float currentHeight;
    private float currentCameraY;
    private float rotationX = 0f;

    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        currentHeight = standingHeight;
        currentCameraY = standingCameraY;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleCrouch();
        HandleMovement();
        ApplyGravity();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
        }

        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        float targetCameraY = isCrouching ? crouchCameraY : standingCameraY;

        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        currentCameraY = Mathf.Lerp(currentCameraY, targetCameraY, Time.deltaTime * crouchTransitionSpeed);

        controller.height = currentHeight;
        controller.center = new Vector3(0, currentHeight / 2f, 0);

        Vector3 camPos = cameraTransform.localPosition;
        camPos.y = currentCameraY;
        cameraTransform.localPosition = camPos;
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        move.Normalize();

        float currentSpeed = isCrouching ? crouchSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }
}