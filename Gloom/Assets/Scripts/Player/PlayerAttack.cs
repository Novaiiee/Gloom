using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AnimationClip playerAttackAnimation;
    [SerializeField] private float attackTimeLength;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

    private PlayerControls controls;
    private Animator animator;
    private float timeSinceLastAttack;
    private LayerMask enemyMask;

    private void Awake() => controls = new PlayerControls();
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        animator = GetComponent<Animator>();
        timeSinceLastAttack = attackTimeLength + 1;
        controls.Player.Attack.performed += Attack;
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (timeSinceLastAttack < attackTimeLength)
            return;

        animator.SetTrigger("IsAttacking");

        Collider2D enemy = HasHitEnemy();

        if (!enemy)
            return;

        Debug.Log("hit an enemy");
        timeSinceLastAttack = 0f;
    }

    private Collider2D HasHitEnemy()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}
