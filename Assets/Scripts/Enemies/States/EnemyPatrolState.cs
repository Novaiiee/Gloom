public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyAI enemy) : base(enemy) { }

    public override void Update()
    {
        if (TargetInDistance(enemy.activationDistance)) enemy.ChangeState(enemy.followState);
    }

    public override void Initialize() { }
}