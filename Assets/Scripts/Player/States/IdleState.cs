using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{
    public IdleState(PlayerController controller) : base(controller) { }

    public override void Initialize()
    {
        controller.animator.SetBool("IsWalking", false);
        controller.controls.Player.Attack.performed += OnAttack;
    }

    public override void Update()
    {
        Vector2 input = controller.controls.Player.Move.ReadValue<Vector2>();

        if (input.x > 0 || input.x < 0)
        {
            controller.ChangeState(controller.walkingState);
            return;
        }
    }
}
