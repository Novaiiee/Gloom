using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] Transform attackPoint;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerControls controls;
    private LayerMask groundMask;

    private void Awake() => controls = new PlayerControls();
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        groundMask = LayerMask.GetMask("Ground");
        controls.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        Move();
        ChangeAnimation();
    }

    private void Move()
    {
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        transform.Translate(new Vector2(input.x, 0) * movementSpeed * Time.deltaTime);
    }

    private void ChangeAnimation()
    {
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();

        if (input.x > 0)
        {
            animator.SetBool("IsWalking", true);
            spriteRenderer.flipX = false;
            attackPoint.localPosition = new Vector3(0.79f, 0.03f, 0);
        }

        else if (input.x < 0)
        {
            animator.SetBool("IsWalking", true);
            spriteRenderer.flipX = true; 
            attackPoint.localPosition = new Vector3(-0.79f, 0.03f, 0);
        }

        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!IsGrounded()) return;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
    }
}
