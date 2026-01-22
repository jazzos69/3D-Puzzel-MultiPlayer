using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    [Header("Footsteps")]
    public AudioClip footStepSound;
    public float footStepDelay = 0.5f;
    private float nextFootstep;

    [Header("Input Actions")]
    public InputAction moveAction;
    public InputAction jumpAction;

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 motion = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(motion * speed * Time.deltaTime);

        if (jumpAction.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HandleFootsteps(moveInput);
    }

    private void HandleFootsteps(Vector2 moveInput)
    {
        if (moveInput.magnitude > 0.1f && isGrounded)
        {
            nextFootstep -= Time.deltaTime;

            if (nextFootstep <= 0f)
            {
                GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                nextFootstep = footStepDelay;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}


