using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackingState : EnemyBaseState
{
    public EnemyAttackingState(EnemyAI enemy) : base(enemy) { }

    public override void Update()
    {
        if (!TargetInDistance(enemy.stoppingDistance)) enemy.ChangeState(enemy.followState);
        //Attack
    }

    public override void Initialize() { }
}
