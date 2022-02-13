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
        if (controller.timeSinceLastAttack < controller.attackTimeLength)
            controller.ChangeState(controller.idleState);

        controller.animator.SetTrigger("IsAttacking");
        Attack();
    }

    private void Attack()
    {
        if (controller.timeSinceLastAttack < controller.attackTimeLength)
            controller.ChangeState(controller.idleState);

        Collider2D enemy = HasHitEnemy();

        if (enemy)
        {
            Debug.Log("we hit an enemy");
        }

        controller.timeSinceLastAttack = 0f;
        controller.ChangeState(controller.idleState);
    }

    private Collider2D HasHitEnemy()
    {
        return Physics2D.OverlapCircle(controller.attackPoint.position, controller.attackRadius, controller.enemyMask);
    }
}
