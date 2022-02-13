using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerControls controls;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public LayerMask groundMask;
    [HideInInspector] public LayerMask enemyMask;
    [HideInInspector] public float timeSinceLastAttack;

    [Header("Movement")]
    public Transform groundCheck;
    public float movementSpeed;
    public float jumpForce;
    public float groundRadius;

    [Header("Attacking")]
    public AnimationClip playerAttackAnimation;
    public Transform attackPoint;
    public float attackTimeLength;
    public float attackRadius;

    public IdleState idleState;
    public AttackingState attackingState;
    public MovingState walkingState;

    private BaseState currentState;

    private void Awake()
    {
        controls = new PlayerControls();
        idleState = new IdleState(this);
        walkingState = new MovingState(this);
        attackingState = new AttackingState(this);
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        enemyMask = LayerMask.GetMask("Enemy");
        groundMask = LayerMask.GetMask("Ground");
        timeSinceLastAttack = attackTimeLength + 1;

        ChangeState(idleState);
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        currentState.Update();
    }

    public void ChangeState(BaseState state)
    {
        currentState = state;
        currentState.Initialize();
    }
}
