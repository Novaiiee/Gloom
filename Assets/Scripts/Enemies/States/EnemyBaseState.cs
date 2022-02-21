using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBaseState
{
    protected EnemyAI enemy;

    public EnemyBaseState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public virtual void Update() { }
    public virtual void Initialize() { }

    protected bool TargetInDistance(float distance)
    {
        return (enemy.transform.position.x - enemy.target.position.x) < distance;
    }

    protected void Jump()
    {
        enemy.rb.velocity = new Vector2(enemy.rb.velocity.x, enemy.jumpForce);
    }

    protected void DetectObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, -enemy.transform.right, enemy.obstacleJumpingDistance, enemy.groundMask);

        if (hit && IsGrounded())
        {
            Jump();
        }
    }

    protected bool IsGrounded()
    {
        return Physics2D.OverlapCircle(enemy.groundCheck.position, enemy.groundRadius, enemy.groundMask);
    }
}
