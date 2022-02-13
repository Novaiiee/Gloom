using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseState
{
    protected PlayerController controller;

    public BaseState(PlayerController controller)
    {
        this.controller = controller;
    }

    public virtual void Update() { }
    public virtual void Initialize() { }

    protected void OnAttack(InputAction.CallbackContext ctx)
    {
        controller.ChangeState(controller.attackingState);
    }
}
