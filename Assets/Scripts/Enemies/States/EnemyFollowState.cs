using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFollowState : EnemyBaseState
{
    public EnemyFollowState(EnemyAI enemy) : base(enemy) { }

    public override void Initialize() { }

    public override void Update()
    {
        if (!TargetInDistance(enemy.activationDistance)) enemy.ChangeState(enemy.patrolState);

        DetectObstacle();
        FlipEnemy();

        if (Mathf.Abs(enemy.transform.position.x - enemy.target.position.x) > enemy.stoppingDistance)
        {
            Vector2 position = enemy.target.position;
            position.y = 0;

            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, position, enemy.speed * Time.deltaTime);
        }
    }

    private void FlipEnemy()
    {
        if (enemy.target.position.x < enemy.transform.position.x)
        {
            enemy.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            enemy.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
}
