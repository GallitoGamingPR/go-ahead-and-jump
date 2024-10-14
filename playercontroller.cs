using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject prefab;
    public GameObject shootPoint;

    // Movement parameters
    public float moveSpeed = 5f;            // Speed for moving horizontally
    public float fastMoveSpeed = 10f;       // Speed for moving horizontally fast
    public float jumpForce = 10f;          // Jump force
    public float verticalMoveSpeed = 1f;  // Speed for moving up or down
    public float fastVerticalMoveSpeed = 5f;  // Speed for moving up or down quickly

    public float lookSensitivity = 2f;  // Look sensitivity

    // Ground check variables
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundDistance = 0.2f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalMoveInput;

    public Camera playerCamera;
    private float cameraPitch = 0f;

    // Create an instance of the Input Action class
    public PlayerInputActions inputActions;

    void Awake()
    {
        // Initialize the Input Actions
        inputActions = new PlayerInputActions();
        // Lock cursor and make it invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        // Enable the input actions
        inputActions.Enable();

        // Bind movement action to the Move method
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        // Bind vertical movement action
        inputActions.Player.VerticalMove.performed += ctx => verticalMoveInput = ctx.ReadValue<float>();
        inputActions.Player.VerticalMove.canceled += _ => verticalMoveInput = 0;

        // Bind look action to the Look method
        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += _ => lookInput = Vector2.zero;

        // Bind the jump action to the Jump method
        inputActions.Player.Jump.performed += _ => Jump();

        // Bind the shoot action to the Shoot method
        inputActions.Player.Fire.performed += _ => Shoot();
    }

    void OnDisable()
    {
        // Disable the input actions when the script is disabled
        inputActions.Disable();
    }

    void Start()
    {
        // Initialize the Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle looking around (camera movement)
        LookAround();
    }

    void FixedUpdate()
    {
        // Handle player movement
        Move();

        // Ground check for jumping
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }

    // Method to control the player's movement in 3D
    void Move()
    {
        // Determine the movement speed (regular or fast)
        float speed = Input.GetKey(KeyCode.LeftShift) ? fastMoveSpeed : moveSpeed;

        // Determine the vertical movement speed (regular or fast)
        float verticalSpeed = Input.GetKey(KeyCode.LeftShift) ? fastVerticalMoveSpeed : verticalMoveSpeed;

        // Calculate movement direction independent of camera pitch
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Zero out the y component for the movement vectors to maintain horizontal movement
        forward.y = 0;
        right.y = 0;

        // Normalize the direction vectors to ensure consistent movement speed
        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction based on input and normalized directions
        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        // Add vertical movement based on verticalMoveInput, applying world up direction
        moveDirection += Vector3.up * verticalMoveInput * verticalSpeed;

        // Apply the calculated velocity to the Rigidbody, including vertical movement modifications
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y + (verticalMoveInput * verticalSpeed), moveDirection.z * speed);
    }


    // Method for jumping
    void Jump()
    {
        if (isGrounded)
        {
            // Apply an upward force for jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // Prevent jumping again until the player is grounded
        }
    }

    // Method to handle looking around with mouse or gamepad (rotating the camera)
    void LookAround()
    {
        // Get the mouse or gamepad look input
        Vector2 look = lookInput * lookSensitivity;

        // Adjust camera pitch (up/down rotation)
        cameraPitch -= look.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -89f, 89f);  // Clamping to avoid flipping and stopping

        // Apply pitch to the camera's local rotation
        playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);

        // Rotate the player around the Y axis (horizontal look)
        transform.Rotate(Vector3.up * look.x);
    }

    // Method to control shooting
    void Shoot()
    {
        if (prefab != null && shootPoint != null)
        {
            // Instantiate the bullet prefab at the shoot point's position and rotation
            GameObject bullet = Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation);
        }
    }
}