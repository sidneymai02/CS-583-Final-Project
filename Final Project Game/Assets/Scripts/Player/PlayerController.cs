using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Camera movement
    [SerializeField] Transform playerCamera; // Camera movement with mouse
    [SerializeField] float mouseSensitivity = 1.0f; // Create sensitivity for mouse
    [SerializeField] bool lockCursor = true; // Keep mouse cursor locked in the center of the screen
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f; // Smoothing of movement with a range slider between 0 and 5
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f; // Smoothing of mouse movement with a range slider between 0 and 5

    // Player Movement
    [SerializeField] float walkSpeed = 5.0f; // Walk speed of the character
    [SerializeField] float sprintSpeed = 10.0f; // Walk speed of the character
    [SerializeField] float gravity = -9.81f; // Adds gravity to the character
    [SerializeField] private AnimationCurve jumpFallOff; // Specify how to jump
    [SerializeField] private float jumpMultiplier; // Multiplier to jump
    [SerializeField] private KeyCode jumpKey; // Key to jump
    [SerializeField] private bool canSprint = true; // Boolean to check if the player can sprint
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift; // Sets the sprint key to left shift

    float cameraPitch = 0.0f; // Track the camera's current x rotation
    float velocityY = 0.0f; // Track the downward speed of the character
    CharacterController controller; // Handles the movement and collision

    Vector2 currentDir = Vector2.zero; // Stores current direction
    Vector2 currentDirVelocity = Vector2.zero; // Stores current direction velocity

    Vector2 currentMouseDelta = Vector2.zero; // Stores current mouse direction
    Vector2 currentMouseDeltaVelocity = Vector2.zero; // Stores current mouse direction velocity

    private bool isGrounded; // Check to see if player is jumping
    private bool isSprinting => canSprint && Input.GetKey(sprintKey); // Check to see if the player is sprinting

    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            // Obtains the character controller
            controller = GetComponent<CharacterController>();
            // Locks cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
            // Makes the cursor invisible
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    // Mouse look functionalities
    void UpdateMouseLook()
    {
        // Vertical and horizontal movement of the mouse. isSprinting and sprintSpeed to add in sprint feature
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Smoothly transition current mouse position to the target value
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        // Vertical movement to influence rotation
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        // Clamp the camera's rotation values between -90 and 90
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        // Set camera's local euler angles to rotate around right angle
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        // Rotate on the horizontal motion of the cursor
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    // Handles all normal movement functionality
    void UpdateMovement()
    {
        JumpInput();

        // Target value of the horizontal axis that you want to smooth towards
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Normalizes the vector
        targetDir.Normalize();

        // Current direction the player is moving in
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Smooth diagonal movement
        controller.SimpleMove(Vector3.ClampMagnitude(currentDir, 1.0f));

        // Check to see if the controller is grounded
        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }
        // Set velocity in Y direction to gravity
        velocityY += gravity * Time.deltaTime;


        // Velocity vector to set the speed and gravity
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * (isSprinting ? sprintSpeed : walkSpeed) + Vector3.up * velocityY;

        // Apply velocity to the character controller
        controller.Move(velocity * Time.deltaTime);

    }

    private void JumpInput()
    {
        isGrounded = controller.isGrounded;
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            isGrounded = false;
            StartCoroutine(JumpEvent());
        }
    }

    // Jump routine to create the movement
    private IEnumerator JumpEvent()
    {
        // Helps remove jittering when jumping against objects
        controller.slopeLimit = 90.0f;

        // Tracks the amount of time the player has been in the air
        float timeInAir = 0.0f;

        // Runs the jumping event
        do
        {
            // Jump force in the air
            float jumpForce = jumpFallOff.Evaluate(timeInAir);

            // Jumping animation (move upwards)
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);

            // Increment the time in the air
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        // Helps remove jittering when jumping against objects
        controller.slopeLimit = 45.0f;

        // Jumping has finished
        isGrounded = true;
    }
}
