using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float rotationSpeed = 10f;

    [Header("Gravity & Jump")]
    public float gravity = -9.81f;
    public float jumpForce = 5f;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Įvestis
        float x = Input.GetAxis("Horizontal");   // A/D arba ← →
        float z = Input.GetAxis("Vertical");     // W/S arba ↑ ↓

        // Kryptis pagal kamerą
        Transform cam = Camera.main.transform;

        Vector3 camForward = cam.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDir = camForward * z + camRight * x;
        float inputMagnitude = new Vector2(x, z).magnitude;

        // Judėjimas ir sukimasis
        if (moveDir.magnitude > 0.1f)
        {
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        // Animator parametras (Idle/Walk)
        if (animator != null)
        {
            animator.SetFloat("Speed", inputMagnitude);
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Jump
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }
    }
}
