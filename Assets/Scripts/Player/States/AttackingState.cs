using UnityEngine;

/*
    Walking or idle state changes to this class when attacking

    Check if the time since last attack is greater than the attack speed (animation speed)

    if is then find and enemy to attack and kill them

    otherwise change the state back to idle when finished
*/
public class AttackingState : BaseState
{
    public AttackingState(PlayerController controller) : base(controller) { }

    public override void Initialize()
    {
        Attack();
    }

    private void Attack()
    {
        if (controller.timeSinceLastAttack < controller.attackTimeLength)
        {
            controller.ChangeState(controller.idleState);
            return;
        }

        controller.animator.SetTrigger("IsAttacking");
        Collider2D enemy = HasHitEnemy();

        if (enemy)
        {
            //Enemy Logic
        }

        controller.timeSinceLastAttack = 0f;
        controller.ChangeState(controller.idleState);
    }

    private Collider2D HasHitEnemy()
    {
        return Physics2D.OverlapCircle(controller.attackPoint.position, controller.attackRadius, controller.enemyMask);
    }
}
