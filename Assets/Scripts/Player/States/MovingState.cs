using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingState : BaseState
{
    public MovingState(PlayerController controller) : base(controller) { }

    public override void Initialize()
    {
        controller.animator.SetBool("IsWalking", true);

        controller.controls.Player.Jump.performed += Jump;
        controller.controls.Player.Attack.performed += OnAttack;
    }

    public override void Update()
    {
        Move();
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!IsGrounded()) return;
        controller.rb.velocity = new Vector2(controller.rb.velocity.x, controller.jumpForce);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(controller.groundCheck.position, controller.groundRadius, controller.groundMask);
    }

    private void Move()
    {
        Vector2 input = controller.controls.Player.Move.ReadValue<Vector2>();

        if (input.x == 0)
        {
            controller.ChangeState(controller.idleState);
            return;
        }

        else if (input.x > 0)
        {
            controller.spriteRenderer.flipX = false;
            controller.attackPoint.localPosition = new Vector3(0.79f, 0.03f, 0);
        }

        else if (input.x < 0)
        {
            controller.spriteRenderer.flipX = true;
            controller.attackPoint.localPosition = new Vector3(-0.79f, 0.03f, 0);
        }

        controller.transform.Translate(new Vector2(input.x, 0) * controller.movementSpeed * Time.deltaTime);
    }
}
